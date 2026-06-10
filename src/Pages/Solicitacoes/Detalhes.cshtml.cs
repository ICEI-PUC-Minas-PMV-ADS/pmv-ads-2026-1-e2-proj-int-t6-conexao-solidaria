using ConexaoSolidaria.Data;
using ConexaoSolidaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ConexaoSolidaria.Pages.Solicitacoes;

[Authorize]
public class DetalhesModel : PageModel
{
    private readonly AppDbContext _db;
    public DetalhesModel(AppDbContext db) { _db = db; }

    public Solicitacao? Solicitacao { get; set; }
    public bool JaOfereceuAjuda { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Solicitacao = await _db.Solicitacoes
            .Include(s => s.Usuario)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (Solicitacao is null) return NotFound();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId != null)
        {
            JaOfereceuAjuda = await _db.OfertasAjuda.AnyAsync(o => o.SolicitacaoId == id && o.VoluntarioId == userId);
        }

        return Page();
    }
}
