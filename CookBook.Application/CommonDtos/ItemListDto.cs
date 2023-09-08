namespace CookBook.Application.CommonDtos
{
    public class ItemListDto
    {
        public string? Name { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedTime { get; set; }
        public bool IsEditable { get; set; }
    }
}
