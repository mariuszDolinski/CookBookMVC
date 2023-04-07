using CookBook.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using CookBook.Application.DtoModels;

namespace CookBook.Application.Services
{
    internal class FileService : IFileService
    {
        private readonly IHostEnvironment _environment;
        public FileService(IHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task UploadImageFile(RecipeDto dto)
        {
            if(dto.ImageName != null && dto.ImageFile != null)
            {
                var filePath = Path.Combine(_environment.ContentRootPath, @"wwwroot\images", dto.ImageName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await dto.ImageFile.CopyToAsync(fileStream);
            }          
        }
    }
}
