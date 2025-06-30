using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolWebApplication.DTOs;
using SchoolWebApplication.Services.Interfaces;

namespace SchoolWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionsController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<PositionDto>>> GetFiltered(string? keyword, string? sortBy, int page = 1, int pageSize = 10)
        {
            var result = await _positionService.GetFilteredAsync(keyword, sortBy, page, pageSize);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionDto>>> GetAll()
        {
            var result = await _positionService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PositionDto>> GetById(int id)
        {
            var position = await _positionService.GetByIdAsync(id);
            return position == null ? NotFound() : Ok(position);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePositionDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _positionService.CreateAsync(dto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePositionDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _positionService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _positionService.DeleteAsync(id);
            return NoContent();
        }
    }
}
