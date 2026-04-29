using ConexaoSolidaria.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ConexaoSolidaria.Pages;

public class CriarContaModel : PageModel
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;

    public CriarContaModel(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public List<string> ErrorList { get; set; } = new();

    public class InputModel
    {
        [Required(ErrorMessage = "Informe seu nome completo.")]
        [StringLength(120, MinimumLength = 3)]
        [Display(Name = "Nome completo")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe seu e-mail.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "Crie uma senha.")]
        [StringLength(50, MinimumLength = 8,
            ErrorMessage = "A senha deve ter pelo menos 8 caracteres.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirme sua senha.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Senha), ErrorMessage = "As senhas não coincidem.")]
        [Display(Name = "Confirmar senha")]
        public string ConfirmarSenha { get; set; } = string.Empty;
    }

    public IActionResult OnGet()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToPage("/Dashboard");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var usuario = new Usuario
        {
            UserName = Input.Email,
            Email = Input.Email,
            EmailConfirmed = true, // simplificação para a POC
            NomeCompleto = Input.NomeCompleto,
            Telefone = Input.Telefone,
            TipoPerfil = "beneficiario"
        };

        var result = await _userManager.CreateAsync(usuario, Input.Senha);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(usuario, isPersistent: false);
            return RedirectToPage("/Dashboard");
        }

        foreach (var error in result.Errors)
            ErrorList.Add(error.Description);

        return Page();
    }
}
