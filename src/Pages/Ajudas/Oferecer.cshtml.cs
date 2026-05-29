using System.Security.Claims;
using ConexaoSolidaria.Data;
using ConexaoSolidaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConexaoSolidaria.Pages.Ajudas;

[Authorize]
public class OfereceModel : PageModel
{
    private readonly AppDbContext _db;
    public OfereceModel(AppDbContext db) => _db = db;

    public Solicitacao? Solicitacao { get; set; }

    [BindProperty]
    public OfertaInputModel Input { get; set; } = new();

    public class OfertaInputModel
    {
        public string Modalidade { get; set; } = "doacao";
        public bool ItemAlimentos { get; set; }
        public bool ItemRoupas    { get; set; }
        public bool ItemHigiene   { get; set; }
        public bool ItemColchoes  { get; set; }
        public string? Mensagem   { get; set; }
        public int SolicitacaoId  { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(int solicitacaoId)
    {
        Solicitacao = await _db.Solicitacoes
            .Include(s => s.Usuario)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == solicitacaoId);

        if (Solicitacao == null) return NotFound();

        Input.SolicitacaoId = solicitacaoId;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Solicitacao = await _db.Solicitacoes
            .Include(s => s.Usuario)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == Input.SolicitacaoId);

        if (Solicitacao == null) return NotFound();
        if (!ModelState.IsValid) return Page();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Challenge();

        var jaOfertou = await _db.OfertasAjuda
            .AnyAsync(o => o.SolicitacaoId == Input.SolicitacaoId
                        && o.VoluntarioId  == userId
                        && o.Status        != "cancelada");
        if (jaOfertou)
        {
            ModelState.AddModelError(string.Empty,
                "Voce ja registrou uma oferta de ajuda para esta solicitacao.");
            return Page();
        }

        // Montar itens como mensagem complementar
        var itens = new List<string>();
        if (Input.ItemAlimentos) itens.Add("Alimentos nao-pereciveis");
        if (Input.ItemRoupas)    itens.Add("Roupas (G/GG)");
        if (Input.ItemHigiene)   itens.Add("Itens de higiene pessoal");
        if (Input.ItemColchoes)  itens.Add("Colchoes / Cobertores");

        var mensagem = itens.Count > 0
            ? $"Itens: {string.Join(", ", itens)}. {Input.Mensagem}".Trim()
            : Input.Mensagem?.Trim();

        var oferta = new OfertaAjuda
        {
            SolicitacaoId = Input.SolicitacaoId,
            VoluntarioId  = userId,
            Modalidade    = Input.Modalidade,
            Mensagem      = mensagem,
            Status        = "confirmada",
            CriadaEm      = DateTime.UtcNow,
        };

        _db.OfertasAjuda.Add(oferta);
        await _db.SaveChangesAsync();

        return RedirectToPage("/Ajudas/Confirmada", new { ofertaId = oferta.Id });
    }
}
