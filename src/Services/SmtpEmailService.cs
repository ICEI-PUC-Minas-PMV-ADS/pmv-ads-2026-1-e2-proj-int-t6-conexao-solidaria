using System.Diagnostics;

namespace ConexaoSolidaria.Services
{
    public class SmtpEmailService : IEmailService
    {
        public Task EnviarEmailAsync(string emailDestino, string assunto, string mensagemHtml)
        {
            Debug.WriteLine("\n\n========== 📧 NOVO E-MAIL SIMULADO ==========");
            Debug.WriteLine($"Para: {emailDestino}");
            Debug.WriteLine($"Assunto: {assunto}");
            Debug.WriteLine($"Mensagem (HTML):\n{mensagemHtml}");
            Debug.WriteLine("=============================================\n\n");

            return Task.CompletedTask;
        }
    }
}