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

        public async Task<IActionResult> Index(int doacaoId)
        {
            // Busca o chat desta doação ou cria um se não existir
            var chat = await _context.Chats
                .Include(c => c.Doacao).ThenInclude(d => d.Solicitacao)
                .Include(c => c.Mensagens).ThenInclude(m => m.Remetente)
                .FirstOrDefaultAsync(c => c.DoacaoId == doacaoId);

            if (chat == null)
            {
                chat = new ChatApoio { DoacaoId = doacaoId };
                _context.Chats.Add(chat);
                await _context.SaveChangesAsync();
            }

            var usuarioAtual = await _userManager.GetUserAsync(User);
            ViewBag.UsuarioAtualId = usuarioAtual?.Id;
            ViewBag.NomeUsuario = usuarioAtual?.NomeCompleto;

            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarMensagem([FromBody] MensagemChat novaMensagem)
        {
            // Salva no banco para que o histórico não suma ao dar F5
            novaMensagem.EnviadaEm = DateTime.Now;
            _context.MensagensChat.Add(novaMensagem);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}