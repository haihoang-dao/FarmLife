using UnityEngine;

// Lớp PlayerMovement chịu trách nhiệm
// di chuyển nhân vật bằng Rigidbody2D.
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInputReader))]
public class PlayerMovement : MonoBehaviour
{
    // Nhóm thiết lập di chuyển của người chơi.
    [Header("Movement Settings")]

    // Tốc độ di chuyển của người chơi.
    [SerializeField] private float moveSpeed = 10f;

    // Tham chiếu đến Rigidbody2D của người chơi.
    private Rigidbody2D playerRigidbody;

    // Tham chiếu đến script đọc Input.
    private PlayerInputReader inputReader;

    // Phương thức Awake được gọi khi đối tượng được khởi tạo.
    private void Awake()
    {
        // Lấy Rigidbody2D trên Player.
        playerRigidbody = GetComponent<Rigidbody2D>();

        // Lấy PlayerInputReader trên Player.
        inputReader = GetComponent<PlayerInputReader>();
    }

    // Phương thức FixedUpdate được gọi theo chu kỳ vật lý.
    private void FixedUpdate()
    {
        // Gọi phương thức xử lý di chuyển.
        MovePlayer();
    }

    // Phương thức di chuyển người chơi bằng Rigidbody2D.
    private void MovePlayer()
    {
        // Lấy hướng di chuyển từ Input Reader.
        Vector2 moveInput = inputReader.MoveInput;

        // Tính vận tốc dựa trên hướng và tốc độ.
        Vector2 movementVelocity = moveInput * moveSpeed;

        // Gán vận tốc vào Rigidbody2D.
        playerRigidbody.linearVelocity = movementVelocity;
    }

    // Phương thức OnDisable được gọi khi script bị vô hiệu hóa.
    private void OnDisable()
    {
        // Kiểm tra Rigidbody2D đã tồn tại.
        if (playerRigidbody != null)
        {
            // Dừng Player để tránh tiếp tục trượt.
            playerRigidbody.linearVelocity = Vector2.zero;
        }
    }
}