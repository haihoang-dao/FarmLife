using UnityEngine;

/// <summary>
/// Đại diện cho một vật phẩm có thể nhặt trong thế giới.
///
/// ItemPickup sử dụng lại IInteractable.
/// Khi Player tương tác, Component sẽ yêu cầu PlayerInventory
/// thêm Item vào túi đồ.
///
/// ItemPickup chỉ bị hủy khi toàn bộ số lượng Item đã được thêm.
/// Nếu Inventory chỉ còn một phần sức chứa, số lượng còn lại
/// tiếp tục tồn tại trên ItemPickup.
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public sealed class ItemPickup : MonoBehaviour, IInteractable
{
    /// <summary>
    /// Dữ liệu cố định của Item mà đối tượng này đại diện.
    ///
    /// ItemData chứa ID, tên, icon, loại Item,
    /// khả năng stack và kích thước stack tối đa.
    /// </summary>
    [Header("Item Settings")]
    [SerializeField]
    private ItemData itemData;

    /// <summary>
    /// Số lượng Item hiện còn trong đối tượng Pickup.
    ///
    /// Giá trị này có thể giảm trong Play Mode nếu Inventory
    /// chỉ chứa được một phần số lượng Item.
    /// </summary>
    [SerializeField, Min(1)]
    private int quantity = 1;

    /// <summary>
    /// Inventory của Player sẽ nhận Item.
    ///
    /// Có thể để None trên Prefab. Khi vào Play Mode,
    /// ItemPickup sẽ tự tìm PlayerInventory trong Scene.
    /// </summary>
    [Header("Runtime Reference")]
    [SerializeField]
    private PlayerInventory playerInventory;

    /// <summary>
    /// SpriteRenderer dùng để hiển thị icon của ItemData.
    /// </summary>
    private SpriteRenderer itemRenderer;

    /// <summary>
    /// Collider được PlayerInteraction phát hiện.
    /// Collider này phải là Trigger để không chặn Player.
    /// </summary>
    private BoxCollider2D pickupCollider;

    /// <summary>
    /// Ngăn việc nhặt cùng một đối tượng nhiều lần
    /// trong khoảng thời gian chờ Unity hủy GameObject.
    /// </summary>
    private bool isCollected;

    /// <summary>
    /// Cho phép hệ thống khác đọc ItemData,
    /// nhưng không được thay đổi trực tiếp.
    /// </summary>
    public ItemData ItemData => itemData;

    /// <summary>
    /// Số lượng Item hiện còn trong Pickup.
    /// </summary>
    public int Quantity => quantity;

    /// <summary>
    /// Lấy các Component bắt buộc, cập nhật icon
    /// và tìm PlayerInventory khi GameObject được khởi tạo.
    /// </summary>
    private void Awake()
    {
        CacheRequiredComponents();
        ApplyItemIcon();
        TryResolvePlayerInventory();

        if (itemData == null)
        {
            Debug.LogError(
                $"{nameof(ItemPickup)} trên {name} chưa được gán Item Data.",
                this);
        }

        if (pickupCollider != null && !pickupCollider.isTrigger)
        {
            Debug.LogWarning(
                $"{nameof(BoxCollider2D)} trên {name} chưa bật Is Trigger. " +
                "Vật phẩm có thể chặn đường Player.",
                this);
        }
    }

    /// <summary>
    /// Được Unity gọi khi Component vừa được thêm
    /// hoặc khi người dùng chọn Reset Component.
    /// </summary>
    private void Reset()
    {
        CacheRequiredComponents();

        if (pickupCollider != null)
        {
            pickupCollider.isTrigger = true;
        }

        ApplyItemIcon();
    }

    /// <summary>
    /// Kiểm tra dữ liệu mỗi khi có giá trị thay đổi
    /// trong Unity Inspector.
    /// </summary>
    private void OnValidate()
    {
        quantity = Mathf.Max(1, quantity);

        CacheRequiredComponents();
        ApplyItemIcon();
    }

    /// <summary>
    /// Khởi tạo ItemPickup bằng code.
    ///
    /// Phương thức này sẽ được dùng trong các phase sau
    /// khi Crop, Chest hoặc hệ thống khác tạo ItemPickup.
    /// </summary>
    /// <param name="newItemData">
    /// ItemData mà Pickup mới sẽ đại diện.
    /// </param>
    /// <param name="newQuantity">
    /// Số lượng Item cần chứa, tối thiểu là 1.
    /// </param>
    public void Initialize(
        ItemData newItemData,
        int newQuantity)
    {
        if (newItemData == null)
        {
            Debug.LogError(
                $"Không thể khởi tạo {nameof(ItemPickup)} " +
                "với ItemData bằng null.",
                this);

            return;
        }

        itemData = newItemData;
        quantity = Mathf.Max(1, newQuantity);

        ApplyItemIcon();
    }

    /// <summary>
    /// Được PlayerInteraction gọi khi Player nhấn phím E.
    ///
    /// Phương thức yêu cầu PlayerInventory thêm số lượng Item
    /// hiện có và xử lý kết quả thực tế do AddItem trả về.
    /// </summary>
    public void Interact()
    {
        if (isCollected)
        {
            return;
        }

        if (itemData == null)
        {
            Debug.LogError(
                $"Không thể nhặt {name} vì chưa có Item Data.",
                this);

            return;
        }

        if (!TryResolvePlayerInventory())
        {
            Debug.LogError(
                $"Không thể nhặt {itemData.DisplayName} vì Scene " +
                $"không tìm thấy {nameof(PlayerInventory)}.",
                this);

            return;
        }

        int requestedAmount = quantity;

        int addedAmount =
            playerInventory.AddItem(itemData, requestedAmount);

        if (addedAmount <= 0)
        {
            Debug.LogWarning(
                $"Không thể nhặt {itemData.DisplayName}. " +
                $"Inventory không còn đủ chỗ.",
                this);

            return;
        }

        quantity = Mathf.Max(0, quantity - addedAmount);

        Debug.Log(
            $"Đã nhặt {addedAmount} × {itemData.DisplayName}. " +
            $"Số lượng còn lại trên mặt đất: {quantity}.",
            this);

        if (quantity > 0)
        {
            Debug.LogWarning(
                $"Inventory chỉ chứa được {addedAmount}/{requestedAmount} " +
                $"{itemData.DisplayName}. Phần còn lại vẫn ở trên mặt đất.",
                this);

            return;
        }

        CompletePickup();
    }

    /// <summary>
    /// Lưu tham chiếu tới các Component bắt buộc
    /// để không phải gọi GetComponent nhiều lần.
    /// </summary>
    private void CacheRequiredComponents()
    {
        if (itemRenderer == null)
        {
            itemRenderer = GetComponent<SpriteRenderer>();
        }

        if (pickupCollider == null)
        {
            pickupCollider = GetComponent<BoxCollider2D>();
        }
    }

    /// <summary>
    /// Dùng icon trong ItemData làm hình ảnh của ItemPickup.
    /// </summary>
    private void ApplyItemIcon()
    {
        if (itemRenderer == null)
        {
            return;
        }

        if (itemData == null || itemData.Icon == null)
        {
            return;
        }

        itemRenderer.sprite = itemData.Icon;
    }

    /// <summary>
    /// Tìm PlayerInventory trong Scene nếu chưa được gán.
    ///
    /// Việc tìm kiếm chỉ cần thực hiện khi chưa có reference.
    /// Dự án hiện là Single Player nên Scene chỉ có một
    /// PlayerInventory hợp lệ.
    /// </summary>
    /// <returns>
    /// True nếu đã tìm thấy PlayerInventory; ngược lại là false.
    /// </returns>
    private bool TryResolvePlayerInventory()
    {
        if (playerInventory != null)
        {
            return true;
        }

        playerInventory =
            FindFirstObjectByType<PlayerInventory>();

        return playerInventory != null;
    }

    /// <summary>
    /// Hoàn tất việc nhặt Item.
    ///
    /// Renderer và Collider được tắt ngay lập tức để Item không
    /// tiếp tục hiển thị hoặc bị tương tác lần nữa trong frame hiện tại.
    /// GameObject sau đó được Unity hủy vào cuối frame.
    /// </summary>
    private void CompletePickup()
    {
        isCollected = true;

        if (pickupCollider != null)
        {
            pickupCollider.enabled = false;
        }

        if (itemRenderer != null)
        {
            itemRenderer.enabled = false;
        }

        Destroy(gameObject);
    }
}