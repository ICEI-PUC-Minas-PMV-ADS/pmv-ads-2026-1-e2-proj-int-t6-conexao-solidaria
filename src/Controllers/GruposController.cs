using Microsoft.AspNetCore.Mvc;
using ConexaoSolidaria.Data; 
using ConexaoSolidaria.Models;
using Microsoft.EntityFrameworkCore;

namespace ConexaoSolidaria.Controllers
{
    public class GruposController : Controller
    {
        private readonly AppDbContext _context;

        public GruposController(AppDbContext context)
        {
            _context = context;
        }

        // TELA 09a: Detalhes
        public IActionResult Detalhes(int id)
        {
            var grupo = _context.Grupos.Find(id);
            if (grupo == null) return NotFound();
            return View(grupo);
        }

        // TELA 09b: Editar (GET)
        public IActionResult Editar(int id)
        {
            var grupo = _context.Grupos.Find(id);
            if (grupo == null) return NotFound();
            return View(grupo);
        }

        // TELA 09b: Editar (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                _context.Update(grupo);
                _context.SaveChanges();
                return RedirectToAction("Lista"); // Ou Index
            }
            return View(grupo);
        }

        // TELA 09c: Compartilhar (Mantendo seu modelo fixo para testes, mas conectado ao ID)
        public ActionResult Compartilhar(int id)
        {
            var grupo = _context.Grupos.Find(id);

            var model = new CompartilharGrupoViewModel
            {
                GrupoId = id,
                NomeGrupo = grupo?.Nome ?? "Grupo não encontrado",
                Descricao = grupo?.Descricao ?? "",
                Tipo = "Público",
                TotalMembros = 48,
                LinkGrupo = "conexaosolidaria.app/grupos/enchentes-mg",
                QrCodeUrl = "https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=https://conexaosolidaria.app/grupos/enchentes-mg",
                UsuariosSugeridos = new List<UsuarioConviteViewModel>
                {
                    new UsuarioConviteViewModel { UsuarioId = 1, Nome = "Luisa Ferreira", Papel = "Voluntária", Cidade = "BH", Estado = "MG", Iniciais = "LF" },
                    new UsuarioConviteViewModel { UsuarioId = 2, Nome = "Rodrigo Silva", Papel = "Doador", Cidade = "Petrópolis", Estado = "RJ", Iniciais = "RS" },
                    new UsuarioConviteViewModel { UsuarioId = 3, Nome = "Beatriz Melo", Papel = "Vítima", Cidade = "RJ", Estado = "RJ", Iniciais = "BM" },
                    new UsuarioConviteViewModel { UsuarioId = 4, Nome = "Carlos Pinto", Papel = "Voluntário", Cidade = "NJ", Estado = "RJ", Iniciais = "CP" },
                    new UsuarioConviteViewModel { UsuarioId = 5, Nome = "Maria Ribeiro", Papel = "Vítima", Cidade = "Petrópolis", Estado = "RJ", Iniciais = "MR" },
                }
            };
            return View(model);
        }

        public IActionResult Lista()
        {
            var grupos = _context.Grupos.ToList();
            return View(grupos);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }
    }
}
