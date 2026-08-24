using KPM.Application.DTOs.Department;
using KPM.Application.Features.Department;
using KPM.Domain;
using KPM.Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KPM_Backend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class DepartmentController : ControllerBase
  {
    private readonly IDepartmentService _departmentService;
    public DepartmentController(IDepartmentService departmentService)
    {
      _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<DepartmentDTO>>> GetAll()
    {
      var departments = await _departmentService.GetAllAsync();
      return Ok(departments);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDTO>> GetById(Guid Id)
    {
      var department = await _departmentService.GetByIdAsync(Id);
      if (department == null) return NotFound();
      return Ok(department);

    }

    [HttpPost]
    public async Task<ActionResult<DepartmentDTO>> Create(CreateDepartmentDTO createDto)
    {
      var result = await _departmentService.CreateAsync(createDto);
      return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<DepartmentDTO>> Patch(Guid id, UpdateDepartmentDTO updateDto)
    {
      var result = await _departmentService.PatchAsync(id, updateDto);
      if (result == null) return NotFound();
      return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      var success = await _departmentService.DeleteAsync(id);
      if (!success) return NotFound();
      return NoContent();
    }
  }
}
