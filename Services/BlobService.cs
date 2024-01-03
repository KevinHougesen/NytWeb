using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace NytWeb.Services
{

    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        // Upload Single Image To Blob
        public async Task<string> UploadBlobFilesAsync(Stream fileStream, string fileName, string contentType, string Username)
        {
            string containerName = "images";

            BlobContainerClient container = _blobServiceClient.GetBlobContainerClient(containerName);

            // Create the container if it does not exist
            await container.CreateIfNotExistsAsync(PublicAccessType.Blob);

            string uniqueFileName = $"{Guid.NewGuid()}_{fileName}";

            BlobClient blob = container.GetBlobClient(uniqueFileName);

            await using (fileStream) // Ensure the stream is disposed after use
            {
                await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });
            }

            return blob.Uri.ToString();
        }

        public async Task<string> GetBlobUrlAsync(string username, string fileName)
        {
            string containerName = $"{username}/images";
            BlobContainerClient container = _blobServiceClient.GetBlobContainerClient(containerName);

            BlobClient blob = container.GetBlobClient(fileName);

            if (await blob.ExistsAsync())
            {
                return blob.Uri.ToString();
            }
            else
            {
                // Handle the case where the blob does not exist
                return null;
            }
        }

    }
}