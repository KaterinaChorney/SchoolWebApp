using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolWebApplication.DTOs;
using SchoolWebApplication.Services.Interfaces;

namespace SchoolWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class JournalsController : ControllerBase
    {
        private readonly IJournalService _journalService;

        public JournalsController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        // GET: api/journals
        [HttpGet]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<ActionResult<IEnumerable<JournalDto>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? sort,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var journals = await _journalService.GetAllAsync(search, sort, page, pageSize);
            return Ok(journals);
        }

        // GET: api/journals/5
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<ActionResult<JournalDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");

            var journal = await _journalService.GetByIdAsync(id);
            if (journal == null)
                return NotFound();

            return Ok(journal);
        }

        // POST: api/journals
        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create([FromBody] CreateJournalDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newId = await _journalService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newId }, dto);
        }

        // PUT: api/journals/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateJournalDto dto)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _journalService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _journalService.UpdateAsync(id, dto);
            return Ok($"Журнал з ID = {id} успішно оновлено.");
        }

        // DELETE: api/journals/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id має бути більшим за 0.");

            var existing = await _journalService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _journalService.DeleteAsync(id);
            return Ok($"Журнал з ID = {id} успішно видалено.");
        }
    }
}