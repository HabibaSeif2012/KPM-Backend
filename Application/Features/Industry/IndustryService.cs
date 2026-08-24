using Microsoft.EntityFrameworkCore;
using Mapster;
using KPM.Infrastructure;
using KPM.Application.DTOs.Industry;
using Microsoft.Extensions.Logging;
using KPM.Application.Features.Auth;
namespace KPM.Application.Features.Industry
{
  public class IndustryService : IIndustryService
  {
    private readonly ApplicationDbContext _context;
    private readonly ILogger<IndustryService> _logger;

    public IndustryService(ApplicationDbContext context, ILogger<IndustryService> logger)
    {
      _context = context;
      _logger = logger;
    }

    public async Task<List<IndustryDTO>> GetAllAsync()
    {
      _logger.LogInformation("Fetching all industries");
      var industries = await _context.Industries.ToListAsync();
      return industries.Adapt<List<IndustryDTO>>();
    }

    public async Task<IndustryDTO?> GetByIdAsync(Guid id)
    {
      var industry = await _context.Industries.FindAsync(id);
      if (industry == null)
      {
        _logger.LogWarning("Industry {Id} not found", id);
        return null;
      }
      return industry.Adapt<IndustryDTO>();
    }

    public async Task<IndustryDTO> CreateAsync(CreateIndustryDTO createDto)
    {
      var industry = createDto.Adapt<KPM.Domain.Industry>();
      industry.Id = Guid.NewGuid();
      industry.CreatedDate = DateTime.Now;
      industry.ModifiedDate = DateTime.Now;

      _context.Industries.Add(industry);
      await _context.SaveChangesAsync();

      _logger.LogInformation("Created industry {IndustryId} - {Name}", industry.Id, industry.Name);
      return industry.Adapt<IndustryDTO>();
    }

    public async Task<IndustryDTO?> PatchAsync(Guid id, UpdateIndustryDTO updateDto)
    {
      var industry = await _context.Industries.FindAsync(id);
      if (industry == null)
      {
        _logger.LogWarning("Attempted to patch Industry {Id} but it was not found", id);
        return null;
      }

      updateDto.Adapt(industry);
      industry.ModifiedDate = DateTime.Now;

      await _context.SaveChangesAsync();
      _logger.LogInformation("Patched industry {IndustryId}", industry.Id);

      return industry.Adapt<IndustryDTO>();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
      var industry = await _context.Industries.FindAsync(id);
      if (industry == null)
      {
        _logger.LogWarning("Attempted to delete Industry {Id} but it was not found", id);
        return false;
      }

      _context.Industries.Remove(industry);
      await _context.SaveChangesAsync();
      _logger.LogInformation("Deleted industry {IndustryId}", id);

      return true;
    }
  }
}
