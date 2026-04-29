namespace ConexaoSolidaria.Services;

/// <summary>
/// Abstração para upload de arquivos no Azure Blob Storage.
/// Usada pelas telas de Perfil (RF13) e Nova Solicitação (RF05).
/// </summary>
public interface IBlobStorageService
{
    /// <summary>
    /// Envia o arquivo para o container configurado e retorna a URL pública.
    /// </summary>
    Task<string> UploadAsync(Stream content, string fileName, string contentType);
}
