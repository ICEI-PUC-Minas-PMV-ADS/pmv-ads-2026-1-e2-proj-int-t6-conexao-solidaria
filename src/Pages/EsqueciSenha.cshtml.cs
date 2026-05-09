using ConexaoSolidaria.Models;
using ConexaoSolidaria.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ConexaoSolidaria.Pages
{
    public class EsqueciSenhaModel : PageModel
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IEmailService _emailService;

        public EsqueciSenhaModel(UserManager<Usuario> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        [BindProperty]
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        public string? MensagemSucesso { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await _userManager.FindByEmailAsync(Email);

            // Por segurança, sempre dizemos que o e-mail foi enviado, mesmo que não exista no banco (evita que hackers descubram e-mails válidos)
            if (user != null && await _userManager.IsEmailConfirmedAsync(user))
            {
                // Token de segurança
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                // Cria o link de recuperação
                var linkRecuperacao = Url.Page(
                    "/RedefinirSenha",
                    pageHandler: null,
                    values: new { token, email = user.Email },
                    protocol: Request.Scheme);

                // Monta e envia o e-mail
                var assunto = "Redefinição de Senha - Conexão Solidária";
                var corpoHtml = $@"
                    <h3>Olá, {user.NomeCompleto}!</h3>
                    <p>Você solicitou a redefinição de sua senha.</p>
                    <p>Clique no link abaixo para criar uma nova senha:</p>
                    <a href='{linkRecuperacao}' style='display:inline-block; padding:10px 20px; background-color:#3B82F6; color:#fff; text-decoration:none; border-radius:5px;'>Redefinir Senha</a>
                    <p><br>Se você não solicitou isso, pode ignorar este e-mail.</p>";

                await _emailService.EnviarEmailAsync(user.Email, assunto, corpoHtml);
            }

            MensagemSucesso = "Se o seu e-mail estiver cadastrado, você receberá um link para redefinir sua senha em instantes.";
            return Page();
        }
    }
}