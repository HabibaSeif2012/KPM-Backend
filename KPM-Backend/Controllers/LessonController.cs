using Microsoft.AspNetCore.Mvc;
using KPM.Application.Features.Lesson;
using KPM.Application.DTOs.Lesson;

namespace KPM_Backend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class LessonController : ControllerBase
  {
    private readonly ILessonService _lessonService;

    public LessonController(ILessonService lessonService)
    {
      _lessonService = lessonService;
    }

    [HttpGet]
    public async Task<ActionResult<List<LessonDto>>> GetAll()
    {
      var lessons = await _lessonService.GetAllAsync();
      return Ok(lessons);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LessonDto>> GetById(Guid id)
    {
      var lesson = await _lessonService.GetByIdAsync(id);
      if (lesson == null) return NotFound();
      return Ok(lesson);
    }

    [HttpPost]
    public async Task<ActionResult<LessonDto>> Create(CreateLessonDTO createDto)
    {
      var result = await _lessonService.CreateAsync(createDto);
      return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
  }
}
