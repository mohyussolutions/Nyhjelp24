using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ChatService : IChatService
    {
        private readonly AppDbContext _context;

        public ChatService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ChatMessage> SaveMessageAsync(ChatMessage message)
        {
            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<IEnumerable<ChatMessage>> GetMessagesAsync(string user1, string user2)
        {
            return await _context.ChatMessages
                .Where(m =>
                    (m.From == user1 && m.To == user2) ||
                    (m.From == user2 && m.To == user1))
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }
    }
}