using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ConexaoSolidaria.Models;

namespace ConexaoSolidaria.Controllers
{
    public class GruposController : grupos
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        // GET: /Grupos/Compartilhar/5
        public ActionResult Compartilhar(int id)
        {
            var model = new CompartilharGrupoViewModel
            {
                GrupoId = id,
                NomeGrupo = "Vítimas de Enchentes",
                Descricao = "Grupo de apoio para vítimas de enchentes",
                Tipo = "Público",
                TotalMembros = 48,
                LinkGrupo = "conexaosolidaria.app/grupos/enchentes-mg",
                QrCodeUrl = "https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=https://conexaosolidaria.app/grupos/enchentes-mg",
                UsuariosSugeridos = new List<UsuarioConviteViewModel>
                {
                    new UsuarioConviteViewModel { UsuarioId = 1, Nome = "Luisa Ferreira", Papel = "Voluntária", Cidade = "BH",        Estado = "MG", Iniciais = "LF" },
                    new UsuarioConviteViewModel { UsuarioId = 2, Nome = "Rodrigo Silva",  Papel = "Doador",     Cidade = "Petrópolis", Estado = "RJ", Iniciais = "RS" },
                    new UsuarioConviteViewModel { UsuarioId = 3, Nome = "Beatriz Melo",   Papel = "Vítima",     Cidade = "RJ",         Estado = "RJ", Iniciais = "BM" },
                    new UsuarioConviteViewModel { UsuarioId = 4, Nome = "Carlos Pinto",   Papel = "Voluntário", Cidade = "NJ",         Estado = "RJ", Iniciais = "CP" },
                    new UsuarioConviteViewModel { UsuarioId = 5, Nome = "Maria Ribeiro",  Papel = "Vítima",     Cidade = "Petrópolis", Estado = "RJ", Iniciais = "MR" },
                }
            };
            return View(model);
        }

        [HttpPost]
        public JsonResult ConvidarUsuario(ConvidarUsuarioRequest request)
        {
            return Json(new { sucesso = true, mensagem = "Convite enviado com sucesso!" });
        }
    }
}
