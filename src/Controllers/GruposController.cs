using Microsoft.AspNetCore.Mvc;

namespace ConexaoSolidaria.Controllers
{
    public class GruposController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Criar()
        {
            return View();
        }
    }
}
