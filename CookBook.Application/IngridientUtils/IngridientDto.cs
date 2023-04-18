namespace CookBook.Application.IngridientUtils
{
    public class IngridientDto
    {
        public string Name { get; set; } = default!;
        public string EncodedName { get; private set; } = default!;
    }
}
