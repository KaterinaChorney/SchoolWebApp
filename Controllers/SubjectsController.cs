using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolWebApplication.DTOs;
using SchoolWebApplication.Services.Interfaces;

namespace SchoolWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        // GET: api/subjects
        [HttpGet]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? sort,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var subjects = await _subjectService.GetAllAsync(search, sort, page, pageSize);
            return Ok(subjects);
        }

        // GET: api/subjects/5
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<ActionResult<SubjectDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");

            var subject = await _subjectService.GetByIdAsync(id);
            if (subject == null)
                return NotFound();

            return Ok(subject);
        }

        // POST: api/subjects
        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create([FromBody] CreateSubjectDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newId = await _subjectService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newId }, dto);
        }

        // PUT: api/subjects/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSubjectDto dto)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _subjectService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _subjectService.UpdateAsync(id, dto);
            return Ok($"Предмет з ID = {id} успішно оновлено.");
        }

        // DELETE: api/subjects/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");

            var existing = await _subjectService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _subjectService.DeleteAsync(id);
            return Ok($"Предмет з ID = {id} успішно видалено.");
        }
    }
}