using Microsoft.AspNetCore.Mvc;
using SchoolWebApplication.DTOs;
using SchoolWebApplication.Services.Interfaces;

namespace SchoolWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // GET: api/teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? sort,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var teachers = await _teacherService.GetAllAsync(search, sort, page, pageSize);
            return Ok(teachers); // 200 OK
        }

        // GET: api/teachers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> GetById(int id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher == null)
                return NotFound(); // 404

            return Ok(teacher); // 200 OK
        }

        // POST: api/teachers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTeacherDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400

            await _teacherService.CreateAsync(dto);
            return StatusCode(201); // 201 Created
        }

        // PUT: api/teachers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTeacherDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400

            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher == null)
                return NotFound(); // 404

            await _teacherService.UpdateAsync(id, dto);
            return NoContent(); // 204
        }

        // DELETE: api/teachers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher == null)
                return NotFound(); // 404

            await _teacherService.DeleteAsync(id);
            return NoContent(); // 204
        }
    }
}