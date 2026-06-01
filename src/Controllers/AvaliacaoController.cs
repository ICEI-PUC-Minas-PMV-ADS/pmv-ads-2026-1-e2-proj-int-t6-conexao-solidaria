using Microsoft.AspNetCore.Mvc;
using ConexaoSolidaria.Models;

namespace ConexaoSolidaria.Controllers
{
    public class AvaliacaoController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Salvar(int nota, string comentario)
        {
            // Aqui depois você salva no banco

            TempData["Sucesso"] =
                "Avaliação enviada com sucesso!";

            return RedirectToAction("Index");
        }
    }
}
