using KPM.Application.DTOs.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPM.Application.Features.Auth;

namespace KPM.Application.Features.Department
{
  public interface IDepartmentService
  {
    Task<List<DepartmentDTO>> GetAllAsync();
    Task<DepartmentDTO?> GetByIdAsync(Guid Id);
    Task<DepartmentDTO> CreateAsync(CreateDepartmentDTO createDto);
    Task<DepartmentDTO?> PatchAsync(Guid id, UpdateDepartmentDTO updateDto);
    Task<bool> DeleteAsync(Guid id);
  }
}
