using KPM.Application.DTOs.Lesson;
using KPM.Domain;
using KPM.Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KPM_Backend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class LessonController:ControllerBase
  {
    private readonly ApplicationDbContext  _context;
    public LessonController( ApplicationDbContext context)
    {
      _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LessonDto>>>GetAll()
    {
      var lessons = await _context.Lessons.ToListAsync();
      var dtos = lessons.Adapt<List<LessonDto>>();
      return Ok(dtos);
       
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<LessonDto>> GetById(Guid id)
    {
      var lesson = await _context.Lessons.FindAsync(id);
      if (lesson == null) return NotFound();

      var dto = lesson.Adapt<LessonDto>();
      return Ok(dto);
    }
    [HttpPost]
    public async Task<ActionResult<LessonDto>> Create(CreateLessonDTO createDto)
    {
      var lesson = createDto.Adapt<Lesson>();
      lesson.Id = Guid.NewGuid();
      lesson.CreatedDate = DateTime.Now;
      lesson.ModifiedDate = DateTime.Now;

      _context.Lessons.Add(lesson);
      await _context.SaveChangesAsync();

      var resultDto = lesson.Adapt<LessonDto>();
      return CreatedAtAction(nameof(GetById), new { id = lesson.Id }, resultDto);
    }
  }
}
