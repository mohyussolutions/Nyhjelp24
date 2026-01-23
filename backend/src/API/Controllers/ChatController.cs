using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private static List<ChatMessage> Messages = new List<ChatMessage>();

        [HttpGet("{user1}/{user2}")]
        public ActionResult<IEnumerable<ChatMessage>> GetMessages(string user1, string user2)
        {
            var chat = Messages.Where(m => (m.From == user1 && m.To == user2) || (m.From == user2 && m.To == user1)).ToList();
            return Ok(chat);
        }

        [HttpPost]
        public ActionResult PostMessage([FromBody] ChatMessage message)
        {
            Messages.Add(message);
            return Ok();
        }
    }
}
