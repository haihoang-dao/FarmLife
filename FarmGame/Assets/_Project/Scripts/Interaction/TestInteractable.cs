using UnityEngine;

/// <summary>
/// Đối tượng tương tác thử nghiệm dùng để kiểm tra hệ thống tương tác của Player.
/// </summary>
/// <remarks>
/// Class này triển khai <see cref="IInteractable"/> nên có thể được
/// <see cref="PlayerInteraction"/> nhận diện và gọi phương thức
/// <see cref="Interact"/>.
///
/// Khi tương tác thành công, đối tượng chỉ ghi một thông báo vào Console.
/// Vì vậy, script này phù hợp để kiểm tra hệ thống trước khi tạo các đối tượng
/// tương tác thật như NPC, rương, cửa hoặc biển báo.
///
/// Để được Player phát hiện, GameObject này cần:
/// <list type="bullet">
/// <item>Có một Collider2D.</item>
/// <item>Thuộc Layer Interactable.</item>
/// <item>Nằm trong vùng phát hiện của PlayerInteraction.</item>
/// </list>
///
/// Từ khóa sealed ngăn class khác kế thừa từ class thử nghiệm này.
/// Vì đây chỉ là một đối tượng dùng để kiểm tra nên không cần mở rộng bằng kế thừa.
/// </remarks>
[RequireComponent(typeof(BoxCollider2D))]
public sealed class TestInteractable : MonoBehaviour, IInteractable
{
    /// <summary>
    /// Nội dung thông báo được ghi vào Unity Console
    /// khi Player tương tác thành công với đối tượng.
    /// </summary>
    /// <remarks>
    /// Trường này được serialize để có thể chỉnh sửa trực tiếp trong Inspector
    /// mà không cần thay đổi mã nguồn.
    ///
    /// Mỗi đối tượng thử nghiệm có thể sử dụng một nội dung khác nhau,
    /// giúp xác định chính xác đối tượng nào đã nhận được lệnh tương tác.
    /// </remarks>
    [Header("Test Interaction")]
    [SerializeField]
    private string interactionMessage =
        "Tương tác thành công với đối tượng thử.";

    /// <summary>
    /// Thực hiện hành vi của đối tượng khi Player tương tác.
    /// </summary>
    /// <remarks>
    /// Phương thức này là phần triển khai bắt buộc của
    /// <see cref="IInteractable"/>.
    ///
    /// <see cref="PlayerInteraction"/> sẽ gọi phương thức này khi:
    /// <list type="number">
    /// <item>Player nhấn phím tương tác.</item>
    /// <item>Collider2D của đối tượng nằm trong vùng kiểm tra.</item>
    /// <item>Collider2D thuộc Layer Interactable.</item>
    /// <item>Đối tượng triển khai IInteractable.</item>
    /// </list>
    ///
    /// Hiện tại phương thức chỉ ghi thông báo vào Console để xác nhận
    /// toàn bộ luồng tương tác đang hoạt động chính xác.
    /// </remarks>
    public void Interact()
    {
        // Ghép nội dung được cấu hình trong Inspector với tên GameObject.
        // Tên GameObject giúp xác định chính xác đối tượng nào vừa được tương tác.
        string logMessage =
            $"{interactionMessage} Đối tượng: {name}.";

        // Ghi kết quả tương tác vào Unity Console.
        //
        // Tham số this cung cấp Context Object cho thông báo.
        // Khi nhấn vào dòng log trong Console, Unity sẽ tự động chọn
        // GameObject đang chứa component TestInteractable này.
        Debug.Log(logMessage, this);
    }
}