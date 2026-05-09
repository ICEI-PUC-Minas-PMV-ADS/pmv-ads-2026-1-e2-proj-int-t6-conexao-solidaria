using System.Net;
using System.Net.Mail;

namespace ConexaoSolidaria.Services
{
    public class SmtpEmailService : IEmailService
    {
        public async Task EnviarEmailAsync(string emailDestino, string assunto, string mensagemHtml)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("suporte@conexaosolidaria.app", "Conexão Solidária"),
                Subject = assunto,
                Body = mensagemHtml,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(emailDestino);

            // DICA: Para testes, crie uma conta grátis no Mailtrap.io
            // Depois, quando for para produção, basta trocar estes dados pelos do SendGrid ou Gmail
            using var smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("SEU_USUARIO_MAILTRAP", "SUA_SENHA_MAILTRAP"),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}