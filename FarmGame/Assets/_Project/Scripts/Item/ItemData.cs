using UnityEngine;

/// <summary>
/// Chứa dữ liệu cố định dùng chung của một loại vật phẩm.
///
/// ItemData không lưu số lượng vật phẩm hiện tại và không chứa
/// trạng thái thay đổi trong lúc chơi. Số lượng thực tế của vật phẩm
/// sẽ do InventorySlot quản lý.
/// </summary>
[CreateAssetMenu(
    fileName = "NewItemData",
    menuName = "Farm Life/Item/Item Data")]
public sealed class ItemData : ScriptableObject
{
    /// <summary>
    /// Mã nhận dạng duy nhất của vật phẩm.
    ///
    /// ID được dùng để nhận dạng vật phẩm khi lưu và tải dữ liệu.
    /// Ví dụ: turnip_seed, turnip_crop hoặc watering_can.
    /// </summary>
    [Header("Identity")]
    [SerializeField]
    private string itemId = string.Empty;

    /// <summary>
    /// Tên vật phẩm được hiển thị cho người chơi.
    /// </summary>
    [SerializeField]
    private string displayName = string.Empty;

    /// <summary>
    /// Nội dung mô tả chức năng hoặc đặc điểm của vật phẩm.
    /// </summary>
    [SerializeField, TextArea(3, 6)]
    private string description = string.Empty;

    /// <summary>
    /// Hình ảnh đại diện được sử dụng trong Inventory UI và các giao diện khác.
    /// </summary>
    [SerializeField]
    private Sprite icon;

    /// <summary>
    /// Nhóm chức năng chính của vật phẩm.
    /// </summary>
    [Header("Classification")]
    [SerializeField]
    private ItemType itemType = ItemType.None;

    /// <summary>
    /// Xác định nhiều vật phẩm cùng loại có được chứa chung trong một slot hay không.
    /// </summary>
    [Header("Stack Settings")]
    [SerializeField]
    private bool isStackable = true;

    /// <summary>
    /// Số lượng tối đa được phép chứa trong một stack.
    ///
    /// Đối với vật phẩm không thể stack, giá trị hiệu lực luôn bằng 1.
    /// </summary>
    [SerializeField, Min(1)]
    private int maxStackSize = 99;

    /// <summary>
    /// Giá mua cơ bản của vật phẩm.
    /// </summary>
    [Header("Economy")]
    [SerializeField, Min(0)]
    private int buyPrice;

    /// <summary>
    /// Giá bán cơ bản của vật phẩm.
    /// </summary>
    [SerializeField, Min(0)]
    private int sellPrice;

    /// <summary>
    /// Lấy mã nhận dạng duy nhất của vật phẩm.
    /// </summary>
    public string ItemId => itemId;

    /// <summary>
    /// Lấy tên hiển thị của vật phẩm.
    /// </summary>
    public string DisplayName => displayName;

    /// <summary>
    /// Lấy nội dung mô tả của vật phẩm.
    /// </summary>
    public string Description => description;

    /// <summary>
    /// Lấy hình ảnh đại diện của vật phẩm.
    /// </summary>
    public Sprite Icon => icon;

    /// <summary>
    /// Lấy nhóm chức năng chính của vật phẩm.
    /// </summary>
    public ItemType ItemType => itemType;

    /// <summary>
    /// Xác định vật phẩm có cho phép xếp chồng hay không.
    /// </summary>
    public bool IsStackable => isStackable;

    /// <summary>
    /// Lấy giới hạn số lượng hiệu lực của một stack.
    ///
    /// Vật phẩm không cho phép stack luôn có giới hạn bằng 1.
    /// </summary>
    public int MaxStackSize => isStackable ? maxStackSize : 1;

    /// <summary>
    /// Lấy giá mua cơ bản của vật phẩm.
    /// </summary>
    public int BuyPrice => buyPrice;

    /// <summary>
    /// Lấy giá bán cơ bản của vật phẩm.
    /// </summary>
    public int SellPrice => sellPrice;

    /// <summary>
    /// Chuẩn hóa các giá trị được nhập từ Inspector trong Unity Editor.
    /// </summary>
    private void OnValidate()
    {
        itemId = NormalizeItemId(itemId);
        displayName = displayName.Trim();
        maxStackSize = Mathf.Max(1, maxStackSize);
        buyPrice = Mathf.Max(0, buyPrice);
        sellPrice = Mathf.Max(0, sellPrice);
    }

    /// <summary>
    /// Chuẩn hóa ID về chữ thường, loại bỏ khoảng trắng thừa
    /// và thay khoảng trắng giữa các từ bằng dấu gạch dưới.
    /// </summary>
    /// <param name="rawItemId">ID chưa được chuẩn hóa.</param>
    /// <returns>ID đã được chuẩn hóa.</returns>
    private static string NormalizeItemId(string rawItemId)
    {
        if (string.IsNullOrWhiteSpace(rawItemId))
        {
            return string.Empty;
        }

        return rawItemId
            .Trim()
            .ToLowerInvariant()
            .Replace(' ', '_');
    }
}