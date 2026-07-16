using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Đọc dữ liệu điều khiển Player từ Unity Input System
/// và cung cấp dữ liệu đó cho các hệ thống gameplay khác.
/// </summary>
public class PlayerInputReader : MonoBehaviour
{
    [Header("Debug")]

    [Tooltip("Bật để hiển thị thông báo khi nhận input tương tác.")]
    [SerializeField] private bool showDebugMessages;

    // Đối tượng quản lý toàn bộ Input Actions của game.
    private FarmInput farmInput;

    /// <summary>
    /// Hướng di chuyển hiện tại của Player.
    /// Giá trị đã được chuẩn hóa để tốc độ đi chéo không nhanh hơn đi thẳng.
    /// </summary>
    public Vector2 MoveInput { get; private set; }

    /// <summary>
    /// Sự kiện được phát một lần khi Player thực hiện action Interact.
    /// </summary>
    public event Action InteractPressed;

    /// <summary>
    /// Khởi tạo lớp FarmInput được Unity tự động sinh.
    /// </summary>
    private void Awake()
    {
        farmInput = new FarmInput();
    }

    /// <summary>
    /// Đăng ký nhận sự kiện Interact và bật Action Map Player.
    /// </summary>
    private void OnEnable()
    {
        if (farmInput == null)
        {
            return;
        }

        // Đăng ký callback trước khi bật Action Map.
        farmInput.Player.Interact.performed += OnInteractPerformed;

        // Bật toàn bộ action thuộc Action Map Player.
        farmInput.Player.Enable();
    }

    /// <summary>
    /// Hủy đăng ký sự kiện, tắt Action Map và xóa dữ liệu di chuyển.
    /// </summary>
    private void OnDisable()
    {
        if (farmInput != null)
        {
            // Hủy đăng ký để tránh callback bị gọi nhiều lần
            // khi GameObject được Disable rồi Enable lại.
            farmInput.Player.Interact.performed -= OnInteractPerformed;

            // Tắt toàn bộ action thuộc Action Map Player.
            farmInput.Player.Disable();
        }

        // Đưa hướng di chuyển về 0 để Player không tiếp tục trượt.
        MoveInput = Vector2.zero;
    }

    /// <summary>
    /// Đọc giá trị action Move trong mỗi frame.
    /// </summary>
    private void Update()
    {
        if (farmInput == null)
        {
            MoveInput = Vector2.zero;
            return;
        }

        // Đọc hướng di chuyển từ action Move.
        MoveInput = farmInput.Player.Move.ReadValue<Vector2>();

        // Chuẩn hóa để đi chéo không nhanh hơn đi thẳng.
        MoveInput = MoveInput.normalized;
    }

    /// <summary>
    /// Được Unity Input System gọi khi action Interact chuyển sang trạng thái performed.
    /// </summary>
    /// <param name="context">
    /// Thông tin ngữ cảnh của action Interact tại thời điểm được thực hiện.
    /// </param>
    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (showDebugMessages)
        {
            Debug.Log(
                "PlayerInputReader: Đã nhận input Interact.",
                this
            );
        }

        // Thông báo cho các hệ thống đang lắng nghe rằng Player đã nhấn Interact.
        InteractPressed?.Invoke();
    }

    /// <summary>
    /// Giải phóng tài nguyên của FarmInput khi Player bị hủy.
    /// </summary>
    private void OnDestroy()
    {
        farmInput?.Dispose();
    }
}