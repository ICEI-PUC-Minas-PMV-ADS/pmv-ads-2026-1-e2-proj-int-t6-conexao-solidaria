namespace ConexaoSolidaria.Services
{
    public interface IEmailService
    {
        Task EnviarEmailAsync(string emailDestino, string assunto, string mensagemHtml);
    }
}