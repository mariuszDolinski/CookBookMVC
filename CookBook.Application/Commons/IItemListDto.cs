namespace CookBook.Application.Commons
{
    public interface IItemListDto
    {
        string? Name { get; set; }
        string? CreatedBy { get; set; }
        string? CreatedTime { get; set; }
        string? AddInfo { get; set; }
        bool IsEditable { get; set; }
    }
}
