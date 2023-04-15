using CookBook.Application.RecipeUtils;

namespace CookBook.Application.Services.Interfaces
{
    public interface IFileService
    {
        Task UploadImageFile(RecipeDto dto);
    }
}
