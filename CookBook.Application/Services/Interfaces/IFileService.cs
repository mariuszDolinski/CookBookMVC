using CookBook.Application.DtoModels;
using Microsoft.AspNetCore.Http;

namespace CookBook.Application.Services.Interfaces
{
    public interface IFileService
    {
        Task UploadImageFile(RecipeDto dto);
    }
}
