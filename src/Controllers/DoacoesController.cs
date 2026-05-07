using System;
using System.Linq;
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
        public IActionResult Create()
        {
            var doadoresDisponiveis = _context.Users
                .Select(u => new { Id = u.Id, Exibicao = u.Email })
                .ToList();

            var solicitacoesDisponiveis = _context.Solicitacoes
                .Select(s => new { Id = s.Id, Exibicao = s.Titulo })
                .ToList();

            ViewData["ListaDoadores"] = new SelectList(doadoresDisponiveis, "Id", "Exibicao");
            ViewData["ListaSolicitacoes"] = new SelectList(solicitacoesDisponiveis, "Id", "Exibicao");

            return View();
        }

        // POST: Doacoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DoadorId,SolicitacaoId,ItensDoados,DataDoacao,Status")] Doacao doacao)
        {
            if (ModelState.IsValid)
            {
                doacao.DataDoacao = DateTime.Now;
                doacao.Status = StatusDoacao.Pendente;

                _context.Add(doacao);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            var doadoresDisponiveis = _context.Users
                .Select(u => new { Id = u.Id, Exibicao = u.Email })
                .ToList();

            var solicitacoesDisponiveis = _context.Solicitacoes
                .Select(s => new { Id = s.Id, Exibicao = s.Titulo })
                .ToList();

            ViewData["ListaDoadores"] = new SelectList(doadoresDisponiveis, "Id", "Exibicao", doacao.DoadorId);
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