using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mapster;
using KPM.Infrastructure;
using KPM.Domain;
using KPM.Application.DTOs.Department;

namespace KPM_Backend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class DepartmentController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public DepartmentController(ApplicationDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetAll()
    {
      var departments = await _context.Departments.ToListAsync();
      var dtos = departments.Adapt<List<DepartmentDTO>>();
      return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDTO>> GetById(Guid id)
    {
      var department = await _context.Departments.FindAsync(id);
      if (department == null) return NotFound();

      var dto = department.Adapt<DepartmentDTO>();
      return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentDTO>> Create(CreateDepartmentDTO createDto)
    {
      var department = createDto.Adapt<Department>();
      department.Id = Guid.NewGuid();
      department.CreatedDate = DateTime.Now;
      department.ModifiedDate = DateTime.Now;

      _context.Departments.Add(department);
      await _context.SaveChangesAsync();

      var resultDto = department.Adapt<DepartmentDTO>();
      return CreatedAtAction(nameof(GetById), new { id = department.Id }, resultDto);
    }
  }
}
