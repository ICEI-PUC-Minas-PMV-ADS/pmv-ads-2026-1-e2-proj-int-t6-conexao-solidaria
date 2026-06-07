using ConexaoSolidaria.Data;
using ConexaoSolidaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConexaoSolidaria.Pages.Solicitacoes;

[Authorize]
public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public IndexModel(AppDbContext db) { _db = db; }

    public List<Solicitacao> Solicitacoes { get; set; } = new();

    [BindProperty(SupportsGet = true)] public string? FiltroUrgencia { get; set; }
    [BindProperty(SupportsGet = true)] public string? FiltroTipo { get; set; }
    [BindProperty(SupportsGet = true)] public string? FiltroCidade { get; set; }

    public async Task OnGetAsync()
    {
        var query = _db.Solicitacoes
            .Include(s => s.Usuario)
            .Where(s => s.Status == "ativa")
            .AsQueryable();

        if (!string.IsNullOrEmpty(FiltroUrgencia))
            query = query.Where(s => s.Urgencia == FiltroUrgencia);

        if (!string.IsNullOrEmpty(FiltroTipo))
            query = query.Where(s => s.TipoNecessidade == FiltroTipo);

        if (!string.IsNullOrEmpty(FiltroCidade))
            query = query.Where(s => s.Cidade.Contains(FiltroCidade));

        Solicitacoes = await query
            .OrderByDescending(s => s.Urgencia == "alta")
            .ThenByDescending(s => s.Urgencia == "media")
            .ThenByDescending(s => s.CriadoEm)
            .ToListAsync();
    }
}
