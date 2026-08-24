using Microsoft.AspNetCore.Mvc;
using KPM.Application.Features.Industry;
using KPM.Application.DTOs.Industry;

namespace KPM_Backend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class IndustryController : ControllerBase
  {
    private readonly IIndustryService _industryService;

    public IndustryController(IIndustryService industryService)
    {
      _industryService = industryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<IndustryDTO>>> GetAll()
    {
      var industries = await _industryService.GetAllAsync();
      return Ok(industries);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IndustryDTO>> GetById(Guid id)
    {
      var industry = await _industryService.GetByIdAsync(id);
      if (industry == null) return NotFound();
      return Ok(industry);
    }

    [HttpPost]
    public async Task<ActionResult<IndustryDTO>> Create(CreateIndustryDTO createDto)
    {
      var result = await _industryService.CreateAsync(createDto);
      return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<IndustryDTO>> Patch(Guid id, UpdateIndustryDTO updateDto)
    {
      var result = await _industryService.PatchAsync(id, updateDto);
      if (result == null) return NotFound();
      return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      var success = await _industryService.DeleteAsync(id);
      if (!success) return NotFound();
      return NoContent();
    }
  }
}
