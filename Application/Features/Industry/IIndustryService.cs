using KPM.Application.DTOs.Function;
using KPM.Application.DTOs.Industry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPM.Application.Features.Industry
{
  public interface IIndustryService
  {
    Task<List<IndustryDTO>> GetAllAsync();
    Task<IndustryDTO?> GetByIdAsync(Guid id);
    Task<IndustryDTO> CreateAsync(CreateIndustryDTO createDto);
    Task<IndustryDTO?> PatchAsync(Guid id, UpdateIndustryDTO updateDto);
    Task<bool> DeleteAsync(Guid id);
  }
}
