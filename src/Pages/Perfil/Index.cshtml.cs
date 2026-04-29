using ConexaoSolidaria.Models;
using ConexaoSolidaria.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ConexaoSolidaria.Pages.Perfil;

[Authorize]
public class IndexModel : PageModel
{
    private readonly UserManager<Usuario> _userManager;
    private readonly IBlobStorageService _blob;

    public IndexModel(UserManager<Usuario> userManager, IBlobStorageService blob)
    {
        _userManager = userManager;
        _blob = blob;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    [BindProperty]
    public IFormFile? Foto { get; set; }

    public string? FotoAtualUrl { get; set; }
    public string? Email { get; set; }
    public string? MensagemSucesso { get; set; }

    public class InputModel
    {
        [Required(ErrorMessage = "Informe seu nome completo.")]
        [StringLength(120, MinimumLength = 3)]
        [Display(Name = "Nome completo")]
        public string NomeCompleto { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Telefone { get; set; }

        [StringLength(100)]
        public string? Cidade { get; set; }

        [StringLength(2)]
        public string? Estado { get; set; }
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Challenge();

        Input = new InputModel
        {
            NomeCompleto = user.NomeCompleto,
            Telefone     = user.Telefone,
            Cidade       = user.Cidade,
            Estado       = user.Estado
        };
        FotoAtualUrl = user.FotoUrl;
        Email = user.Email;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Challenge();

        if (!ModelState.IsValid)
        {
            FotoAtualUrl = user.FotoUrl;
            Email = user.Email;
            return Page();
        }

        // Atualiza foto se houver upload
        if (Foto is not null && Foto.Length > 0)
        {
            if (Foto.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError("Foto", "Foto muito grande (máx. 5 MB).");
                FotoAtualUrl = user.FotoUrl;
                Email = user.Email;
                return Page();
            }
            using var stream = Foto.OpenReadStream();
            user.FotoUrl = await _blob.UploadAsync(stream, Foto.FileName, Foto.ContentType);
        }

        user.NomeCompleto = Input.NomeCompleto;
        user.Telefone     = Input.Telefone;
        user.Cidade       = Input.Cidade;
        user.Estado       = Input.Estado?.ToUpper();

        await _userManager.UpdateAsync(user);

        MensagemSucesso = "Perfil atualizado com sucesso!";
        FotoAtualUrl = user.FotoUrl;
        Email = user.Email;
        return Page();
    }
}
