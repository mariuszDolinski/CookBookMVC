namespace CookBook.Application.Commons
{
    public interface IEditItemCommand : IItemListDto
    {
        string OldName { get; set; }
    }
}
