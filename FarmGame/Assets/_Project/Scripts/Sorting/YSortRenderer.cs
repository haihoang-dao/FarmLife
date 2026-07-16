using UnityEngine;

// Lớp YSortRenderer chịu trách nhiệm cập nhật thứ tự hiển thị
// của SpriteRenderer dựa trên vị trí Y của một điểm sắp xếp.
[RequireComponent(typeof(SpriteRenderer))]
public class YSortRenderer : MonoBehaviour
{
    // Nhóm thiết lập Y-Sorting.
    [Header("Y-Sorting Settings")]

    // Điểm dùng để tính vị trí Y.
    // Nếu không gán, script dùng Transform hiện tại.
    [SerializeField] private Transform sortPoint;

    // Hệ số nhân để chuyển vị trí Y thành Order in Layer.
    [SerializeField] private int sortingMultiplier = 100;

    // Giá trị cộng thêm để điều chỉnh thứ tự thủ công.
    [SerializeField] private int sortingOffset = 0;

    // Tham chiếu đến SpriteRenderer cần sắp xếp.
    private SpriteRenderer targetSpriteRenderer;

    // Order gần nhất đã được gán.
    private int lastSortingOrder = int.MinValue;

    // Awake được gọi khi GameObject được khởi tạo.
    private void Awake()
    {
        // Lấy SpriteRenderer trên GameObject hiện tại.
        targetSpriteRenderer = GetComponent<SpriteRenderer>();

        // Nếu chưa gán Sort Point, dùng Transform hiện tại.
        if (sortPoint == null)
        {
            sortPoint = transform;
        }

        // Cập nhật Order ngay khi khởi tạo.
        UpdateSortingOrder();
    }

    // LateUpdate chạy sau khi đối tượng hoàn tất di chuyển trong frame.
    private void LateUpdate()
    {
        // Cập nhật thứ tự hiển thị.
        UpdateSortingOrder();
    }

    // Tính Sorting Order dựa trên tọa độ Y của Sort Point.
    private void UpdateSortingOrder()
    {
        // Tính Order mới.
        int calculatedSortingOrder =
            Mathf.RoundToInt(-sortPoint.position.y * sortingMultiplier)
            + sortingOffset;

        // Không gán lại nếu không có thay đổi.
        if (calculatedSortingOrder == lastSortingOrder)
        {
            return;
        }

        // Lưu Order mới.
        lastSortingOrder = calculatedSortingOrder;

        // Gán vào SpriteRenderer.
        targetSpriteRenderer.sortingOrder = calculatedSortingOrder;
    }
}