using KPM.Application.DTOs.Department;
using KPM.Infrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KPM.Application.Features.Department
{
  internal class DepartmentService : IDepartmentService
  {
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DepartmentService> _logger;

    public DepartmentService(ApplicationDbContext context, ILogger<DepartmentService> logger)
    {
      _context = context;
      _logger = logger;
    }
    public async Task<List<DepartmentDTO>> GetAllAsync()
    {
      var departments = await _context.Departments.ToListAsync();
      return departments.Adapt<List<DepartmentDTO>>();

    }
    public async Task<DepartmentDTO?> GetByIdAsync(Guid id)
    {
      var department = await _context.Departments.FindAsync(id);
      if (department == null)
      {
        _logger.LogWarning("Department {Id} not found", id);
        return null;
      }
      return department?.Adapt<DepartmentDTO>();
    }
    public async Task<DepartmentDTO> CreateAsync(CreateDepartmentDTO createDto)
    {
      var department = createDto.Adapt<Domain.Department>();
      department.Id = Guid.NewGuid();
      department.CreatedDate = DateTime.Now;
      department.ModifiedDate = DateTime.Now;

      _context.Departments.Add(department);
      await _context.SaveChangesAsync();
      _logger.LogInformation("Created Department{DepartmentId} with name {Name}", department.Id, department.Name);

      return department.Adapt<DepartmentDTO>();
    }

    public async Task<DepartmentDTO?> PatchAsync(Guid id, UpdateDepartmentDTO updateDto)
    {
      var department = await _context.Departments.FindAsync(id);
      if (department == null)
      {
        _logger.LogWarning("Attempted to patch Department {Id} but it was not found", id);
        return null;
      }

      // Only apply fields that were actually provided (non-null).
      // Mapster respects this automatically via .Adapt(existingInstance)
      // as long as the source DTO properties are nullable.
      updateDto.Adapt(department);
      department.ModifiedDate = DateTime.Now;

      await _context.SaveChangesAsync();
      _logger.LogInformation("Patched Department {DepartmentId}", department.Id);

      return department.Adapt<DepartmentDTO>();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
      var department = await _context.Departments.FindAsync(id);
      if (department == null)
      {
        _logger.LogWarning("Attempted to delete Department {Id} but it was not found", id);
        return false;
      }

      _context.Departments.Remove(department);
      await _context.SaveChangesAsync();
      _logger.LogInformation("Deleted Department {DepartmentId}", id);

      return true;
    }
  }
}
