
using System.Collections.Generic;
using System.Linq;
using API.Models;

namespace API.Data
{
    public class ChatData
    {
        private readonly List<ChatMessage> _messages = new();

        public IEnumerable<ChatMessage> GetMessages(string from, string to)
        {
            return _messages.Where(m => (m.From == from && m.To == to) || (m.From == to && m.To == from));
        }

        public void AddMessage(ChatMessage message)
        {
            _messages.Add(message);
        }
    }
}
