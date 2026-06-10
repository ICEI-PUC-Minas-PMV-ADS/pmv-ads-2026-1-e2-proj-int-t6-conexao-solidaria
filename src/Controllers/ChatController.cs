using ConexaoSolidaria.Data;
using ConexaoSolidaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ConexaoSolidaria.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public ChatController(AppDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Histórico de Conversas
        public async Task<IActionResult> Lista()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Challenge();

            // Busca todos os chats vinculados às Ofertas de Ajuda
            var chats = await _context.Chats
                .Include(c => c.OfertaAjuda).ThenInclude(o => o!.Solicitacao).ThenInclude(s => s!.Usuario)
                .Include(c => c.OfertaAjuda).ThenInclude(o => o!.Voluntario)
                .Include(c => c.Mensagens)
                .Where(c => c.OfertaAjuda!.VoluntarioId == userId || c.OfertaAjuda.Solicitacao!.UsuarioId == userId)
                .ToListAsync();

            return View(chats);
        }

        [HttpGet]
        public async Task<IActionResult> ValidarAcessoChat(int solicitacaoId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Challenge();

            // Busca na tabela OfertasAjuda usando as propriedades corretas
            var oferta = await _context.OfertasAjuda
                .Include(o => o.Solicitacao)
                .FirstOrDefaultAsync(o => o.SolicitacaoId == solicitacaoId && 
                                          (o.VoluntarioId == userId || o.Solicitacao!.UsuarioId == userId));

            if (oferta == null)
            {
                TempData["Erro"] = "Para enviar uma mensagem, você precisa primeiro oferecer ajuda nesta solicitação.";
                return RedirectToPage("/Solicitacoes/Detalhes", new { id = solicitacaoId });
            }

            // Redireciona passando o parâmetro correto para a Action Index
            return RedirectToAction("Index", new { ofertaId = oferta.Id });
        }

        // Abrir um Chat específico baseado na Oferta de Ajuda
        public async Task<IActionResult> Index(int ofertaId)
        {
            var chat = await _context.Chats
                .Include(c => c.OfertaAjuda).ThenInclude(o => o!.Solicitacao)
                .Include(c => c.Mensagens).ThenInclude(m => m.Remetente)
                .FirstOrDefaultAsync(c => c.OfertaAjudaId == ofertaId);

            if (chat == null)
            {
                var ofertaExiste = await _context.OfertasAjuda.AnyAsync(o => o.Id == ofertaId);
                if (!ofertaExiste)
                {
                    return NotFound("Oferta de ajuda não encontrada. É necessário oferecer ajuda antes de iniciar o chat.");
                }

                // Cria o chat amarrado à Oferta correspondente
                chat = new ChatApoio { OfertaAjudaId = ofertaId };
                _context.Chats.Add(chat);
                await _context.SaveChangesAsync();
            }

            var usuarioAtual = await _userManager.GetUserAsync(User);
            ViewBag.UsuarioAtualId = usuarioAtual?.Id ?? "";
            ViewBag.NomeUsuario = usuarioAtual?.NomeCompleto ?? "Usuário";

            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarMensagem([FromBody] MensagemChat novaMensagem)
        {
            if (novaMensagem == null) return BadRequest();

            novaMensagem.EnviadaEm = DateTime.Now;
            _context.MensagensChat.Add(novaMensagem);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}