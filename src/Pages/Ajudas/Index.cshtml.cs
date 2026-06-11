using System.Security.Claims;
using ConexaoSolidaria.Data;
using ConexaoSolidaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConexaoSolidaria.Pages.Ajudas;

[Authorize]
public class AjudasIndexModel : PageModel
{
    private readonly AppDbContext _db;
    public AjudasIndexModel(AppDbContext db) => _db = db;

    public List<OfertaAjuda> Ofertas { get; set; } = new();

    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return;

        Ofertas = await _db.OfertasAjuda
            .Include(o => o.Solicitacao)
            .Where(o => o.VoluntarioId == userId)
            .OrderByDescending(o => o.CriadaEm)
            .ToListAsync();
    }
}
