using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolWebApplication.DTOs;
using SchoolWebApplication.Services.Interfaces;

namespace SchoolWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // GET: api/teachers
        [HttpGet]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? sort,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var teachers = await _teacherService.GetAllAsync(search, sort, page, pageSize);
            return Ok(teachers);
        }

        // GET: api/teachers/5
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<ActionResult<TeacherDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");

            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher == null)
                return NotFound();

            return Ok(teacher);
        }

        // POST: api/teachers
        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create([FromBody] CreateTeacherDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newId = await _teacherService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newId }, dto);
        }

        // PUT: api/teachers/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTeacherDto dto)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _teacherService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _teacherService.UpdateAsync(id, dto);
            return Ok($"Викладача з ID = {id} успішно оновлено.");
        }

        // DELETE: api/teachers/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");

            var existing = await _teacherService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _teacherService.DeleteAsync(id);
            return Ok($"Викладача з ID = {id} успішно видалено.");
        }
    }
}