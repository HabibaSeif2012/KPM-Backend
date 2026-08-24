using KPM.Application.DTOs.Function;
using KPM.Application.Features.Function;
using KPM.Domain;
using KPM.Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KPM_Backend.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class FunctionController : ControllerBase
  {
    private readonly IFunctionService _FunctionService;
    public FunctionController(IFunctionService FunctionService)
    {
      _FunctionService = FunctionService;
    }

    [HttpGet]
    public async Task<ActionResult<List<FunctionDTO>>> GetAll()
    {
      var functions = await _FunctionService.GetAllAsync();
      return Ok(functions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FunctionDTO>> GetById(Guid Id)
    {
      var function = await _FunctionService.GetByIdAsync(Id);
      if (function == null) return NotFound();
      return Ok(function);
    }

    [HttpPost]
    public async Task<ActionResult<FunctionDTO>> Create(CreateFunctionDTO createDto)
    {
      var result = await _FunctionService.CreateAsync(createDto);
      return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<FunctionDTO>> Patch(Guid id, UpdateFunctionDTO updateDto)
    {
      var result = await _FunctionService.PatchAsync(id, updateDto);
      if (result == null) return NotFound();
      return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      var success = await _FunctionService.DeleteAsync(id);
      if (!success) return NotFound();
      return NoContent();
    }
  }
}
