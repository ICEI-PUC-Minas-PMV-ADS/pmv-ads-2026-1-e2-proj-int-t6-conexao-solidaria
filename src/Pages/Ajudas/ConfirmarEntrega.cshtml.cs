using System.Security.Claims;
using ConexaoSolidaria.Data;
using ConexaoSolidaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConexaoSolidaria.Pages.Ajudas;

[Authorize]
public class ConfirmarEntregaModel : PageModel
{
    private readonly AppDbContext _db;
    public ConfirmarEntregaModel(AppDbContext db) => _db = db;

    public OfertaAjuda? Oferta { get; set; }

    [BindProperty]
    public EntregaInputModel Input { get; set; } = new();

    public class EntregaInputModel
    {
        public string? ObservacoesEntrega { get; set; }
        public int OfertaId { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(int ofertaId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Oferta = await _db.OfertasAjuda
            .Include(o => o.Solicitacao)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == ofertaId && o.VoluntarioId == userId);

        if (Oferta == null) return NotFound();
        Input.OfertaId = ofertaId;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var oferta = await _db.OfertasAjuda
            .FirstOrDefaultAsync(o => o.Id == Input.OfertaId && o.VoluntarioId == userId);

        if (oferta == null) return NotFound();

        oferta.Status = "concluida";
        oferta.ObservacoesEntrega = Input.ObservacoesEntrega?.Trim();
        oferta.ConcluidaEm = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return RedirectToPage("/Ajudas/Index");
    }
}
