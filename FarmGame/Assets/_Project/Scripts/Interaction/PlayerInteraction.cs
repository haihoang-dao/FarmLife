using UnityEngine;

/// <summary>
/// Quản lý quá trình phát hiện và tương tác của Player
/// với các đối tượng có thể tương tác trong thế giới game.
/// </summary>
/// <remarks>
/// Component này chịu trách nhiệm cho toàn bộ luồng tương tác:
///
/// <list type="number">
/// <item>
/// Nhận sự kiện nhấn nút tương tác từ <see cref="PlayerInputReader"/>.
/// </item>
/// <item>
/// Kiểm tra các Collider2D nằm trong vùng tương tác phía trước Player.
/// </item>
/// <item>
/// Tìm component triển khai <see cref="IInteractable"/>.
/// </item>
/// <item>
/// Loại bỏ các đối tượng không hợp lệ hoặc đang bị vô hiệu hóa.
/// </item>
/// <item>
/// So sánh khoảng cách và chọn đối tượng gần InteractionPoint nhất.
/// </item>
/// <item>
/// Điều khiển trạng thái hiển thị của <see cref="InteractionPromptUI"/>.
/// </item>
/// <item>
/// Gọi <see cref="IInteractable.Interact"/> khi người chơi nhấn nút tương tác.
/// </item>
/// </list>
///
/// Component phải được đặt trên cùng GameObject với
/// <see cref="PlayerInputReader"/>.
///
/// Từ khóa <c>sealed</c> ngăn các class khác kế thừa PlayerInteraction,
/// giúp hành vi tương tác của Player được quản lý tập trung tại một nơi.
/// </remarks>
[RequireComponent(typeof(PlayerInputReader))]
public sealed class PlayerInteraction : MonoBehaviour
{
    /// <summary>
    /// Số lượng Collider2D tối đa có thể được lưu lại
    /// trong một lần kiểm tra vùng tương tác.
    /// </summary>
    /// <remarks>
    /// Mảng kết quả có kích thước cố định để có thể được tái sử dụng
    /// trong mỗi frame mà không tạo thêm mảng mới.
    ///
    /// Vùng tương tác hiện tại có bán kính nhỏ và nằm ngay trước Player,
    /// vì vậy giới hạn 16 Collider được xem là đủ cho phạm vi dự án.
    ///
    /// Nếu có nhiều hơn 16 Collider hợp lệ cùng nằm trong vùng kiểm tra,
    /// chỉ những Collider được lưu vào mảng mới được xử lý.
    /// </remarks>
    private const int MaxDetectedColliders = 16;

    /// <summary>
    /// Điểm trung tâm của vùng phát hiện tương tác.
    /// </summary>
    /// <remarks>
    /// InteractionPoint thường là một GameObject con của Player.
    ///
    /// Component <c>PlayerFacingDirection</c> sẽ di chuyển Transform này
    /// sang phía trước Player dựa theo hướng Player đang nhìn.
    ///
    /// Ví dụ:
    ///
    /// <list type="bullet">
    /// <item>Player nhìn lên: InteractionPoint nằm phía trên Player.</item>
    /// <item>Player nhìn xuống: InteractionPoint nằm phía dưới Player.</item>
    /// <item>Player nhìn trái: InteractionPoint nằm bên trái Player.</item>
    /// <item>Player nhìn phải: InteractionPoint nằm bên phải Player.</item>
    /// </list>
    ///
    /// Tham chiếu này phải được gán trong Inspector.
    /// </remarks>
    [Header("Interaction Detection")]
    [SerializeField]
    private Transform interactionPoint;

    /// <summary>
    /// Bán kính của vùng hình tròn dùng để phát hiện đối tượng tương tác.
    /// </summary>
    /// <remarks>
    /// Giá trị càng lớn thì Player có thể phát hiện và tương tác
    /// với đối tượng ở khoảng cách càng xa.
    ///
    /// Thuộc tính <see cref="MinAttribute"/> ngăn không cho nhập
    /// giá trị nhỏ hơn 0 trong Inspector.
    ///
    /// Giá trị mặc định là 0.35 đơn vị Unity.
    /// </remarks>
    [SerializeField, Min(0f)]
    private float interactionRadius = 0.35f;

    /// <summary>
    /// LayerMask xác định những Layer được phép tham gia truy vấn tương tác.
    /// </summary>
    /// <remarks>
    /// Trong dự án Farm Life, trường này thường được đặt thành
    /// Layer <c>Interactable</c>.
    ///
    /// Việc sử dụng LayerMask giúp hệ thống bỏ qua các Collider2D
    /// không liên quan như:
    ///
    /// <list type="bullet">
    /// <item>Player.</item>
    /// <item>Ground.</item>
    /// <item>Building.</item>
    /// <item>Map Collision.</item>
    /// <item>Decoration không thể tương tác.</item>
    /// </list>
    ///
    /// GameObject muốn được phát hiện phải thuộc một Layer
    /// được chọn trong LayerMask này.
    /// </remarks>
    [SerializeField]
    private LayerMask interactableLayer;

    /// <summary>
    /// Component quản lý giao diện thông báo phím tương tác.
    /// </summary>
    /// <remarks>
    /// InteractionPromptUI được dùng để:
    ///
    /// <list type="bullet">
    /// <item>Hiển thị Prompt khi có mục tiêu tương tác hợp lệ.</item>
    /// <item>Ẩn Prompt khi không còn mục tiêu tương tác.</item>
    /// <item>Ẩn Prompt khi PlayerInteraction bị vô hiệu hóa.</item>
    /// </list>
    ///
    /// Trường này có thể để trống. Khi đó hệ thống tương tác
    /// vẫn hoạt động bình thường nhưng Prompt sẽ không hiển thị.
    /// </remarks>
    [Header("Interaction User Interface")]
    [SerializeField]
    private InteractionPromptUI interactionPromptUI;

    /// <summary>
    /// Component cung cấp sự kiện nhấn nút tương tác.
    /// </summary>
    /// <remarks>
    /// PlayerInteraction không trực tiếp đọc bàn phím hoặc tay cầm.
    ///
    /// Thay vào đó, <see cref="PlayerInputReader"/> đọc Input Action
    /// và phát sự kiện InteractPressed.
    ///
    /// Cách tổ chức này giúp tách hệ thống đầu vào khỏi hệ thống tương tác.
    /// </remarks>
    private PlayerInputReader inputReader;

    /// <summary>
    /// Mảng cố định dùng để lưu các Collider2D được phát hiện
    /// trong vùng tương tác.
    /// </summary>
    /// <remarks>
    /// Mảng được tạo một lần khi PlayerInteraction được khởi tạo
    /// và được tái sử dụng trong tất cả các lần truy vấn sau đó.
    ///
    /// Điều này tránh việc tạo một mảng Collider2D mới trong mỗi frame,
    /// giúp giảm cấp phát bộ nhớ và hạn chế Garbage Collection.
    ///
    /// Từ khóa <c>readonly</c> ngăn tham chiếu của biến bị gán sang
    /// một mảng khác sau khi khởi tạo. Các phần tử trong mảng vẫn có
    /// thể được Unity cập nhật sau mỗi truy vấn.
    /// </remarks>
    private readonly Collider2D[] detectedColliders =
        new Collider2D[MaxDetectedColliders];

    /// <summary>
    /// Bộ lọc vật lý dùng để giới hạn kết quả truy vấn tương tác.
    /// </summary>
    /// <remarks>
    /// Bộ lọc được cấu hình trong <see cref="ConfigureInteractionFilter"/>
    /// để chỉ nhận Collider2D thuộc <see cref="interactableLayer"/>.
    ///
    /// Bộ lọc cũng cho phép phát hiện Trigger Collider,
    /// phù hợp với những vùng tương tác không dùng để chặn chuyển động.
    /// </remarks>
    private ContactFilter2D interactionFilter;

    /// <summary>
    /// Mục tiêu tương tác hợp lệ gần InteractionPoint nhất
    /// trong lần cập nhật hiện tại.
    /// </summary>
    /// <remarks>
    /// Giá trị này được cập nhật trong
    /// <see cref="RefreshClosestInteractable"/>.
    ///
    /// Nếu không có đối tượng hợp lệ trong vùng tương tác,
    /// giá trị sẽ là <see langword="null"/>.
    /// </remarks>
    private IInteractable closestInteractable;

    /// <summary>
    /// Khởi tạo các tham chiếu, cấu hình bộ lọc vật lý
    /// và kiểm tra những trường được thiết lập trong Inspector.
    /// </summary>
    /// <remarks>
    /// Unity gọi Awake khi GameObject được khởi tạo.
    ///
    /// Awake chạy trước OnEnable nên <see cref="inputReader"/>
    /// và <see cref="interactionFilter"/> sẽ sẵn sàng
    /// trước khi hệ thống đăng ký sự kiện tương tác.
    /// </remarks>
    private void Awake()
    {
        // Lấy PlayerInputReader trên cùng GameObject.
        //
        // RequireComponent bảo đảm component này phải tồn tại,
        // vì vậy trong điều kiện bình thường kết quả sẽ không phải null.
        inputReader = GetComponent<PlayerInputReader>();

        // Tạo và cấu hình bộ lọc dùng cho các truy vấn vật lý.
        //
        // Bộ lọc sử dụng LayerMask đã được chọn trong Inspector.
        ConfigureInteractionFilter();

        // Không có InteractionPoint thì hệ thống không thể xác định
        // tâm của vùng phát hiện tương tác.
        if (interactionPoint == null)
        {
            Debug.LogError(
                $"{nameof(PlayerInteraction)} trên {name} " +
                "chưa được gán InteractionPoint.",
                this);
        }

        // LayerMask có giá trị 0 nghĩa là không có Layer nào được chọn.
        //
        // Khi đó truy vấn vật lý sẽ không tìm thấy đối tượng tương tác,
        // kể cả khi đối tượng đang nằm ngay phía trước Player.
        if (interactableLayer.value == 0)
        {
            Debug.LogWarning(
                $"{nameof(PlayerInteraction)} trên {name} " +
                "chưa được chọn Interactable Layer.",
                this);
        }

        // InteractionPromptUI là thành phần không bắt buộc.
        //
        // Nếu trường này chưa được gán, hệ thống chỉ cảnh báo
        // chứ không ngăn Player tiếp tục tương tác.
        if (interactionPromptUI == null)
        {
            Debug.LogWarning(
                $"{nameof(PlayerInteraction)} trên {name} " +
                "chưa được gán InteractionPromptUI. " +
                "Tương tác vẫn hoạt động nhưng Prompt sẽ không hiển thị.",
                this);
        }
    }

    /// <summary>
    /// Đăng ký phương thức xử lý với sự kiện nhấn nút tương tác.
    /// </summary>
    /// <remarks>
    /// Unity gọi OnEnable mỗi khi component được bật.
    ///
    /// Sau khi đăng ký, mỗi lần PlayerInputReader phát sự kiện
    /// InteractPressed, phương thức <see cref="HandleInteractPressed"/>
    /// sẽ được gọi.
    /// </remarks>
    private void OnEnable()
    {
        // Toán tử += thêm HandleInteractPressed vào danh sách
        // các phương thức đang lắng nghe sự kiện InteractPressed.
        inputReader.InteractPressed += HandleInteractPressed;
    }

    /// <summary>
    /// Hủy đăng ký sự kiện, xóa mục tiêu hiện tại
    /// và ẩn Interaction Prompt khi component bị tắt.
    /// </summary>
    /// <remarks>
    /// Việc hủy đăng ký giúp:
    ///
    /// <list type="bullet">
    /// <item>Ngăn component đã bị tắt tiếp tục nhận đầu vào.</item>
    /// <item>Tránh một sự kiện gọi cùng một phương thức nhiều lần.</item>
    /// <item>Giữ vòng đời đăng ký sự kiện đồng bộ với component.</item>
    /// </list>
    /// </remarks>
    private void OnDisable()
    {
        // Kiểm tra null trước khi hủy đăng ký để tránh lỗi
        // trong các trường hợp vòng đời component bất thường.
        if (inputReader != null)
        {
            // Loại bỏ HandleInteractPressed khỏi sự kiện InteractPressed.
            inputReader.InteractPressed -= HandleInteractPressed;
        }

        // Xóa mục tiêu cũ vì PlayerInteraction không còn hoạt động.
        closestInteractable = null;

        // Nếu có giao diện Prompt, buộc Prompt phải ẩn
        // khi PlayerInteraction bị vô hiệu hóa.
        if (interactionPromptUI != null)
        {
            interactionPromptUI.SetVisible(false);
        }
    }

    /// <summary>
    /// Cập nhật mục tiêu tương tác và trạng thái Prompt trong mỗi frame.
    /// </summary>
    /// <remarks>
    /// Việc kiểm tra trong Update giúp Prompt phản hồi ngay khi:
    ///
    /// <list type="bullet">
    /// <item>Player đi vào vùng tương tác.</item>
    /// <item>Player rời khỏi vùng tương tác.</item>
    /// <item>InteractionPoint thay đổi theo hướng nhìn.</item>
    /// <item>Đối tượng tương tác được bật hoặc tắt.</item>
    /// </list>
    /// </remarks>
    private void Update()
    {
        // Tìm lại mục tiêu gần nhất và đồng bộ trạng thái Prompt.
        RefreshClosestInteractable();
    }

    /// <summary>
    /// Cấu hình <see cref="ContactFilter2D"/> dùng cho truy vấn tương tác.
    /// </summary>
    /// <remarks>
    /// Bộ lọc chỉ cần được cấu hình một lần trong Awake
    /// vì LayerMask hiện tại không thay đổi trong lúc game chạy.
    ///
    /// Nếu interactableLayer được thay đổi bằng code khi game đang chạy,
    /// phương thức này cần được gọi lại để cập nhật bộ lọc.
    /// </remarks>
    private void ConfigureInteractionFilter()
    {
        // Khởi tạo bộ lọc vật lý mới.
        interactionFilter = new ContactFilter2D
        {
            // Cho phép truy vấn nhận cả Collider thường và Trigger Collider.
            //
            // Trigger thường được sử dụng cho vùng tương tác
            // vì nó không chặn chuyển động của Player.
            useTriggers = true
        };

        // Chỉ cho phép bộ lọc nhận những Collider2D
        // thuộc LayerMask được cấu hình trong Inspector.
        interactionFilter.SetLayerMask(interactableLayer);
    }

    /// <summary>
    /// Tìm lại mục tiêu tương tác gần nhất
    /// và cập nhật trạng thái hiển thị của Prompt.
    /// </summary>
    /// <remarks>
    /// Prompt chỉ được hiển thị khi có ít nhất một đối tượng:
    ///
    /// <list type="bullet">
    /// <item>Nằm trong vùng tương tác.</item>
    /// <item>Thuộc Layer Interactable.</item>
    /// <item>Triển khai <see cref="IInteractable"/>.</item>
    /// <item>Không phải Behaviour đang bị vô hiệu hóa.</item>
    /// </list>
    /// </remarks>
    private void RefreshClosestInteractable()
    {
        // Tìm và lưu đối tượng hợp lệ gần InteractionPoint nhất.
        //
        // Kết quả sẽ là null nếu không có mục tiêu hợp lệ.
        closestInteractable = FindClosestInteractable();

        // InteractionPromptUI là thành phần không bắt buộc,
        // vì vậy cần kiểm tra null trước khi sử dụng.
        if (interactionPromptUI != null)
        {
            // Hiển thị Prompt nếu tìm được mục tiêu.
            // Ẩn Prompt nếu closestInteractable là null.
            interactionPromptUI.SetVisible(
                closestInteractable != null);
        }
    }

    /// <summary>
    /// Xử lý yêu cầu tương tác khi người chơi nhấn Input Action Interact.
    /// </summary>
    /// <remarks>
    /// Mục tiêu được kiểm tra lại ngay tại thời điểm nhấn phím
    /// thay vì chỉ sử dụng kết quả từ frame trước.
    ///
    /// Việc kiểm tra lại giúp tránh tương tác nhầm khi Player
    /// vừa di chuyển hoặc vừa đổi hướng nhìn trong cùng frame.
    /// </remarks>
    private void HandleInteractPressed()
    {
        // Không thể xác định vùng tương tác nếu InteractionPoint chưa được gán.
        if (interactionPoint == null)
        {
            return;
        }

        // Kiểm tra lại mục tiêu ngay tại thời điểm nhấn nút.
        //
        // Phương thức này đồng thời cập nhật lại trạng thái Prompt.
        RefreshClosestInteractable();

        // Chỉ gọi Interact nếu closestInteractable khác null.
        //
        // Toán tử ?. tương đương với:
        //
        // if (closestInteractable != null)
        // {
        //     closestInteractable.Interact();
        // }
        closestInteractable?.Interact();
    }

    /// <summary>
    /// Thu thập các Collider2D trong vùng tương tác
    /// và tìm đối tượng hợp lệ gần InteractionPoint nhất.
    /// </summary>
    /// <remarks>
    /// Phương thức sử dụng phiên bản truy vấn nhận mảng kết quả có sẵn.
    /// Vì vậy, hệ thống không tạo một mảng Collider2D mới trong mỗi frame.
    ///
    /// Khoảng cách được tính từ InteractionPoint đến điểm gần nhất
    /// trên Collider bằng <see cref="Collider2D.ClosestPoint"/>.
    ///
    /// Việc so sánh sử dụng <see cref="Vector2.sqrMagnitude"/>
    /// thay vì khoảng cách thông thường để tránh phép tính căn bậc hai.
    /// </remarks>
    /// <returns>
    /// Đối tượng triển khai <see cref="IInteractable"/> gần nhất;
    /// hoặc <see langword="null"/> nếu không tìm thấy mục tiêu hợp lệ.
    /// </returns>
    private IInteractable FindClosestInteractable()
    {
        // Không thể thực hiện truy vấn nếu InteractionPoint chưa được gán.
        if (interactionPoint == null)
        {
            return null;
        }

        // Lấy vị trí thế giới của InteractionPoint.
        //
        // Hệ thống vật lý 2D chỉ sử dụng trục X và Y,
        // vì vậy vị trí được lưu dưới dạng Vector2.
        Vector2 interactionPosition =
            interactionPoint.position;

        // Thực hiện truy vấn hình tròn và ghi kết quả trực tiếp
        // vào mảng detectedColliders đã được tạo từ trước.
        //
        // Các tham số lần lượt là:
        // 1. Tâm vùng kiểm tra.
        // 2. Bán kính vùng kiểm tra.
        // 3. Bộ lọc Layer và Trigger.
        // 4. Mảng nhận kết quả.
        //
        // Giá trị trả về là số lượng Collider thực tế
        // đã được ghi vào detectedColliders.
        int detectedCount =
            Physics2D.OverlapCircle(
                interactionPosition,
                interactionRadius,
                interactionFilter,
                detectedColliders);

        // Lưu mục tiêu hợp lệ gần nhất đã tìm thấy.
        //
        // Giá trị ban đầu là null vì chưa kiểm tra Collider nào.
        IInteractable nearestInteractable = null;

        // Lưu bình phương khoảng cách nhỏ nhất hiện tại.
        //
        // PositiveInfinity bảo đảm Collider hợp lệ đầu tiên
        // luôn có khoảng cách nhỏ hơn giá trị ban đầu.
        float nearestSqrDistance =
            float.PositiveInfinity;

        // Chỉ duyệt qua những phần tử đã được Physics2D ghi kết quả.
        //
        // Không duyệt toàn bộ mảng vì các vị trí còn lại
        // có thể chứa null hoặc dữ liệu từ truy vấn trước.
        for (int index = 0;
             index < detectedCount;
             index++)
        {
            // Lấy Collider2D hiện tại từ mảng kết quả.
            Collider2D detectedCollider =
                detectedColliders[index];

            // Tìm component triển khai IInteractable trên chính GameObject
            // chứa Collider hoặc trên các GameObject cha.
            //
            // GetComponentInParent được dùng vì Collider có thể nằm
            // trên GameObject con trong khi script tương tác nằm ở cha.
            //
            // Ví dụ:
            //
            // Chest
            // ├── Sprite
            // └── InteractionCollider
            //
            // InteractionCollider chứa BoxCollider2D,
            // còn Chest chứa script triển khai IInteractable.
            IInteractable interactable =
                detectedCollider
                    .GetComponentInParent<IInteractable>();

            // Collider thuộc Layer Interactable nhưng không có component
            // nào triển khai IInteractable thì không phải mục tiêu hợp lệ.
            if (interactable == null)
            {
                // Không ghi Warning tại đây vì phương thức chạy mỗi frame.
                //
                // Nếu một Collider bị cấu hình sai, việc ghi Warning
                // trong mỗi frame có thể tạo hàng trăm thông báo mỗi giây,
                // làm Console bị spam và ảnh hưởng hiệu năng Editor.
                continue;
            }

            // Một IInteractable có thể đồng thời là MonoBehaviour
            // hoặc một loại Behaviour khác.
            //
            // Nếu Behaviour đó đang bị tắt hoặc GameObject không hoạt động,
            // đối tượng không được phép trở thành mục tiêu tương tác.
            if (interactable is Behaviour behaviour &&
                !behaviour.isActiveAndEnabled)
            {
                continue;
            }

            // Tìm điểm trên Collider gần InteractionPoint nhất.
            //
            // Cách tính này chính xác hơn dùng transform.position
            // vì Pivot của GameObject có thể không nằm giữa Collider.
            Vector2 closestPoint =
                detectedCollider.ClosestPoint(
                    interactionPosition);

            // Tính bình phương khoảng cách từ InteractionPoint
            // đến điểm gần nhất trên Collider.
            //
            // Không cần căn bậc hai vì hệ thống chỉ cần so sánh
            // xem khoảng cách nào nhỏ hơn.
            float sqrDistance =
                (closestPoint - interactionPosition)
                .sqrMagnitude;

            // Nếu Collider hiện tại xa hơn hoặc bằng mục tiêu tốt nhất
            // đã tìm thấy thì giữ nguyên kết quả cũ.
            if (sqrDistance >= nearestSqrDistance)
            {
                continue;
            }

            // Collider hiện tại gần hơn tất cả Collider đã kiểm tra trước đó.
            // Cập nhật khoảng cách nhỏ nhất.
            nearestSqrDistance = sqrDistance;

            // Lưu IInteractable hiện tại làm mục tiêu gần nhất mới.
            nearestInteractable = interactable;
        }

        // Trả về mục tiêu gần nhất sau khi duyệt xong các Collider.
        //
        // Nếu không có mục tiêu hợp lệ, giá trị vẫn là null.
        return nearestInteractable;
    }

    /// <summary>
    /// Vẽ vùng phát hiện tương tác trong cửa sổ Scene của Unity.
    /// </summary>
    /// <remarks>
    /// Gizmo chỉ hiển thị khi GameObject chứa PlayerInteraction được chọn.
    ///
    /// Đường tròn màu vàng thể hiện:
    ///
    /// <list type="bullet">
    /// <item>Tâm truy vấn là vị trí của InteractionPoint.</item>
    /// <item>Bán kính truy vấn là interactionRadius.</item>
    /// </list>
    ///
    /// Gizmo chỉ phục vụ kiểm tra trong Unity Editor
    /// và không xuất hiện trong bản build của game.
    /// </remarks>
    private void OnDrawGizmosSelected()
    {
        // Không thể vẽ vùng kiểm tra nếu InteractionPoint chưa được gán.
        if (interactionPoint == null)
        {
            return;
        }

        // Sử dụng màu vàng để vùng tương tác dễ quan sát trong Scene View.
        Gizmos.color = Color.yellow;

        // Vẽ đường tròn có cùng tâm và bán kính
        // với truy vấn vật lý được thực hiện trong game.
        Gizmos.DrawWireSphere(
            interactionPoint.position,
            interactionRadius);
    }
}