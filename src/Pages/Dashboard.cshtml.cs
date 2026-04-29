using ConexaoSolidaria.Data;
using ConexaoSolidaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConexaoSolidaria.Pages;

[Authorize]
public class DashboardModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly UserManager<Usuario> _userManager;

    public DashboardModel(AppDbContext db, UserManager<Usuario> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public string NomeUsuario { get; set; } = string.Empty;
    public int TotalSolicitacoes { get; set; }
    public int TotalAtivas { get; set; }
    public int TotalUrgentes { get; set; }
    public List<Solicitacao> SolicitacoesUrgentes { get; set; } = new();

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        NomeUsuario = user?.NomeCompleto ?? "Usuário";

        TotalSolicitacoes = await _db.Solicitacoes.CountAsync();
        TotalAtivas       = await _db.Solicitacoes.CountAsync(s => s.Status == "ativa");
        TotalUrgentes     = await _db.Solicitacoes.CountAsync(s => s.Urgencia == "alta" && s.Status == "ativa");

        SolicitacoesUrgentes = await _db.Solicitacoes
            .Include(s => s.Usuario)
            .Where(s => s.Status == "ativa")
            .OrderByDescending(s => s.Urgencia == "alta")
            .ThenByDescending(s => s.CriadoEm)
            .Take(5)
            .ToListAsync();
    }
}
