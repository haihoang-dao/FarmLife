using UnityEngine;

// Lớp PlayerInputReader chịu trách nhiệm đọc dữ liệu
// điều khiển người chơi từ Unity Input System.
public class PlayerInputReader : MonoBehaviour
{
    // Đối tượng quản lý toàn bộ hành động đầu vào.
    private FarmInput farmInput;

    // Hướng di chuyển hiện tại của người chơi.
    public Vector2 MoveInput { get; private set; }

    // Phương thức Awake được gọi khi đối tượng được khởi tạo.
    private void Awake()
    {
        // Khởi tạo lớp Input được Unity tự động sinh.
        farmInput = new FarmInput();
    }

    // Phương thức OnEnable được gọi khi GameObject được kích hoạt.
    private void OnEnable()
    {
        // Kiểm tra hệ thống Input đã được khởi tạo.
        if (farmInput != null)
        {
            // Bật nhóm hành động Player.
            farmInput.Player.Enable();
        }
    }

    // Phương thức OnDisable được gọi khi GameObject bị vô hiệu hóa.
    private void OnDisable()
    {
        // Kiểm tra hệ thống Input đã được khởi tạo.
        if (farmInput != null)
        {
            // Tắt nhóm hành động Player.
            farmInput.Player.Disable();
        }

        // Đưa hướng di chuyển về 0 khi ngừng nhận Input.
        MoveInput = Vector2.zero;
    }

    // Phương thức Update được gọi ở mỗi khung hình.
    private void Update()
    {
        // Đọc hướng di chuyển từ Action Move.
        MoveInput = farmInput.Player.Move.ReadValue<Vector2>();

        // Chuẩn hóa để đi chéo không nhanh hơn đi thẳng.
        MoveInput = MoveInput.normalized;
    }
}