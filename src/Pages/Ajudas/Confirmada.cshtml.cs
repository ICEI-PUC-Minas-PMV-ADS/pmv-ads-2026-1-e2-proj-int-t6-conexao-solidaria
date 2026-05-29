using System.Security.Claims;
using ConexaoSolidaria.Data;
using ConexaoSolidaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConexaoSolidaria.Pages.Ajudas;

/// <summary>
/// Tela 15 — Ajuda Confirmada.
/// Exibe o feedback de sucesso após o usuário confirmar uma oferta de ajuda.
/// Apresenta o resumo da oferta, os próximos passos e atalhos para chat e avaliação.
/// RF07, RF09 — confirmação visual do registro de OfertaAjuda.
/// </summary>
[Authorize]
public class ConfirmadaModel : PageModel
{
    private readonly AppDbContext _db;

    public ConfirmadaModel(AppDbContext db) => _db = db;

    // ── Dados carregados ──────────────────────────────────────────────────
    public OfertaAjuda? Oferta    { get; set; }
    public Solicitacao? Solicitacao { get; set; }

    // ── GET ───────────────────────────────────────────────────────────────
    public async Task<IActionResult> OnGetAsync(int ofertaId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        Oferta = await _db.OfertasAjuda
            .Include(o => o.Solicitacao)
                .ThenInclude(s => s!.Usuario)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == ofertaId && o.VoluntarioId == userId);

        if (Oferta == null)
            return NotFound();

        Solicitacao = Oferta.Solicitacao;

        return Page();
    }
}
