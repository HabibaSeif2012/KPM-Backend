using KPM.Application.DTOs.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPM.Application.Features.Auth;
namespace KPM.Application.Features.Lesson
{
  public interface ILessonService
  {
    Task<List<LessonDto>> GetAllAsync();
    Task<LessonDto?> GetByIdAsync(Guid id);
    Task<LessonDto> CreateAsync(CreateLessonDTO createDto);
    Task<LessonDto?> PatchAsync(Guid id, UpdateLessonDTO updateDto);
    Task<bool> DeleteAsync(Guid id);
  }
}
