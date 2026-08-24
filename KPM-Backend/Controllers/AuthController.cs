using Microsoft.AspNetCore.Mvc;
using KPM.Application.Features.Auth;
using KPM.Application.DTOs.Auth;

namespace KPM_Backend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
      _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO dto)
    {
      var result = await _authService.RegisterAsync(dto);
      return result != null ? Ok(result) : BadRequest("Registration failed");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO dto)
    {
      var token = await _authService.LoginAsync(dto);
      return token != null ? Ok(new { token }) : Unauthorized("Invalid credentials");
    }
  }
}
