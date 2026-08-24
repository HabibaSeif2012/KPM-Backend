using KPM.Application.DTOs.Auth;
namespace KPM.Application.Features.Auth
{
  public interface IAuthService
  {
    Task<string?> RegisterAsync(RegisterDTO dto);
    Task<string?> LoginAsync(LoginDTO dto);
  }
}
