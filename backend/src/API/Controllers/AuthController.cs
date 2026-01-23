using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly API.Services.IUserService _userService;

        public AuthController(API.Services.IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userService.LoginAsync(loginDto.Email, loginDto.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { Token = "dummy_token", User = user });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = await _userService.RegisterAsync(registerDto.Name, registerDto.Email, registerDto.Password);
            return Ok(user);
        }
    }

    public class LoginDto { public required string Email { get; set; } public required string Password { get; set; } }
    public class RegisterDto { public required string Name { get; set; } public required string Email { get; set; } public required string Password { get; set; } }
}
