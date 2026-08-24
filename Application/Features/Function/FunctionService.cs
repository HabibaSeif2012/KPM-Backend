using KPM.Application.DTOs.Function;
using KPM.Application.Features.Department;
using KPM.Domain;
using KPM.Infrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPM.Application.Features.Function
{
  public class FunctionService : IFunctionService
  {
    private readonly ApplicationDbContext _context;
    private readonly ILogger<FunctionService> _logger;

    public FunctionService(ApplicationDbContext context, ILogger<FunctionService> logger)
    {
      _context = context;
      _logger = logger;
    }
    public async Task<List<FunctionDTO>> GetAllAsync()
    {
      var functions = await _context.Functions.ToListAsync();
      return functions.Adapt<List<FunctionDTO>>();
    }
    public async Task<FunctionDTO?> GetByIdAsync(Guid id)
    {
      var function = await _context.Functions.FindAsync(id);
      if (function == null) {
        _logger.LogWarning("Function {Id} not found", id);

      }
      return function?.Adapt<FunctionDTO>();
    }
    public async Task<FunctionDTO> CreateAsync(CreateFunctionDTO createDto)
    {
      var function = createDto.Adapt<KPM.Domain.Function>();
      function.Id = Guid.NewGuid();
      function.CreatedDate = DateTime.Now;
      function.LastModifiedDate = DateTime.Now;

      _context.Functions.Add(function);
      await _context.SaveChangesAsync();
      _logger.LogInformation("Created Function{DepartmentId} with name {Name}", function.Id,function.Name);

      return function.Adapt<FunctionDTO>();
    }

    public async Task<FunctionDTO?> PatchAsync(Guid id, UpdateFunctionDTO updateDto)
    {
      var function = await _context.Functions.FindAsync(id);
      if (function == null)
      {
        _logger.LogWarning("Attempted to patch Function {Id} but it was not found", id);
        return null;
      }

      updateDto.Adapt(function);
      function.LastModifiedDate = DateTime.Now;

      await _context.SaveChangesAsync();
      _logger.LogInformation("Patched Function {FunctionId}", function.Id);

      return function.Adapt<FunctionDTO>();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
      var function = await _context.Functions.FindAsync(id);
      if (function == null)
      {
        _logger.LogWarning("Attempted to delete Function {Id} but it was not found", id);
        return false;
      }

      _context.Functions.Remove(function);
      await _context.SaveChangesAsync();
      _logger.LogInformation("Deleted Function {FunctionId}", id);

      return true;
    }
  }
}
