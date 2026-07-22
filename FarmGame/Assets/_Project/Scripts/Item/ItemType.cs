/// <summary>
/// Xác định nhóm chức năng chính của một vật phẩm.
///
/// ItemType chỉ dùng để phân loại dữ liệu vật phẩm.
/// Hành vi sử dụng cụ thể của hạt giống, công cụ hoặc vật phẩm tiêu hao
/// sẽ được xây dựng trong các hệ thống gameplay tương ứng.
/// </summary>
public enum ItemType
{
    /// <summary>
    /// Giá trị mặc định khi vật phẩm chưa được phân loại.
    /// </summary>
    None = 0,

    /// <summary>
    /// Hạt giống có thể được sử dụng để gieo trồng.
    /// </summary>
    Seed = 1,

    /// <summary>
    /// Nông sản thu được từ cây trồng.
    /// </summary>
    Crop = 2,

    /// <summary>
    /// Công cụ phục vụ các hoạt động như cuốc đất hoặc tưới nước.
    /// </summary>
    Tool = 3,

    /// <summary>
    /// Nguyên liệu cơ bản như gỗ, đá hoặc cỏ.
    /// </summary>
    Resource = 4,

    /// <summary>
    /// Vật phẩm có thể được tiêu thụ khi sử dụng.
    /// </summary>
    Consumable = 5,

    /// <summary>
    /// Vật phẩm liên quan đến nhiệm vụ.
    /// </summary>
    Quest = 6
}