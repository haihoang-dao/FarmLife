using UnityEngine;

/// <summary>
/// Công cụ kiểm thử thủ công cho hệ thống Inventory.
///
/// Component này chỉ được sử dụng trong quá trình phát triển.
/// Nó không chứa logic gameplay chính thức và không phải Inventory UI.
/// </summary>
[DisallowMultipleComponent]
public sealed class InventoryTester : MonoBehaviour
{
    /// <summary>
    /// PlayerInventory cần được kiểm thử.
    /// </summary>
    [Header("References")]
    [SerializeField]
    private PlayerInventory playerInventory;

    /// <summary>
    /// Item được sử dụng trong lần kiểm thử hiện tại.
    /// Có thể sử dụng Item stack hoặc không stack.
    /// </summary>
    [SerializeField]
    private ItemData testItem;

    /// <summary>
    /// Số lượng Item sẽ được yêu cầu thêm.
    /// </summary>
    [Header("Test Settings")]
    [SerializeField, Min(1)]
    private int addAmount = 150;

    /// <summary>
    /// Số lượng Item sẽ được yêu cầu xóa.
    /// </summary>
    [SerializeField, Min(1)]
    private int removeAmount = 60;

    /// <summary>
    /// Số lần sự kiện Inventory.Changed được phát ra
    /// trong quá trình kiểm thử.
    /// </summary>
    private int changedEventCount;

    /// <summary>
    /// Xác định Tester đã đăng ký sự kiện Changed hay chưa.
    /// </summary>
    private bool isSubscribed;

    /// <summary>
    /// Dùng khi chuẩn bị dữ liệu kiểm thử.
    /// Sự kiện phát ra lúc xóa dữ liệu cũ sẽ không được tính.
    /// </summary>
    private bool isPreparingTest;

    /// <summary>
    /// Đăng ký theo dõi Inventory khi Component được bật.
    /// </summary>
    private void OnEnable()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        SubscribeToInventory();
    }

    /// <summary>
    /// Hủy đăng ký sự kiện khi Component bị tắt.
    /// </summary>
    private void OnDisable()
    {
        UnsubscribeFromInventory();
    }

    /// <summary>
    /// Bảo vệ các giá trị kiểm thử không nhỏ hơn 1.
    /// </summary>
    private void OnValidate()
    {
        addAmount = Mathf.Max(1, addAmount);
        removeAmount = Mathf.Max(1, removeAmount);
    }

    /// <summary>
    /// Xóa toàn bộ Item đang có trong Inventory kiểm thử.
    /// </summary>
    [ContextMenu("Clear Test Inventory")]
    private void ClearTestInventory()
    {
        if (!CanRunTest())
        {
            return;
        }

        int countBefore = playerInventory.GetItemCount(testItem);

        playerInventory.Clear();

        int countAfter = playerInventory.GetItemCount(testItem);

        Debug.Log(
            $"[InventoryTester] Inventory cleared.\n" +
            $"Test Item Count Before: {countBefore}\n" +
            $"Test Item Count After: {countAfter}",
            this);
    }

    /// <summary>
    /// Kiểm tra CanAddItem mà không thay đổi Inventory.
    /// </summary>
    [ContextMenu("Check Can Add Without Changes")]
    private void CheckCanAddWithoutChanges()
    {
        if (!CanRunTest())
        {
            return;
        }

        int countBefore = playerInventory.GetItemCount(testItem);
        int eventCountBefore = changedEventCount;

        bool canAddEntireAmount =
            playerInventory.CanAddItem(testItem, addAmount);

        int countAfter = playerInventory.GetItemCount(testItem);
        bool changedEventRaised =
            changedEventCount != eventCountBefore;

        Debug.Log(
            $"[InventoryTester] CanAddItem check completed.\n" +
            $"Requested Amount: {addAmount}\n" +
            $"Can Add Entire Amount: {canAddEntireAmount}\n" +
            $"Count Before: {countBefore}\n" +
            $"Count After: {countAfter}\n" +
            $"Changed Event Raised: {changedEventRaised}",
            this);
    }

    /// <summary>
    /// Thêm số lượng Item đã thiết lập vào Inventory.
    /// </summary>
    [ContextMenu("Add Test Item")]
    private void AddTestItem()
    {
        if (!CanRunTest())
        {
            return;
        }

        int addedAmount =
            playerInventory.AddItem(testItem, addAmount);

        int totalAmount =
            playerInventory.GetItemCount(testItem);

        Debug.Log(
            $"[InventoryTester] AddItem completed.\n" +
            $"Requested: {addAmount}\n" +
            $"Actually Added: {addedAmount}\n" +
            $"Total In Inventory: {totalAmount}",
            this);
    }

    /// <summary>
    /// Xóa số lượng Item đã thiết lập khỏi Inventory.
    /// </summary>
    [ContextMenu("Remove Test Item")]
    private void RemoveTestItem()
    {
        if (!CanRunTest())
        {
            return;
        }

        int removedAmount =
            playerInventory.RemoveItem(testItem, removeAmount);

        int remainingAmount =
            playerInventory.GetItemCount(testItem);

        Debug.Log(
            $"[InventoryTester] RemoveItem completed.\n" +
            $"Requested: {removeAmount}\n" +
            $"Actually Removed: {removedAmount}\n" +
            $"Remaining: {remainingAmount}",
            this);
    }

    /// <summary>
    /// Chạy toàn bộ quy trình kiểm thử:
    /// xóa dữ liệu cũ, kiểm tra sức chứa, thêm Item,
    /// xóa Item và kiểm tra sự kiện Changed.
    /// </summary>
    [ContextMenu("Run Full Inventory Test")]
    private void RunFullInventoryTest()
    {
        if (!CanRunTest())
        {
            return;
        }

        // Chuẩn bị một Inventory trống.
        // Sự kiện phát ra ở bước chuẩn bị không được tính vào bài test.
        isPreparingTest = true;
        playerInventory.Clear();
        isPreparingTest = false;

        changedEventCount = 0;

        bool canAddEntireAmount =
            playerInventory.CanAddItem(testItem, addAmount);

        int addedAmount =
            playerInventory.AddItem(testItem, addAmount);

        int countAfterAdd =
            playerInventory.GetItemCount(testItem);

        bool containedRemoveAmount =
            playerInventory.Contains(testItem, removeAmount);

        int removedAmount =
            playerInventory.RemoveItem(testItem, removeAmount);

        int countAfterRemove =
            playerInventory.GetItemCount(testItem);

        int expectedRemovedAmount =
            Mathf.Min(removeAmount, addedAmount);

        int expectedEventCount = 0;

        if (addedAmount > 0)
        {
            expectedEventCount++;
        }

        if (removedAmount > 0)
        {
            expectedEventCount++;
        }

        bool addCountCorrect =
            countAfterAdd == addedAmount;

        bool removeCountCorrect =
            removedAmount == expectedRemovedAmount &&
            countAfterRemove == addedAmount - removedAmount;

        bool capacityResultCorrect =
            canAddEntireAmount == (addedAmount == addAmount);

        bool eventCountCorrect =
            changedEventCount == expectedEventCount;

        bool testPassed =
            addCountCorrect &&
            removeCountCorrect &&
            capacityResultCorrect &&
            eventCountCorrect;

        string resultMessage =
            $"[InventoryTester] Full test: " +
            $"{(testPassed ? "PASS" : "FAIL")}\n" +
            $"Can Add Entire Amount: {canAddEntireAmount}\n" +
            $"Requested Add: {addAmount}\n" +
            $"Actually Added: {addedAmount}\n" +
            $"Count After Add: {countAfterAdd}\n" +
            $"Contained Remove Amount: {containedRemoveAmount}\n" +
            $"Requested Remove: {removeAmount}\n" +
            $"Actually Removed: {removedAmount}\n" +
            $"Count After Remove: {countAfterRemove}\n" +
            $"Changed Events: {changedEventCount}\n" +
            $"Expected Changed Events: {expectedEventCount}";

        if (testPassed)
        {
            Debug.Log(resultMessage, this);
        }
        else
        {
            Debug.LogError(resultMessage, this);
        }
    }

    /// <summary>
    /// Nhận thông báo mỗi khi Inventory thực sự thay đổi.
    /// </summary>
    private void HandleInventoryChanged()
    {
        if (isPreparingTest)
        {
            return;
        }

        changedEventCount++;

        Debug.Log(
            $"[InventoryTester] Inventory Changed event " +
            $"#{changedEventCount}",
            this);
    }

    /// <summary>
    /// Đăng ký nhận sự kiện Changed từ Inventory.
    /// </summary>
    private void SubscribeToInventory()
    {
        if (isSubscribed || playerInventory == null)
        {
            return;
        }

        playerInventory.Inventory.Changed +=
            HandleInventoryChanged;

        isSubscribed = true;
    }

    /// <summary>
    /// Hủy đăng ký sự kiện Changed để tránh đăng ký lặp
    /// hoặc giữ tham chiếu tới Component đã bị tắt.
    /// </summary>
    private void UnsubscribeFromInventory()
    {
        if (!isSubscribed || playerInventory == null)
        {
            return;
        }

        playerInventory.Inventory.Changed -=
            HandleInventoryChanged;

        isSubscribed = false;
    }

    /// <summary>
    /// Kiểm tra các điều kiện bắt buộc trước khi chạy test.
    /// </summary>
    private bool CanRunTest()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning(
                "[InventoryTester] Chỉ chạy kiểm thử trong Play Mode.",
                this);

            return false;
        }

        if (playerInventory == null)
        {
            Debug.LogError(
                "[InventoryTester] Chưa gán Player Inventory.",
                this);

            return false;
        }

        if (testItem == null)
        {
            Debug.LogError(
                "[InventoryTester] Chưa gán Test Item.",
                this);

            return false;
        }

        SubscribeToInventory();
        return true;
    }
}