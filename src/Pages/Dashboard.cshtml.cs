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
    public double MediaReputacao { get; set; } 
    public int TotalAtivas { get; set; }
    public int TotalUrgentes { get; set; }
    public int DoacoesPendentes { get; set; }
    public int DoacoesEntregues { get; set; }

    public List<Solicitacao> SolicitacoesUrgentes { get; set; } = new();

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        NomeUsuario = user?.NomeCompleto ?? "Usuário";

        // Cálculo da Média de Reputação
        if (user != null)
        {
            var avaliacoes = await _db.Avaliacoes
                .Where(a => a.AvaliadoId == user.Id)
                .Select(a => a.Nota)
                .ToListAsync();

            MediaReputacao = avaliacoes.Any() ? avaliacoes.Average() : 0;
        }

        TotalAtivas = await _db.Solicitacoes.CountAsync(s => s.Status == "ativa");
        TotalUrgentes = await _db.Solicitacoes.CountAsync(s => s.Urgencia == "alta" && s.Status == "ativa");

        // Busca os dados do módulo de Doações
        DoacoesPendentes = await _db.Doacoes.CountAsync(d => d.Status == StatusDoacao.Pendente);
        DoacoesEntregues = await _db.Doacoes.CountAsync(d => d.Status == StatusDoacao.Entregue);

        SolicitacoesUrgentes = await _db.Solicitacoes
            .Include(s => s.Usuario)
            .Include(s => s.Doacoes) 
            .Where(s => s.Status == "ativa" || s.Status == "atendida")
            .OrderByDescending(s => s.Urgencia == "alta")
            .ThenByDescending(s => s.CriadoEm)
            .Take(5)
            .ToListAsync();
    }
}
