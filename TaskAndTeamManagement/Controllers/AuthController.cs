using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAndTeamManagement.Core.Interface.Service;
using TaskAndTeamManagement.Infrascture.Implementation.Service;

namespace TaskAndTeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService _userService, JwtService _jwtService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userService.GetByEmailAsync(email);
            if (user == null) return Unauthorized("Invalid email or password");

            // Simple password check (You should use a proper hashing mechanism in production)
            if (user.PasswordHash != password) return Unauthorized("Invalid email or password");

            var token = _jwtService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}
