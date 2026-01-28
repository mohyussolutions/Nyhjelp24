using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using System;

namespace API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessage(string from, string to, string message)
        {
            var chatMessage = new ChatMessage
            {
                From = from,
                To = to,
                Message = message,
                Timestamp = DateTime.UtcNow
            };
            await _chatService.SaveMessageAsync(chatMessage);
            await Clients.User(from).SendAsync("ReceiveMessage", chatMessage);
            await Clients.User(to).SendAsync("ReceiveMessage", chatMessage);
        }

        public async Task GetMessageHistory(string user1, string user2)
        {
            var messages = await _chatService.GetMessagesAsync(user1, user2);
            await Clients.Caller.SendAsync("ReceiveMessageHistory", messages);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
    }
}
