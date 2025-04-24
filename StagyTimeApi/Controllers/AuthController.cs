using Microsoft.AspNetCore.Mvc;
using StagyTimeApi.Models;
using StagyTimeApi.Services.Interfaces;

namespace StagyTimeApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        [HttpPost("register")]
        public async Task<ActionResult<User>> Resgister([FromBody] RegisterDto dto)
        {
            try
            {
                var user = await _auth.RegisterAsync(dto.Email, dto.Senha, dto.FirstName, dto.LastName);
                return CreatedAtAction(nameof(Resgister), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginDto dto)
        {
            var user = await _auth.LoginAsync(dto.Email, dto.Senha);
            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });
            return Ok(user);
        }
    }

    public class RegisterDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
