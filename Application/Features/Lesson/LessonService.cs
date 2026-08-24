using Microsoft.EntityFrameworkCore;
using Mapster;
using KPM.Infrastructure;
using KPM.Application.DTOs.Lesson;
using Microsoft.Extensions.Logging;

namespace KPM.Application.Features.Lesson
{
  public class LessonService : ILessonService
  {
    private readonly ApplicationDbContext _context;
    private readonly ILogger<LessonService> _logger;

    public LessonService(ApplicationDbContext context, ILogger<LessonService> logger)
    {
      _context = context;
      _logger = logger;
    }

    public async Task<List<LessonDto>> GetAllAsync()
    {
      _logger.LogInformation("Fetching all lessons");
      var lessons = await _context.Lessons.ToListAsync();
      return lessons.Adapt<List<LessonDto>>();
    }

    public async Task<LessonDto?> GetByIdAsync(Guid id)
    {
      var lesson = await _context.Lessons.FindAsync(id);
      if (lesson == null)
      {
        _logger.LogWarning("Lesson {Id} not found", id);
        return null;
      }
      return lesson.Adapt<LessonDto>();
    }

    public async Task<LessonDto> CreateAsync(CreateLessonDTO createDto)
    {
      var lesson = createDto.Adapt<KPM.Domain.Lesson>();
      lesson.Id = Guid.NewGuid();
      lesson.CreatedDate = DateTime.Now;
      lesson.ModifiedDate = DateTime.Now;

      _context.Lessons.Add(lesson);
      await _context.SaveChangesAsync();

      _logger.LogInformation("Created lesson {LessonId} - {Title}", lesson.Id, lesson.Title);
      return lesson.Adapt<LessonDto>();
    }

    public async Task<LessonDto?> PatchAsync(Guid id, UpdateLessonDTO updateDto)
    {
      var lesson = await _context.Lessons.FindAsync(id);
      if (lesson == null)
      {
        _logger.LogWarning("Attempted to patch Lesson {Id} but it was not found", id);
        return null;
      }

      updateDto.Adapt(lesson);
      lesson.ModifiedDate = DateTime.Now;

      await _context.SaveChangesAsync();
      _logger.LogInformation("Patched lesson {LessonId}", lesson.Id);

      return lesson.Adapt<LessonDto>();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
      var lesson = await _context.Lessons.FindAsync(id);
      if (lesson == null)
      {
        _logger.LogWarning("Attempted to delete Lesson {Id} but it was not found", id);
        return false;
      }

      _context.Lessons.Remove(lesson);
      await _context.SaveChangesAsync();

      _logger.LogInformation("Deleted lesson {LessonId}", id);
      return true;
    }
  }
}
