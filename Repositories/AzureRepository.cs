using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LoginRegisterIdentity.Helpers;
using LoginRegisterIdentity.Interfaces;

namespace LoginRegisterIdentity.Repositories
{
	public class AzureRepository : IPhotoService
	{
		private readonly BlobServiceClient _blobServiceClient;
		private readonly string _containerName;

		public AzureRepository(IOptions<AzureStorageOptions> options)
		{
			_blobServiceClient = new BlobServiceClient(options.Value.ConnectionString);
			_containerName = options.Value.ContainerName;
		}

		public async Task<string> AddPhotoAsync(IFormFile file)
		{
			var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

			// Upewnij się, że kontener istnieje
			await containerClient.CreateIfNotExistsAsync();

			var blobName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			var blobClient = containerClient.GetBlobClient(blobName);

			using (var stream = file.OpenReadStream())
			{
				await blobClient.UploadAsync(stream, true);
			}

			// Zwróć publiczny URL pliku w Azure Blob Storage
			return blobClient.Uri.ToString();
		}

		public async Task<bool> DeletePhotoAsync(string imageUrl)
		{
			var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

			var blobName = imageUrl.Split('/').Last();
			var blobClient = containerClient.GetBlobClient(blobName);

			if (await blobClient.ExistsAsync())
			{
				await blobClient.DeleteIfExistsAsync();
				return true;
			}

			return false;
		}
	}
}