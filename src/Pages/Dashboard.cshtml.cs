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
    public int TotalAtivas { get; set; }
    public int TotalUrgentes { get; set; }
    public int DoacoesPendentes { get; set; }
    public int DoacoesEntregues { get; set; }
    public List<Solicitacao> SolicitacoesUrgentes { get; set; } = new();

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        NomeUsuario = user?.NomeCompleto ?? "Usuário";

        TotalAtivas = await _db.Solicitacoes.CountAsync(s => s.Status == "ativa");
        TotalUrgentes = await _db.Solicitacoes.CountAsync(s => s.Urgencia == "alta" && s.Status == "ativa");

        // Busca os dados do novo módulo de Doações
        DoacoesPendentes = await _db.Doacoes.CountAsync(d => d.Status == StatusDoacao.Pendente);
        DoacoesEntregues = await _db.Doacoes.CountAsync(d => d.Status == StatusDoacao.Entregue);

        SolicitacoesUrgentes = await _db.Solicitacoes
            .Include(s => s.Usuario)
            .Where(s => s.Status == "ativa" || s.Status == "atendida")
            .OrderByDescending(s => s.Urgencia == "alta")
            .ThenByDescending(s => s.CriadoEm)
            .Take(5)
            .ToListAsync();
    }
}
