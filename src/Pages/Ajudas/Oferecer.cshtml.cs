using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using ConexaoSolidaria.Data;
using ConexaoSolidaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConexaoSolidaria.Pages.Ajudas;

/// <summary>
/// Tela 10 — Como Ajudar (Oferecer Ajuda).
/// Permite ao usuário autenticado escolher a modalidade de apoio
/// (doação de itens, voluntariado presencial ou transporte) e
/// registrar a oferta para uma Solicitacao específica.
/// RF07, RF09 — CRUD de OfertaAjuda.
/// </summary>
[Authorize]
public class OfereceModel : PageModel
{
    private readonly AppDbContext _db;

    public OfereceModel(AppDbContext db) => _db = db;

    // ── Dados da solicitação para exibição ────────────────────────────────
    public Solicitacao? Solicitacao { get; set; }

    // ── Input do formulário ───────────────────────────────────────────────
    [BindProperty]
    public OfertaInputModel Input { get; set; } = new();

    public class OfertaInputModel
    {
        [Required(ErrorMessage = "Selecione a modalidade de ajuda.")]
        public string Modalidade { get; set; } = "doacao";

        // Checkboxes de itens (apenas para modalidade "doacao")
        public bool ItemAlimentos { get; set; }
        public bool ItemRoupas    { get; set; }
        public bool ItemHigiene   { get; set; }
        public bool ItemColchoes  { get; set; }

        [MaxLength(500, ErrorMessage = "Observacoes devem ter no maximo 500 caracteres.")]
        public string? Observacoes { get; set; }

        [Required]
        public int SolicitacaoId  { get; set; }
    }

    // ── GET ───────────────────────────────────────────────────────────────
    public async Task<IActionResult> OnGetAsync(int solicitacaoId)
    {
        Solicitacao = await _db.Solicitacoes
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == solicitacaoId);

        if (Solicitacao == null)
            return NotFound();

        Input.SolicitacaoId = solicitacaoId;
        return Page();
    }

    // ── POST ──────────────────────────────────────────────────────────────
    public async Task<IActionResult> OnPostAsync()
    {
        // Recarregar solicitação para exibir no formulário mesmo com erros
        Solicitacao = await _db.Solicitacoes
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == Input.SolicitacaoId);

        if (Solicitacao == null)
            return NotFound();

        if (!ModelState.IsValid)
            return Page();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
            return Challenge();

        // Verificar se usuário já ofereceu ajuda para esta solicitação
        var jaOfertou = await _db.OfertasAjuda
            .AnyAsync(o => o.SolicitacaoId == Input.SolicitacaoId
                        && o.VoluntarioId  == userId
                        && o.Status        != "cancelada");

        if (jaOfertou)
        {
            ModelState.AddModelError(string.Empty,
                "Voce ja registrou uma oferta de ajuda para esta solicitacao.");
            return Page();
        }

        // Montar descrição dos itens
        var itens = new List<string>();
        if (Input.ItemAlimentos) itens.Add("Alimentos nao-pereciveis");
        if (Input.ItemRoupas)    itens.Add("Roupas (G/GG)");
        if (Input.ItemHigiene)   itens.Add("Itens de higiene pessoal");
        if (Input.ItemColchoes)  itens.Add("Colchoes / Cobertores");

        var oferta = new OfertaAjuda
        {
            SolicitacaoId = Input.SolicitacaoId,
            VoluntarioId  = userId,
            Modalidade    = Input.Modalidade,
            Itens         = itens.Count > 0 ? string.Join(", ", itens) : null,
            Observacoes   = Input.Observacoes?.Trim(),
            Status        = "confirmada",
            DataOferta    = DateTime.UtcNow,
        };

        _db.OfertasAjuda.Add(oferta);
        await _db.SaveChangesAsync();

        return RedirectToPage("/Ajudas/Confirmada", new { ofertaId = oferta.Id });
    }
}
