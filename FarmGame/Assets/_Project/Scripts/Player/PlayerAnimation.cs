using UnityEngine;

// Lớp PlayerAnimation chịu trách nhiệm điều khiển
// animation và hướng nhìn của nhân vật người chơi.
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerInputReader))]
public class PlayerAnimation : MonoBehaviour
{
    // Tham chiếu đến Animator của người chơi.
    private Animator playerAnimator;

    // Tham chiếu đến SpriteRenderer của người chơi.
    private SpriteRenderer playerSpriteRenderer;

    // Tham chiếu đến script đọc Input.
    private PlayerInputReader inputReader;

    // Hướng nhìn cuối cùng của người chơi.
    // Mặc định người chơi nhìn xuống.
    private Vector2 lastMoveDirection = Vector2.down;

    // Mã định danh parameter MoveX trong Animator.
    private static readonly int MoveXHash =
        Animator.StringToHash("MoveX");

    // Mã định danh parameter MoveY trong Animator.
    private static readonly int MoveYHash =
        Animator.StringToHash("MoveY");

    // Mã định danh parameter Speed trong Animator.
    private static readonly int SpeedHash =
        Animator.StringToHash("Speed");

    // Phương thức Awake được gọi khi đối tượng được khởi tạo.
    private void Awake()
    {
        // Lấy Animator trên Player.
        playerAnimator = GetComponent<Animator>();

        // Lấy SpriteRenderer trên Player.
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        // Lấy PlayerInputReader trên Player.
        inputReader = GetComponent<PlayerInputReader>();
    }

    // LateUpdate được gọi sau các phương thức Update trong frame hiện tại.
    // Dùng LateUpdate để bảo đảm PlayerInputReader đã cập nhật input trước.
    private void LateUpdate()
    {
        // Lấy hướng di chuyển hiện tại từ Input Reader.
        Vector2 moveInput = inputReader.MoveInput;

        // Cập nhật hướng nhìn cuối cùng của người chơi.
        UpdateLastMoveDirection(moveInput);

        // Cập nhật các parameter của Animator.
        UpdateAnimator(moveInput);

        // Cập nhật trạng thái lật Sprite trái hoặc phải.
        UpdateSpriteFlip(moveInput);
    }

    // Phương thức cập nhật hướng nhìn cuối cùng của người chơi.
    private void UpdateLastMoveDirection(Vector2 moveInput)
    {
        // Không cập nhật hướng nếu Player đang đứng yên.
        if (moveInput.sqrMagnitude <= 0.01f)
        {
            return;
        }

        // Chuyển input thành một trong bốn hướng animation chính.
        lastMoveDirection = GetAnimationDirection(moveInput);
    }

    // Phương thức cập nhật các parameter trong Animator.
    private void UpdateAnimator(Vector2 moveInput)
    {
        // Kiểm tra Player hiện có đang di chuyển hay không.
        bool isMoving = moveInput.sqrMagnitude > 0.01f;

        // Khi đang di chuyển, dùng hướng input hiện tại.
        // Khi đứng yên, dùng hướng di chuyển cuối cùng.
        Vector2 animationDirection = isMoving
            ? GetAnimationDirection(moveInput)
            : lastMoveDirection;

        // Gửi hướng ngang vào Blend Tree.
        playerAnimator.SetFloat(
            MoveXHash,
            animationDirection.x
        );

        // Gửi hướng dọc vào Blend Tree.
        playerAnimator.SetFloat(
            MoveYHash,
            animationDirection.y
        );

        // Gửi trạng thái di chuyển vào Animator.
        playerAnimator.SetFloat(
            SpeedHash,
            isMoving ? 1f : 0f
        );
    }

    // Phương thức chuyển input thành một trong bốn hướng chính.
    private Vector2 GetAnimationDirection(Vector2 direction)
    {
        // Nếu độ lớn hướng ngang lớn hơn hướng dọc,
        // sử dụng animation Side.
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Trả về hướng phải hoặc trái tùy theo input.
            return direction.x > 0f
                ? Vector2.right
                : Vector2.left;
        }

        // Nếu hướng dọc đang đi lên,
        // sử dụng animation Up.
        if (direction.y > 0f)
        {
            return Vector2.up;
        }

        // Các trường hợp dọc còn lại sử dụng animation Down.
        return Vector2.down;
    }

    // Phương thức cập nhật hướng lật Sprite.
    private void UpdateSpriteFlip(Vector2 moveInput)
    {
        // Không thay đổi Flip X nếu Player không đi ngang.
        if (Mathf.Abs(moveInput.x) <= 0.01f)
        {
            return;
        }

        // Lật Sprite khi người chơi đi sang trái.
        playerSpriteRenderer.flipX = moveInput.x < 0f;
    }
}