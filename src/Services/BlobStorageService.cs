using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace ConexaoSolidaria.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobContainerClient _container;

    public BlobStorageService(IConfiguration config)
    {
        var connectionString = config["AzureBlobStorage:ConnectionString"]
            ?? throw new InvalidOperationException(
                "AzureBlobStorage:ConnectionString não configurado.");
        var containerName = config["AzureBlobStorage:ContainerName"] ?? "imagens";

        var serviceClient = new BlobServiceClient(connectionString);
        _container = serviceClient.GetBlobContainerClient(containerName);

        // Garante que o container existe e é publicamente legível
        _container.CreateIfNotExists(PublicAccessType.Blob);
    }

    public async Task<string> UploadAsync(Stream content, string fileName, string contentType)
    {
        // Gera nome único para evitar colisões
        var ext = Path.GetExtension(fileName);
        var blobName = $"{Guid.NewGuid():N}{ext}";

        var blob = _container.GetBlobClient(blobName);
        await blob.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });

        return blob.Uri.ToString();
    }
}
