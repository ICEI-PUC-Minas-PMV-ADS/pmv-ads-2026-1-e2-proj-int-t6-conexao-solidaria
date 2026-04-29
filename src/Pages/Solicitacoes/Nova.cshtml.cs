using ConexaoSolidaria.Data;
using ConexaoSolidaria.Models;
using ConexaoSolidaria.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ConexaoSolidaria.Pages.Solicitacoes;

[Authorize]
public class NovaModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly UserManager<Usuario> _userManager;
    private readonly IBlobStorageService _blob;

    public NovaModel(AppDbContext db, UserManager<Usuario> userManager, IBlobStorageService blob)
    {
        _db = db;
        _userManager = userManager;
        _blob = blob;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    [BindProperty]
    public IFormFile? Anexo { get; set; }

    public class InputModel
    {
        [Required(ErrorMessage = "Selecione o tipo de necessidade.")]
        public string TipoNecessidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe um título.")]
        [StringLength(120, MinimumLength = 5)]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descreva sua solicitação.")]
        [StringLength(1000, MinimumLength = 20)]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione a urgência.")]
        public string Urgencia { get; set; } = "media";

        [Required(ErrorMessage = "Informe a cidade.")]
        [StringLength(100)]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o estado.")]
        [StringLength(2, MinimumLength = 2)]
        public string Estado { get; set; } = string.Empty;
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Challenge();

        string? anexoUrl = null;
        if (Anexo is not null && Anexo.Length > 0)
        {
            // Limite simples de tamanho (5 MB)
            if (Anexo.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError("Anexo", "Anexo muito grande (máx. 5 MB).");
                return Page();
            }

            using var stream = Anexo.OpenReadStream();
            anexoUrl = await _blob.UploadAsync(stream, Anexo.FileName, Anexo.ContentType);
        }

        var solicitacao = new Solicitacao
        {
            UsuarioId       = user.Id,
            TipoNecessidade = Input.TipoNecessidade,
            Titulo          = Input.Titulo,
            Descricao       = Input.Descricao,
            Urgencia        = Input.Urgencia,
            Cidade          = Input.Cidade,
            Estado          = Input.Estado.ToUpper(),
            AnexoUrl        = anexoUrl,
            Status          = "ativa"
        };

        _db.Solicitacoes.Add(solicitacao);
        await _db.SaveChangesAsync();

        return RedirectToPage("/Solicitacoes/Detalhes", new { id = solicitacao.Id });
    }
}
