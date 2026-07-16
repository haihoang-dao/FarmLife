/// <summary>
/// Định nghĩa hợp đồng chung cho các đối tượng
/// mà Player có thể thực hiện tương tác.
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// Thực hiện hành vi tương tác của đối tượng.
    /// </summary>
    void Interact();
}