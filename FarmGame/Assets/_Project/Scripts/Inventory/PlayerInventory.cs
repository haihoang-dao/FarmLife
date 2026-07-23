using UnityEngine;

/// <summary>
/// Component đại diện cho Inventory thuộc về Player.
///
/// PlayerInventory là cầu nối giữa Player GameObject
/// và lớp dữ liệu Inventory thuần C#.
/// </summary>
[DisallowMultipleComponent]
public sealed class PlayerInventory : MonoBehaviour
{
    /// <summary>
    /// Số slot được tạo khi Player chưa có dữ liệu Inventory.
    ///
    /// Giá trị này sẽ được sử dụng khi bắt đầu game mới.
    /// </summary>
    [Header("Inventory Settings")]
    [SerializeField, Min(1)]
    private int initialCapacity = 24;

    /// <summary>
    /// Dữ liệu Inventory thực tế thuộc về Player.
    ///
    /// Field được serialize để có thể quan sát dữ liệu runtime
    /// trong Inspector và hỗ trợ Save/Load ở phase sau.
    /// </summary>
    [Header("Runtime Data")]
    [SerializeField]
    private Inventory inventory;

    /// <summary>
    /// Inventory hiện tại của Player.
    ///
    /// Property luôn bảo đảm Inventory đã được khởi tạo
    /// trước khi trả về cho hệ thống bên ngoài.
    /// </summary>
    public Inventory Inventory
    {
        get
        {
            EnsureInventoryInitialized();
            return inventory;
        }
    }

    /// <summary>
    /// Tổng số slot hiện có trong Inventory của Player.
    /// </summary>
    public int Capacity => Inventory.Capacity;

    /// <summary>
    /// Khởi tạo Inventory trước khi các hệ thống gameplay
    /// bắt đầu sử dụng Player.
    /// </summary>
    private void Awake()
    {
        EnsureInventoryInitialized();
    }

    /// <summary>
    /// Bảo đảm giá trị cấu hình trong Inspector không nhỏ hơn 1.
    /// </summary>
    private void OnValidate()
    {
        initialCapacity = Mathf.Max(1, initialCapacity);
    }

    /// <summary>
    /// Kiểm tra Inventory của Player có đủ chỗ để chứa
    /// toàn bộ số lượng Item được yêu cầu hay không.
    /// </summary>
    public bool CanAddItem(ItemData itemData, int amount)
    {
        return Inventory.CanAddItem(itemData, amount);
    }

    /// <summary>
    /// Thêm Item vào Inventory của Player.
    /// </summary>
    /// <returns>
    /// Số lượng thực tế đã được thêm.
    /// </returns>
    public int AddItem(ItemData itemData, int amount)
    {
        return Inventory.AddItem(itemData, amount);
    }

    /// <summary>
    /// Xóa Item khỏi Inventory của Player.
    /// </summary>
    /// <returns>
    /// Số lượng thực tế đã được xóa.
    /// </returns>
    public int RemoveItem(ItemData itemData, int amount)
    {
        return Inventory.RemoveItem(itemData, amount);
    }

    /// <summary>
    /// Trả về tổng số lượng của một loại Item
    /// đang có trong Inventory của Player.
    /// </summary>
    public int GetItemCount(ItemData itemData)
    {
        return Inventory.GetItemCount(itemData);
    }

    /// <summary>
    /// Kiểm tra Player có ít nhất một số lượng Item nhất định.
    /// </summary>
    public bool Contains(ItemData itemData, int amount)
    {
        return Inventory.Contains(itemData, amount);
    }

    /// <summary>
    /// Xóa toàn bộ Item khỏi Inventory của Player.
    /// </summary>
    public void Clear()
    {
        Inventory.Clear();
    }

    /// <summary>
    /// Tạo Inventory mới nếu Player chưa có Inventory hợp lệ.
    ///
    /// Inventory đang có dữ liệu sẽ được giữ nguyên,
    /// không bị thay thế mỗi lần property Inventory được truy cập.
    /// </summary>
    private void EnsureInventoryInitialized()
    {
        if (inventory != null && inventory.Capacity > 0)
        {
            return;
        }

        int validCapacity = Mathf.Max(1, initialCapacity);
        inventory = new Inventory(validCapacity);
    }
}