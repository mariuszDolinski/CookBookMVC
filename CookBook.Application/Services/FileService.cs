using CookBook.Application.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using CookBook.Application.RecipeUtils;
using Microsoft.AspNetCore.Http;
using MediatR;

namespace CookBook.Application.Services
{
    internal class FileService : IFileService
    {
        private readonly IHostEnvironment _environment;
        public FileService(IHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<string?> UploadImageFile(IFormFile imageFile)
        {
            string? imageName = null;
            if (imageFile != null)
            {
                string filename = Path.GetFileNameWithoutExtension(imageFile.FileName);
                string extension = Path.GetExtension(imageFile.FileName);
                imageName = filename + DateTime.Now.ToString("yyMMddHHmmssfff") + extension;

                var filePath = GetFilePath(imageName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        public Task DeleteImageFile(string fileName)
        {
            var filePath = GetFilePath(fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }

        private string GetFilePath(string fileName)
            => Path.Combine(_environment.ContentRootPath, @"wwwroot\images", fileName);
    }
}
