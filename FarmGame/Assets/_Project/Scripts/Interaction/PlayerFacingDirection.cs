using UnityEngine;

/// <summary>
/// Quản lý hướng nhìn bốn chiều của Player
/// và đặt InteractionPoint ở phía trước Player.
/// </summary>
[RequireComponent(typeof(PlayerInputReader))]
public class PlayerFacingDirection : MonoBehaviour
{
    /// <summary>
    /// Transform dùng làm tâm của vùng tương tác.
    /// </summary>
    [Header("Interaction Point")]
    [SerializeField] private Transform interactionPoint;

    /// <summary>
    /// Khoảng cách từ tâm Player đến InteractionPoint.
    /// </summary>
    [SerializeField, Min(0f)] private float interactionDistance = 0.65f;

    /// <summary>
    /// Component cung cấp hướng di chuyển hiện tại của Player.
    /// </summary>
    private PlayerInputReader inputReader;

    /// <summary>
    /// Hướng nhìn bốn chiều hiện tại của Player.
    /// Giá trị mặc định là hướng xuống.
    /// </summary>
    public Vector2 FacingDirection { get; private set; } = Vector2.down;

    /// <summary>
    /// Lấy các component cần thiết và thiết lập
    /// vị trí mặc định của InteractionPoint.
    /// </summary>
    private void Awake()
    {
        inputReader = GetComponent<PlayerInputReader>();

        if (interactionPoint == null)
        {
            Debug.LogError(
                $"{nameof(PlayerFacingDirection)} trên {name} chưa được gán InteractionPoint.",
                this);

            enabled = false;
            return;
        }

        UpdateInteractionPoint();
    }

    /// <summary>
    /// Cập nhật hướng nhìn sau khi PlayerInputReader
    /// đã đọc input của frame hiện tại.
    /// </summary>
    private void LateUpdate()
    {
        Vector2 moveInput = inputReader.MoveInput;

        if (moveInput.sqrMagnitude <= 0.01f)
        {
            return;
        }

        FacingDirection = GetCardinalDirection(moveInput);
        UpdateInteractionPoint();
    }

    /// <summary>
    /// Chuyển hướng input thành một trong bốn hướng chính:
    /// lên, xuống, trái hoặc phải.
    /// </summary>
    /// <param name="direction">Hướng input hiện tại.</param>
    /// <returns>Hướng bốn chiều đã được chuẩn hóa.</returns>
    private static Vector2 GetCardinalDirection(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return direction.x > 0f
                ? Vector2.right
                : Vector2.left;
        }

        return direction.y > 0f
            ? Vector2.up
            : Vector2.down;
    }

    /// <summary>
    /// Đưa InteractionPoint đến phía trước Player
    /// theo hướng nhìn hiện tại.
    /// </summary>
    private void UpdateInteractionPoint()
    {
        interactionPoint.localPosition =
            FacingDirection * interactionDistance;
    }
}