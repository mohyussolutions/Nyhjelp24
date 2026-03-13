using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("{user1}/{user2}")]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetMessages(string user1, string user2)
        {
            var chat = await _chatService.GetMessagesAsync(user1, user2);
            return Ok(chat);
        }

        [HttpPost]
        public async Task<ActionResult> PostMessage([FromBody] ChatMessage message)
        {
            await _chatService.SaveMessageAsync(message);
            return Ok();
        }
    }
}
