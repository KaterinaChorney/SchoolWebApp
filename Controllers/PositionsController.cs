using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolWebApplication.DTOs;
using SchoolWebApplication.Services.Interfaces;

namespace SchoolWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionsController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        // GET: api/positions
        [HttpGet]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<ActionResult<IEnumerable<PositionDto>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? sort,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var positions = await _positionService.GetAllAsync(search, sort, page, pageSize);
            return Ok(positions);
        }

        // GET: api/positions/5
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<ActionResult<PositionDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");

            var position = await _positionService.GetByIdAsync(id);
            if (position == null)
                return NotFound();

            return Ok(position);
        }

        // POST: api/positions
        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create([FromBody] CreatePositionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newId = await _positionService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newId }, dto);
        }

        // PUT: api/positions/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePositionDto dto)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _positionService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _positionService.UpdateAsync(id, dto);
            return Ok($"Посаду з ID = {id} успішно оновлено.");
        }

        // DELETE: api/positions/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");

            var existing = await _positionService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _positionService.DeleteAsync(id);
            return Ok($"Посаду з ID = {id} успішно видалено.");
        }
    }
}