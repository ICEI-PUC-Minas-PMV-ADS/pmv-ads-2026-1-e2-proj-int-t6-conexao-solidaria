using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ConexaoSolidaria.Models;
using ConexaoSolidaria.Data;

namespace ConexaoSolidaria.Controllers
{
    [Authorize]
    public class DoacoesController : Controller
    {
        private readonly AppDbContext _context;

        public DoacoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Doacoes
        public async Task<IActionResult> Index()
        {
            var doacoes = _context.Doacoes
                .Include(d => d.Doador)
                .Include(d => d.Solicitacao);

            return View(await doacoes.ToListAsync());
        }

        // GET: Doacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var doacao = await _context.Doacoes
                .Include(d => d.Doador)
                .Include(d => d.Solicitacao)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (doacao == null) return NotFound();

            return View(doacao);
        }

        // GET: Doacoes/Create (Oferecer Ajuda)
        public IActionResult Create(int? id)
        {
            var solicitacoesDisponiveis = _context.Solicitacoes
                .Where(s => s.Status == "ativa")
                .Select(s => new { Id = s.Id, Exibicao = s.Titulo })
                .ToList();

            ViewData["ListaSolicitacoes"] = new SelectList(solicitacoesDisponiveis, "Id", "Exibicao", id);

            return View();
        }

        // POST: Doacoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SolicitacaoId,ItensDoados,DataDoacao,Status")] Doacao doacao)
        {
            ModelState.Remove("DoadorId");

            if (ModelState.IsValid)
            {
                // Captura o ID de quem está logado no site e joga na doação
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                doacao.DoadorId = userId;

                doacao.DataDoacao = DateTime.Now;
                doacao.Status = StatusDoacao.Pendente;

                _context.Add(doacao);

                var solicitacao = await _context.Solicitacoes.FindAsync(doacao.SolicitacaoId);
                if (solicitacao != null)
                {
                    solicitacao.Status = "em andamento";
                    _context.Update(solicitacao);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var solicitacoesDisponiveis = _context.Solicitacoes
                .Where(s => s.Status == "ativa")
                .Select(s => new { Id = s.Id, Exibicao = s.Titulo })
                .ToList();

            ViewData["ListaSolicitacoes"] = new SelectList(solicitacoesDisponiveis, "Id", "Exibicao", doacao.SolicitacaoId);

            return View(doacao);
        }

        // POST: Doacoes/ConfirmarEntrega/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEntrega(int id)
        {
            var doacao = await _context.Doacoes.FindAsync(id);
            if (doacao != null)
            {
                doacao.Status = StatusDoacao.Entregue;
                _context.Update(doacao);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}