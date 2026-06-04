
using ConexaoSolidaria.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConexaoSolidaria.Controllers
{
    public class GruposController : Controller
    {
        private readonly IBlobStorageService _blob;

        public GruposController(IBlobStorageService blob)
        {
            _blob = blob;
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

        [HttpPost]
        public async Task<IActionResult> Criar(IFormFile? FotoGrupo)
        {
            string? fotoUrl = null;

            if (FotoGrupo != null && FotoGrupo.Length > 0)
            {
                if (FotoGrupo.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError(
                        "FotoGrupo",
                        "Imagem muito grande (máx. 5 MB).");

                    return View();
                }

                using var stream = FotoGrupo.OpenReadStream();

                fotoUrl = await _blob.UploadAsync(
                    stream,
                    FotoGrupo.FileName,
                    FotoGrupo.ContentType);
            }

            TempData["FotoUrl"] = fotoUrl;

            return RedirectToAction(nameof(Index));
        }
    }
}
