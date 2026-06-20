using Microsoft.AspNetCore.Mvc;
using ConexaoSolidaria.Data; 
using ConexaoSolidaria.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace ConexaoSolidaria.Controllers
{
    public class GruposController : Controller
    {
        private readonly AppDbContext _context;

private readonly UserManager<Usuario> _userManager;

public GruposController(AppDbContext context, UserManager<Usuario> userManager)
{
    _context = context;
    _userManager = userManager;
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
        public IActionResult Editar(GrupoApoio grupo)
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
            var grupos = _context.Grupos.ToList();
            return View(grupos);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View("NovoG", new GrupoApoio());
        }
   /*     [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Criar(Grupo input, IFormFile? Foto)
{
    if (string.IsNullOrWhiteSpace(input.Nome))
    {
        ModelState.AddModelError("Nome", "Informe o nome do grupo.");
        return View("NovoG", input);
    }

    string? fotoUrl = null;

    if (Foto != null && Foto.Length > 0)
    {
        var pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "grupos");
        Directory.CreateDirectory(pasta);

        var nomeArquivo = $"{Guid.NewGuid()}{Path.GetExtension(Foto.FileName)}";
        var caminho = Path.Combine(pasta, nomeArquivo);

        using var stream = new FileStream(caminho, FileMode.Create);
        await Foto.CopyToAsync(stream);

        fotoUrl = $"/uploads/grupos/{nomeArquivo}";
    }

    var grupo = new GrupoApoio
    {
        Nome     = input.Nome.Trim(),
        Descricao = (input.Descricao ?? string.Empty).Trim(),
        FotoUrl  = fotoUrl   // salva o caminho no banco
    };

    _context.Grupos.Add(grupo);
    _context.SaveChanges();
    return RedirectToAction("Lista");
}
*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(GrupoApoio input)
        {
            if (string.IsNullOrWhiteSpace(input.Nome))
            {
                ModelState.AddModelError("Nome", "Informe o nome do grupo.");
                return View("NovoG", input);
            }

            var grupo = new GrupoApoio
            {
                Nome = input.Nome.Trim(),
                Descricao = (input.Descricao ?? string.Empty).Trim()
                 TipoAtividade = input.TipoAtividade ?? "Misto",
                Cidade = input.Cidade?.Trim(),
                Estado = input.Estado,
                Publico = input.Publico,
                CriadoEm = DateTime.UtcNow,
                CriadorId = _userManager.GetUserId(User) ?? string.Empty
            };
            _context.Grupos.Add(grupo);
            _context.SaveChanges();
            return RedirectToAction("Lista");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConvidarUsuario(int grupoId, int usuarioId)
        {
            return Json(new { sucesso = true, mensagem = "Convite enviado com sucesso!" });
        }
    }
}
