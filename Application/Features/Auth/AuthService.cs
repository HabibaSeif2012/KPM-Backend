using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using KPM.Application.DTOs.Auth;
using KPM.Application.Features.Auth;

namespace KPM.Application.Features.Auth
{
  public class AuthService : IAuthService
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _configuration = configuration;
    }

    public async Task<string?> RegisterAsync(RegisterDTO dto)
    {
      var user = new IdentityUser { UserName = dto.Email, Email = dto.Email };
      var result = await _userManager.CreateAsync(user, dto.Password);
      return result.Succeeded ? "User registered successfully" : null;
    }

    public async Task<string?> LoginAsync(LoginDTO dto)
    {
      var user = await _userManager.FindByEmailAsync(dto.Email);
      if (user == null) return null;

      var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
      if (!result.Succeeded) return null;

      return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(IdentityUser user)
    {
      var claims = new[]
      {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Email, user.Email)
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      string? audience = _configuration["Jwt:Audience"];
      var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: audience,
        claims: claims,
        expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiresInMinutes"])),
        signingCredentials: creds
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
