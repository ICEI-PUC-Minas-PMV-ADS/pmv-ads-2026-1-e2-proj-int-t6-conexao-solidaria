using Microsoft.AspNetCore.Mvc;
using Tela_12_projeto.Models;

namespace Tela_12_projeto.Controllers
{
    public class GruposController : Controller
    {
        public IActionResult Detalhes(string? busca, string? acao)
        {
            var vm = CriarViewModel();

            if (!string.IsNullOrWhiteSpace(busca))
            {
                vm.Membros = vm.Membros
                    .Where(m =>
                        m.Nome.Contains(busca, StringComparison.OrdinalIgnoreCase) ||
                        m.Funcao.Contains(busca, StringComparison.OrdinalIgnoreCase) ||
                        m.Cidade.Contains(busca, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                ViewBag.BuscaMembro = busca;
            }

            vm.FeedbackMensagem = acao switch
            {
                "entrar" => "Você entrou no grupo com sucesso.",
                "participar" => "Você entrou no grupo e já pode participar do chat.",
                "compartilhar" => "Link do grupo copiado para compartilhamento.",
                "notificacoes" => "Notificações do grupo ativadas.",
                _ => null
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Acao(string acao)
        {
            return RedirectToAction("Detalhes", new { acao });
        }

        [HttpPost]
        public IActionResult Buscar(string busca)
        {
            return RedirectToAction("Detalhes", new { busca });
        }

        private static DetalhesGrupoViewModel CriarViewModel() => new()
        {
            Grupo = new GrupoDetalhe
            {
                Nome = "Vítimas de Enchentes",
                Tema = "Enchentes",
                Regiao = "MG e RJ",
                TipoAcesso = "Público",
                Descricao = "Apoio emocional para afetados por enchentes em MG e RJ. Moderado por voluntários verificados.",
                TotalMembros = 48,
                TotalMissoes = 127,
                TotalAjudas = 12
            },
            Moderadores = new List<PessoaGrupo>
            {
                new() { Nome = "Ana Santos",  Funcao = "Moderadora principal", Cidade = "Belo Horizonte / MG", Iniciais = "AS" },
                new() { Nome = "João Mendes", Funcao = "Co-moderador",         Cidade = "Rio de Janeiro / RJ", Iniciais = "JM" }
            },
            Membros = new List<PessoaGrupo>
            {
                new() { Nome = "Maria Ribeiro",  Funcao = "Vítima",     Cidade = "Petrópolis", Iniciais = "MR" },
                new() { Nome = "Carlos Pinto",   Funcao = "Voluntário", Cidade = "RJ",         Iniciais = "CP" },
                new() { Nome = "Lúcia Ferreira", Funcao = "Voluntária", Cidade = "MG",         Iniciais = "LF" },
                new() { Nome = "Beatriz Melo",   Funcao = "Vítima",     Cidade = "RJ",         Iniciais = "BM" },
                new() { Nome = "Ana Santos",     Funcao = "Moderadora", Cidade = "MG",         Iniciais = "AS" },
                new() { Nome = "João Mendes",    Funcao = "Voluntário", Cidade = "RJ",         Iniciais = "JM" }
            },
            Mensagens = new List<MensagemGrupo>
            {
                new() { Nome = "Ana Santos",    Texto = "Bem-vindos ao grupo!",      Horario = "08:14", Iniciais = "AS" },
                new() { Nome = "Maria Ribeiro", Texto = "Precisamos de colchões.",   Horario = "09:32", Iniciais = "MR" },
                new() { Nome = "João Mendes",   Texto = "Consigo levar cestas.",     Horario = "09:45", Iniciais = "JM" },
                new() { Nome = "Carlos Pinto",  Texto = "Tenho colchão disponível.", Horario = "10:03", Iniciais = "CP" },
                new() { Nome = "Ana Santos",    Texto = "Mutirão sábado 8h.",        Horario = "10:18", Iniciais = "AS" }
            }
        };
    }
}