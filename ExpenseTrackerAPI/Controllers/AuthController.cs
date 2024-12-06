using ExpenseTrackerAPI.Models.DTO;
using ExpenseTrackerAPI.services;
using ExpenseTrackerAPI.services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var result = await _authService.RegisterUserAsync(model);
            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully!" });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var token = await _authService.LoginUserAsync(model);
            if (token != null)
            {
                return Ok(new { Message = "Login successful!", Token = token });
            }

            return Unauthorized(new { Message = "Invalid credentials" });
        }
    }

}
