using ConexaoSolidaria.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConexaoSolidaria.Pages.Admin
{
    // Restringe o acesso apenas a quem tem a Role "Admin"
    [Authorize(Roles = "Admin")]
    public class DashboardModel : PageModel
    {
        private readonly AppDbContext _db;

        public DashboardModel(AppDbContext db)
        {
            _db = db;
        }

        public int TotalSolicitacoes { get; set; }
        public int TotalAtendidas { get; set; }
        public int DoacoesEntregues { get; set; }
        public Dictionary<string, int> TopCidades { get; set; } = new();

        public async Task OnGetAsync()
        {
            TotalSolicitacoes = await _db.Solicitacoes.CountAsync();
            TotalAtendidas = await _db.Solicitacoes.CountAsync(s => s.Status == "atendida");
            DoacoesEntregues = await _db.Doacoes.CountAsync(d => d.Status == Models.StatusDoacao.Entregue);
            TopCidades = await _db.Solicitacoes
                .Where(s => s.Status == "ativa")
                .GroupBy(s => s.Cidade)
                .Select(g => new { Cidade = g.Key, Total = g.Count() })
                .OrderByDescending(x => x.Total)
                .Take(5)
                .ToDictionaryAsync(x => x.Cidade ?? "Desconhecida", x => x.Total);
        }
    }
}