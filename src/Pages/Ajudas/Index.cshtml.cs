using System.Security.Claims;
using ConexaoSolidaria.Data;
using ConexaoSolidaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConexaoSolidaria.Pages.Ajudas;

/// <summary>
/// Tela "Minhas Ajudas" — lista todas as ofertas de ajuda do voluntário logado.
/// Atende RF07, RF09.
/// </summary>
[Authorize]
public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public IndexModel(AppDbContext db) => _db = db;

    public List<OfertaAjuda> Ofertas { get; set; } = new();

    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        Ofertas = await _db.OfertasAjuda
            .Include(o => o.Solicitacao)
            .Where(o => o.VoluntarioId == userId)
            .OrderByDescending(o => o.CriadaEm)
            .AsNoTracking()
            .ToListAsync();
    }
}
