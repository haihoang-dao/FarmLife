using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quản lý toàn bộ dữ liệu của một Inventory.
///
/// Inventory chứa danh sách các InventorySlot và chịu trách nhiệm
/// phân phối Item vào stack hiện có hoặc slot trống.
/// </summary>
[Serializable]
public sealed class Inventory
{
    /// <summary>
    /// Danh sách tất cả slot thuộc Inventory.
    ///
    /// Thứ tự phần tử trong danh sách cũng chính là chỉ số
    /// của slot trong Inventory.
    /// </summary>
    [SerializeField]
    private List<InventorySlot> slots;

    /// <summary>
    /// Cho phép hệ thống bên ngoài đọc danh sách slot,
    /// nhưng không cho phép trực tiếp thêm hoặc xóa phần tử.
    /// </summary>
    public IReadOnlyList<InventorySlot> Slots
    {
        get
        {
            EnsureSlotInstances();
            return slots;
        }
    }

    /// <summary>
    /// Tổng số slot hiện có trong Inventory.
    /// </summary>
    public int Capacity
    {
        get
        {
            EnsureSlotInstances();
            return slots.Count;
        }
    }

    /// <summary>
    /// Được phát ra sau khi dữ liệu Inventory thực sự thay đổi.
    ///
    /// Giao diện Inventory sau này có thể đăng ký sự kiện này
    /// để cập nhật hình ảnh mà không làm Inventory phụ thuộc UI.
    /// </summary>
    public event Action Changed;

    /// <summary>
    /// Tạo một Inventory chưa có slot.
    ///
    /// Constructor này hỗ trợ quá trình tuần tự hóa dữ liệu.
    /// Khi sử dụng trong gameplay, nên truyền số slot thông qua
    /// constructor Inventory(int capacity).
    /// </summary>
    public Inventory()
    {
        slots = new List<InventorySlot>();
    }

    /// <summary>
    /// Tạo một Inventory với số slot được chỉ định.
    /// </summary>
    /// <param name="capacity">
    /// Số slot cần tạo. Giá trị nhỏ hơn 1 sẽ được điều chỉnh thành 1.
    /// </param>
    public Inventory(int capacity)
        : this()
    {
        int validCapacity = Mathf.Max(1, capacity);

        for (int i = 0; i < validCapacity; i++)
        {
            slots.Add(new InventorySlot());
        }
    }

    /// <summary>
    /// Lấy một slot theo chỉ số.
    /// </summary>
    /// <param name="index">
    /// Chỉ số của slot, bắt đầu từ 0.
    /// </param>
    /// <returns>
    /// InventorySlot tại chỉ số được yêu cầu.
    /// Trả về null nếu chỉ số nằm ngoài Inventory.
    /// </returns>
    public InventorySlot GetSlot(int index)
    {
        EnsureSlotInstances();

        if (index < 0 || index >= slots.Count)
        {
            return null;
        }

        return slots[index];
    }

    /// <summary>
    /// Kiểm tra Inventory có đủ chỗ để chứa toàn bộ số lượng
    /// Item được cung cấp hay không.
    ///
    /// Phương thức này chỉ kiểm tra và không làm thay đổi Inventory.
    /// </summary>
    /// <param name="itemData">
    /// Loại Item cần kiểm tra.
    /// </param>
    /// <param name="amount">
    /// Số lượng Item cần thêm.
    /// </param>
    /// <returns>
    /// True nếu toàn bộ số lượng Item có thể được thêm.
    /// </returns>
    public bool CanAddItem(ItemData itemData, int amount)
    {
        if (itemData == null || amount <= 0)
        {
            return false;
        }

        EnsureSlotInstances();

        int remainingAmount = amount;

        // Kiểm tra sức chứa còn lại trong các stack cùng loại.
        for (int i = 0; i < slots.Count; i++)
        {
            InventorySlot slot = slots[i];

            if (slot.IsEmpty || slot.ItemData != itemData)
            {
                continue;
            }

            remainingAmount -= Mathf.Min(
                remainingAmount,
                slot.RemainingCapacity);

            if (remainingAmount <= 0)
            {
                return true;
            }
        }

        // Kiểm tra sức chứa của các slot đang trống.
        for (int i = 0; i < slots.Count; i++)
        {
            InventorySlot slot = slots[i];

            if (!slot.IsEmpty)
            {
                continue;
            }

            remainingAmount -= Mathf.Min(
                remainingAmount,
                itemData.MaxStackSize);

            if (remainingAmount <= 0)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Thêm Item vào Inventory.
    ///
    /// Inventory ưu tiên điền vào các stack cùng loại đang có
    /// trước khi sử dụng slot trống.
    /// </summary>
    /// <param name="itemData">
    /// Loại Item cần thêm.
    /// </param>
    /// <param name="amount">
    /// Số lượng muốn thêm.
    /// </param>
    /// <returns>
    /// Tổng số lượng thực tế đã được thêm vào Inventory.
    /// Giá trị có thể nhỏ hơn amount nếu Inventory không đủ chỗ.
    /// </returns>
    public int AddItem(ItemData itemData, int amount)
    {
        if (itemData == null || amount <= 0)
        {
            return 0;
        }

        EnsureSlotInstances();

        int remainingAmount = amount;

        // Lượt 1: Điền vào các stack cùng loại đang còn chỗ.
        for (int i = 0; i < slots.Count; i++)
        {
            if (remainingAmount <= 0)
            {
                break;
            }

            InventorySlot slot = slots[i];

            if (slot.IsEmpty || slot.ItemData != itemData)
            {
                continue;
            }

            int addedAmount = slot.AddQuantity(
                itemData,
                remainingAmount);

            remainingAmount -= addedAmount;
        }

        // Lượt 2: Đưa phần còn lại vào các slot trống.
        for (int i = 0; i < slots.Count; i++)
        {
            if (remainingAmount <= 0)
            {
                break;
            }

            InventorySlot slot = slots[i];

            if (!slot.IsEmpty)
            {
                continue;
            }

            int addedAmount = slot.AddQuantity(
                itemData,
                remainingAmount);

            remainingAmount -= addedAmount;
        }

        int totalAdded = amount - remainingAmount;

        if (totalAdded > 0)
        {
            NotifyChanged();
        }

        return totalAdded;
    }

    /// <summary>
    /// Xóa một số lượng Item khỏi Inventory.
    ///
    /// Inventory duyệt các slot từ đầu đến cuối và tự làm trống
    /// những slot có số lượng trở về 0.
    /// </summary>
    /// <param name="itemData">
    /// Loại Item cần xóa.
    /// </param>
    /// <param name="amount">
    /// Số lượng muốn xóa.
    /// </param>
    /// <returns>
    /// Tổng số lượng thực tế đã được xóa.
    /// </returns>
    public int RemoveItem(ItemData itemData, int amount)
    {
        if (itemData == null || amount <= 0)
        {
            return 0;
        }

        EnsureSlotInstances();

        int remainingAmount = amount;

        for (int i = 0; i < slots.Count; i++)
        {
            if (remainingAmount <= 0)
            {
                break;
            }

            InventorySlot slot = slots[i];

            if (slot.IsEmpty || slot.ItemData != itemData)
            {
                continue;
            }

            int removedAmount = slot.RemoveQuantity(
                remainingAmount);

            remainingAmount -= removedAmount;
        }

        int totalRemoved = amount - remainingAmount;

        if (totalRemoved > 0)
        {
            NotifyChanged();
        }

        return totalRemoved;
    }

    /// <summary>
    /// Tính tổng số lượng của một loại Item trong tất cả slot.
    /// </summary>
    /// <param name="itemData">
    /// Loại Item cần đếm.
    /// </param>
    /// <returns>
    /// Tổng số lượng Item hiện có.
    /// </returns>
    public int GetItemCount(ItemData itemData)
    {
        if (itemData == null)
        {
            return 0;
        }

        EnsureSlotInstances();

        int totalQuantity = 0;

        for (int i = 0; i < slots.Count; i++)
        {
            InventorySlot slot = slots[i];

            if (slot.IsEmpty || slot.ItemData != itemData)
            {
                continue;
            }

            totalQuantity += slot.Quantity;
        }

        return totalQuantity;
    }

    /// <summary>
    /// Kiểm tra Inventory có ít nhất một số lượng Item nhất định.
    /// </summary>
    /// <param name="itemData">
    /// Loại Item cần kiểm tra.
    /// </param>
    /// <param name="amount">
    /// Số lượng tối thiểu cần có.
    /// </param>
    /// <returns>
    /// True nếu Inventory có đủ số lượng được yêu cầu.
    /// </returns>
    public bool Contains(ItemData itemData, int amount)
    {
        if (itemData == null || amount <= 0)
        {
            return false;
        }

        return GetItemCount(itemData) >= amount;
    }

    /// <summary>
    /// Xóa toàn bộ Item khỏi tất cả slot trong Inventory.
    /// </summary>
    public void Clear()
    {
        EnsureSlotInstances();

        bool hasChanged = false;

        for (int i = 0; i < slots.Count; i++)
        {
            InventorySlot slot = slots[i];

            if (slot.IsEmpty)
            {
                continue;
            }

            slot.Clear();
            hasChanged = true;
        }

        if (hasChanged)
        {
            NotifyChanged();
        }
    }

    /// <summary>
    /// Bảo đảm danh sách slot và từng phần tử trong danh sách
    /// luôn tồn tại trước khi Inventory sử dụng chúng.
    ///
    /// Đây là lớp bảo vệ cho dữ liệu được Unity deserialize.
    /// </summary>
    private void EnsureSlotInstances()
    {
        if (slots == null)
        {
            slots = new List<InventorySlot>();
            return;
        }

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = new InventorySlot();
            }
        }
    }

    /// <summary>
    /// Phát thông báo cho các hệ thống đang theo dõi Inventory.
    /// </summary>
    private void NotifyChanged()
    {
        Changed?.Invoke();
    }
}