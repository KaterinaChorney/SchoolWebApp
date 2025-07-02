using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolWebApplication.DTOs;
using SchoolWebApplication.Services.Interfaces;

namespace SchoolWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/students
        [HttpGet]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? sort,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var students = await _studentService.GetAllAsync(search, sort, page, pageSize);
            return Ok(students);
        }

        // GET: api/students/5
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<ActionResult<StudentDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");

            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // POST: api/students
        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newId = await _studentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newId }, dto);
        }

        // PUT: api/students/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentDto dto)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _studentService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _studentService.UpdateAsync(id, dto);
            return Ok($"Студента з ID = {id} успішно оновлено.");
        }

        // DELETE: api/students/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");

            var existing = await _studentService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _studentService.DeleteAsync(id);
            return Ok($"Студента з ID = {id} успішно видалено.");
        }
    }
}