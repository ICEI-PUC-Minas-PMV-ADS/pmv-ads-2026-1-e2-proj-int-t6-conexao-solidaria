using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace ConexaoSolidaria.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobContainerClient _container;

    public BlobStorageService(IConfiguration config)
    {
        var connectionString = config.GetConnectionString("BlobStorage")
            ?? config["AzureBlobStorage:ConnectionString"]
            ?? throw new InvalidOperationException("Storage connection string nao configurada.");

        var containerName = config["BlobContainerName"]
            ?? config["AzureBlobStorage:ContainerName"]
            ?? "imagens";

        var serviceClient = new BlobServiceClient(connectionString);
        _container = serviceClient.GetBlobContainerClient(containerName);

        _container.CreateIfNotExists(PublicAccessType.Blob);
    }

    public async Task<string> UploadAsync(Stream content, string fileName, string contentType)
    {
        var ext = Path.GetExtension(fileName);
        var blobName = $"{Guid.NewGuid():N}{ext}";

        var blob = _container.GetBlobClient(blobName);
        await blob.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });

        return blob.Uri.ToString();
  

  }

}

