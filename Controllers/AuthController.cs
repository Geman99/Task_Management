using Microsoft.AspNetCore.Mvc;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            var user = _authService.ValidateUser(login.Username, login.Password);
            if (user == null) return Unauthorized("Invalid credentials");

            var token = _authService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
