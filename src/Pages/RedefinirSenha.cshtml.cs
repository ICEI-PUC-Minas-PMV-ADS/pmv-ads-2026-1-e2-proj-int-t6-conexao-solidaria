using ConexaoSolidaria.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ConexaoSolidaria.Pages
{
    public class RedefinirSenhaModel : PageModel
    {
        private readonly UserManager<Usuario> _userManager;

        public RedefinirSenhaModel(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required]
            public string Email { get; set; } = string.Empty;

            [Required]
            public string Token { get; set; } = string.Empty;

            [Required(ErrorMessage = "A nova senha é obrigatória.")]
            [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} caracteres.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            public string NovaSenha { get; set; } = string.Empty;

            [DataType(DataType.Password)]
            [Compare("NovaSenha", ErrorMessage = "As senhas não coincidem.")]
            public string ConfirmarSenha { get; set; } = string.Empty;
        }

        public IActionResult OnGet(string? token, string? email)
        {
            if (token == null || email == null)
            {
                return RedirectToPage("/Login"); // Se não tiver token, expulsa pra tela de login
            }

            Input.Token = token;
            Input.Email = email;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Mesmo esquema de segurança: não avise que falhou
                return RedirectToPage("/Login");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Token, Input.NovaSenha);
            if (result.Succeeded)
            {
                TempData["MensagemSucesso"] = "Senha redefinida com sucesso! Faça login.";
                return RedirectToPage("/Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}