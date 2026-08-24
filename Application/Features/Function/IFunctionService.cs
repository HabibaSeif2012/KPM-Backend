using KPM.Application.DTOs.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPM.Application.Features.Auth;

namespace KPM.Application.Features.Function
{
  public interface IFunctionService
  {
    Task<List<FunctionDTO>> GetAllAsync();
    Task<FunctionDTO?> GetByIdAsync(Guid id);
    Task<FunctionDTO> CreateAsync(CreateFunctionDTO createDto);
    Task<FunctionDTO?> PatchAsync(Guid id, UpdateFunctionDTO updateDto);
    Task<bool> DeleteAsync(Guid id);
  }
}
