using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.Mappers;
using UKParliament.CodeTest.Web.ViewModels;
using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _service;
        private readonly PersonManagerContext _context;

        public PersonController(IPersonService service, PersonManagerContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> Get()
        {
            var people = await _service.GetAllAsync();
            return Ok(people.Select(PersonMapper.ToDto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> Get(int id)
        {
            var person = await _service.GetByIdAsync(id);
            if (person == null) return NotFound();
            return Ok(PersonMapper.ToDto(person));
        }

        [HttpPost]
        public async Task<ActionResult<PersonDto>> Post([FromBody] PersonDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var person = PersonMapper.ToEntity(dto);
            // Validate Department exists
            if (!_context.Departments.Any(d => d.Id == person.DepartmentId))
                return BadRequest(new { error = "Invalid department." });

            await _service.CreateAsync(person);
            // Reload with department
            var created = await _service.GetByIdAsync(person.Id);
            return CreatedAtAction(nameof(Get), new { id = person.Id }, PersonMapper.ToDto(created!));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PersonDto>> Put(int id, [FromBody] PersonDto dto)
        {
            if (id != dto.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            // Fetch the tracked entity
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // Update properties
            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.DateOfBirth = dto.DateOfBirth;
            existing.DepartmentId = dto.DepartmentId;
            existing.Email = dto.Email;

            await _service.UpdateAsync(existing);
            var updated = await _service.GetByIdAsync(id);
            return Ok(PersonMapper.ToDto(updated!));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
