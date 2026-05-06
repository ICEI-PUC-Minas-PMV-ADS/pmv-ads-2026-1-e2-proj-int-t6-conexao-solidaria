using ConexaoSolidaria.Controllers; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            var doacoes = _context.Doacoes
                .Include(d => d.Doador)
                .Include(d => d.PedidoAjuda);

            return View(await doacoes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doacao = await _context.Doacoes
                .Include(d => d.Doador)
                .Include(d => d.PedidoAjuda)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (doacao == null)
            {
                return NotFound();
            }

            return View(doacao);
        }

        public IActionResult Create()
        {
            ViewData["DoadorId"] = new SelectList(_context.Usuarios, "Id", "Nome");
            ViewData["PedidoAjudaId"] = new SelectList(_context.PedidosAjuda, "Id", "Descricao");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DoadorId,PedidoAjudaId,ItensDoados,DataDoacao,Status")] Doacao doacao)
        {
            if (ModelState.IsValid)
            {
                doacao.DataDoacao = DateTime.Now; 
                doacao.Status = StatusDoacao.Pendente; 

                _context.Add(doacao);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["DoadorId"] = new SelectList(_context.Usuarios, "Id", "Nome", doacao.DoadorId);
            ViewData["PedidoAjudaId"] = new SelectList(_context.PedidosAjuda, "Id", "Descricao", doacao.PedidoAjudaId);
            return View(doacao);
        }

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