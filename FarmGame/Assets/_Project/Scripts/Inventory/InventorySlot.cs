using System;
using UnityEngine;

/// <summary>
/// Đại diện cho một ô dữ liệu trong Inventory.
///
/// Mỗi slot chỉ có thể chứa một loại ItemData và số lượng
/// không được vượt quá MaxStackSize của Item đó.
/// </summary>
[Serializable]
public sealed class InventorySlot
{
    /// <summary>
    /// Dữ liệu cố định của loại vật phẩm đang nằm trong slot.
    ///
    /// Giá trị bằng null khi slot đang trống.
    /// </summary>
    [SerializeField]
    private ItemData itemData;

    /// <summary>
    /// Số lượng vật phẩm hiện có trong slot.
    ///
    /// Giá trị hợp lệ luôn lớn hơn hoặc bằng 0.
    /// </summary>
    [SerializeField, Min(0)]
    private int quantity;

    /// <summary>
    /// Cho phép hệ thống bên ngoài đọc ItemData trong slot
    /// nhưng không được tự ý thay đổi trực tiếp.
    /// </summary>
    public ItemData ItemData => itemData;

    /// <summary>
    /// Cho phép đọc số lượng hiện tại nhưng không cho
    /// code bên ngoài tự ý sửa số lượng.
    /// </summary>
    public int Quantity => quantity;

    /// <summary>
    /// Slot được xem là trống khi không có ItemData
    /// hoặc số lượng không lớn hơn 0.
    /// </summary>
    public bool IsEmpty => itemData == null || quantity <= 0;

    /// <summary>
    /// Số lượng còn có thể thêm vào stack hiện tại.
    ///
    /// Slot trống trả về 0 vì chưa có ItemData để xác định
    /// MaxStackSize. Sức chứa của slot trống sẽ được xác định
    /// khi một ItemData cụ thể được thêm vào.
    /// </summary>
    public int RemainingCapacity =>
        IsEmpty
            ? 0
            : Mathf.Max(0, itemData.MaxStackSize - quantity);

    /// <summary>
    /// Khởi tạo một InventorySlot trống.
    /// </summary>
    public InventorySlot()
    {
        Clear();
    }

    /// <summary>
    /// Kiểm tra Item được cung cấp có thể thêm vào slot hay không.
    /// </summary>
    /// <param name="candidateItem">
    /// ItemData đang muốn thêm vào slot.
    /// </param>
    /// <returns>
    /// Trả về true nếu slot trống hoặc đang chứa cùng ItemData
    /// và vẫn còn chỗ trong stack.
    /// </returns>
    public bool CanAccept(ItemData candidateItem)
    {
        if (candidateItem == null)
        {
            return false;
        }

        if (IsEmpty)
        {
            return true;
        }

        return itemData == candidateItem
            && quantity < itemData.MaxStackSize;
    }

    /// <summary>
    /// Thêm một lượng Item vào slot.
    ///
    /// Phương thức không cho phép vượt MaxStackSize và không
    /// cho phép thêm Item khác loại vào slot đang có dữ liệu.
    /// </summary>
    /// <param name="candidateItem">
    /// ItemData cần thêm.
    /// </param>
    /// <param name="amount">
    /// Số lượng muốn thêm.
    /// </param>
    /// <returns>
    /// Số lượng thực tế đã được thêm vào slot.
    /// Trả về 0 nếu không thể thêm.
    /// </returns>
    public int AddQuantity(ItemData candidateItem, int amount)
    {
        if (candidateItem == null || amount <= 0)
        {
            return 0;
        }

        if (!CanAccept(candidateItem))
        {
            return 0;
        }

        if (IsEmpty)
        {
            itemData = candidateItem;
            quantity = 0;
        }

        int availableSpace = Mathf.Max(
            0,
            itemData.MaxStackSize - quantity);

        int addedAmount = Mathf.Min(amount, availableSpace);

        quantity += addedAmount;

        return addedAmount;
    }

    /// <summary>
    /// Xóa một lượng Item khỏi slot.
    ///
    /// Nếu số lượng trở về 0, slot sẽ được làm trống hoàn toàn.
    /// </summary>
    /// <param name="amount">
    /// Số lượng muốn xóa.
    /// </param>
    /// <returns>
    /// Số lượng thực tế đã được xóa khỏi slot.
    /// </returns>
    public int RemoveQuantity(int amount)
    {
        if (IsEmpty || amount <= 0)
        {
            return 0;
        }

        int removedAmount = Mathf.Min(amount, quantity);

        quantity -= removedAmount;

        if (quantity <= 0)
        {
            Clear();
        }

        return removedAmount;
    }

    /// <summary>
    /// Xóa toàn bộ dữ liệu trong slot và đưa slot
    /// về trạng thái trống hợp lệ.
    /// </summary>
    public void Clear()
    {
        itemData = null;
        quantity = 0;
    }
}