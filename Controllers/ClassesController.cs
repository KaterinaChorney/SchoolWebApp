using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolWebApplication.DTOs;
using SchoolWebApplication.Services.Interfaces;

namespace SchoolWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        // GET: api/classes
        [HttpGet]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<ActionResult<IEnumerable<ClassDto>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? sort,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var classes = await _classService.GetAllAsync(search, sort, page, pageSize);
            return Ok(classes);
        }

        // GET: api/classes/5
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<ActionResult<ClassDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");

            var classItem = await _classService.GetByIdAsync(id);
            if (classItem == null)
                return NotFound();

            return Ok(classItem);
        }

        // POST: api/classes
        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create([FromBody] CreateClassDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newId = await _classService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newId }, dto);
        }

        // PUT: api/classes/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateClassDto dto)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _classService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _classService.UpdateAsync(id, dto);
            return Ok($"Клас з ID = {id} успішно оновлено.");
        }

        // DELETE: api/classes/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");

            var existing = await _classService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _classService.DeleteAsync(id);
            return Ok($"Клас з ID = {id} успішно видалено.");
        }
    }
}