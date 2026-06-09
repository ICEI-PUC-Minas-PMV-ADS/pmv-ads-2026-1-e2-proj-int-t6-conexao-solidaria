using ConexaoSolidaria.Data;
using ConexaoSolidaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            // Busca todos os chats
            var chats = await _context.Chats
                .Include(c => c.Doacao).ThenInclude(d => d!.Solicitacao).ThenInclude(s => s!.Usuario)
                .Include(c => c.Doacao).ThenInclude(d => d!.Doador)
                .Include(c => c.Mensagens)
                .Where(c => c.Doacao!.DoadorId == userId || c.Doacao.Solicitacao!.UsuarioId == userId)
                .ToListAsync();

            return View(chats);
        }

        [HttpGet]
        public async Task<IActionResult> ValidarAcessoChat(int solicitacaoId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Challenge();

            var doacao = await _context.Doacoes
                .FirstOrDefaultAsync(d => d.SolicitacaoId == solicitacaoId && d.DoadorId == userId);

            if (doacao == null)
            {
                TempData["Erro"] = "Para enviar uma mensagem, você precisa primeiro oferecer ajuda nesta solicitação.";
                return RedirectToPage("/Solicitacoes/Detalhes", new { id = solicitacaoId });
            }

            return RedirectToAction("Index", new { doacaoId = doacao.Id });
        }

        // Abrir um Chat específico
        public async Task<IActionResult> Index(int doacaoId)
        {
            var chat = await _context.Chats
                .Include(c => c.Doacao).ThenInclude(d => d!.Solicitacao)
                .Include(c => c.Mensagens).ThenInclude(m => m.Remetente)
                .FirstOrDefaultAsync(c => c.DoacaoId == doacaoId);

            if (chat == null)
            {
                var doacaoExiste = await _context.Doacoes.AnyAsync(d => d.Id == doacaoId);
                if (!doacaoExiste)
                {
                    return NotFound("Doação não encontrada. É necessário oferecer ajuda antes de iniciar o chat.");
                }

                chat = new ChatApoio { DoacaoId = doacaoId };
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