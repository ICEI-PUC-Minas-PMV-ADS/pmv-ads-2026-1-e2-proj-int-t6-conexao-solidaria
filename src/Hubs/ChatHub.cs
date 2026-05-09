using Microsoft.AspNetCore.SignalR;

namespace ConexaoSolidaria.Hubs
{
    public class ChatHub : Hub
    {
        public async Task EnviarMensagem(string chatId, string usuario, string mensagem)
        {
            // Envia a mensagem para todos que estiverem conectados nesse "chatId"
            await Clients.Group(chatId).SendAsync("ReceberMensagem", usuario, mensagem);
        }

        // colocar o usuário na sala correta do chat
        public async Task EntrarNoChat(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }
    }
}