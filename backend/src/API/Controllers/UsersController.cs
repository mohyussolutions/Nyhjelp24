using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly API.Services.IUserService _userService;

        public UsersController(API.Services.IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadImage(int id, [FromForm] IFormFile file)
        {
            // Implementation for file upload would go here
            // distinct service or logic to save file to disk/blob
            // For now, mock success
            return Ok(new { message = "Image uploaded successfully" });
        }
    }
}
