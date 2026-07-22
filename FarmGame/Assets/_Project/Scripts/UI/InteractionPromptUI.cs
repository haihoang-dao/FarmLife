using UnityEngine;

/// <summary>
/// Điều khiển trạng thái hiển thị của thông báo tương tác.
///
/// Component này không tự tìm kiếm đối tượng tương tác.
/// PlayerInteraction sẽ gọi SetVisible để yêu cầu
/// hiển thị hoặc ẩn phần giao diện PromptRoot.
/// </summary>
public sealed class InteractionPromptUI : MonoBehaviour
{
    /// <summary>
    /// GameObject chứa toàn bộ hình nền và nội dung
    /// của thông báo tương tác.
    ///
    /// Chỉ PromptRoot bị bật hoặc tắt.
    /// Component InteractionPromptUI vẫn luôn hoạt động
    /// để có thể nhận yêu cầu hiển thị từ PlayerInteraction.
    /// </summary>
    [Header("Interaction Prompt")]
    [SerializeField]
    private GameObject promptRoot;

    /// <summary>
    /// Ẩn thông báo khi giao diện được khởi tạo,
    /// tránh để prompt xuất hiện trước khi Player
    /// đứng gần một đối tượng tương tác.
    /// </summary>
    private void Awake()
    {
        if (promptRoot == null)
        {
            Debug.LogError(
                $"{nameof(InteractionPromptUI)} trên {name} " +
                "chưa được gán PromptRoot.",
                this);

            return;
        }

        SetVisible(false);
    }

    /// <summary>
    /// Hiển thị hoặc ẩn thông báo tương tác.
    ///
    /// PlayerInteraction gọi method này sau mỗi lần
    /// cập nhật mục tiêu tương tác gần nhất.
    /// </summary>
    /// <param name="shouldBeVisible">
    /// True nếu có mục tiêu hợp lệ và cần hiện prompt;
    /// false nếu không có mục tiêu hoặc cần ẩn prompt.
    /// </param>
    public void SetVisible(bool shouldBeVisible)
    {
        if (promptRoot == null)
        {
            return;
        }

        // Không gọi SetActive lại nếu PromptRoot
        // đã có đúng trạng thái cần thiết.
        if (promptRoot.activeSelf == shouldBeVisible)
        {
            return;
        }

        promptRoot.SetActive(shouldBeVisible);
    }
}