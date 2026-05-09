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

        // Histórico
        public async Task<IActionResult> Lista()
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId)) return Challenge();

            // Busca chats onde o usuário é o Doador OU o autor da Solicitação
            var chats = await _context.Chats
                .Include(c => c.Doacao).ThenInclude(d => d!.Solicitacao)
                .Include(c => c.Doacao).ThenInclude(d => d!.Doador)
                .Include(c => c.Doacao).ThenInclude(d => d!.Solicitacao).ThenInclude(s => s!.Usuario)
                .Include(c => c.Mensagens)
                .Where(c => c.Doacao!.DoadorId == userId || c.Doacao.Solicitacao!.UsuarioId == userId)
                .OrderByDescending(c => c.Mensagens.Max(m => m.EnviadaEm))
                .ToListAsync();

            return View(chats);
        }

        // Abre uma conversa específica
        public async Task<IActionResult> Index(int doacaoId)
        {
            var chat = await _context.Chats
                .Include(c => c.Doacao).ThenInclude(d => d!.Solicitacao)
                .Include(c => c.Mensagens).ThenInclude(m => m.Remetente)
                .FirstOrDefaultAsync(c => c.DoacaoId == doacaoId);

            if (chat == null)
            {
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