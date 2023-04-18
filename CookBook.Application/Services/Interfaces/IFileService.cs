using Microsoft.AspNetCore.Http;

namespace CookBook.Application.Services.Interfaces
{
    public interface IFileService
    {
        Task<string?> UploadImageFile(IFormFile imageFile);
        Task DeleteImageFile(string fileName);
    }
}
