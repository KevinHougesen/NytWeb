using Microsoft.AspNetCore.Mvc;
using NytWeb.Models;

namespace NytWeb.Services
{
    public interface IBlobService
    {
        Task<string> UploadBlobFilesAsync(Stream fileStream, string fileName, string contentType, string Username);

    }
}