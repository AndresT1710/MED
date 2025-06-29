using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly PersonRepository _personRepository;
        public PersonController(PersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetAll()
        {
            var person = await _personRepository.GetAllAsync();
            return Ok(person);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDTO>> GetById(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public async Task<ActionResult<PersonDTO>> Create([FromBody] PersonDTO personDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPersona = await _personRepository.AddAsync(personDto);
            return CreatedAtAction(nameof(GetById), new { id = createdPersona.Id }, createdPersona);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PersonDTO>> Update(int id, [FromBody] PersonDTO personDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (personDto.Id == null || personDto.Id != id)
                return BadRequest("El Id en el cuerpo y la URL no coinciden");

            var updatedPerson = await _personRepository.UpdateAsync(personDto);
            if (updatedPerson == null)
                return NotFound();

            return Ok(updatedPerson);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var person = await _personRepository.DeleteAsync(id);
            
            if (!person)
                return NotFound();

            return NoContent();
        }

        [HttpGet("by-document/{documentNumber}")]
        public async Task<ActionResult<PersonDTO>> GetByDocument(string documentNumber)
        {
            if (string.IsNullOrWhiteSpace(documentNumber))
                return BadRequest("El número de documento es requerido");

            var person = await _personRepository.GetByDocumentNumberAsync(documentNumber);

            if (person == null)
                return NotFound($"No se encontró ninguna persona con el documento {documentNumber}");

            return Ok(person);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<PersonDTO>>> SearchPersons([FromQuery] string term)
        {
            if (string.IsNullOrWhiteSpace(term) || term.Length < 3)
                return BadRequest("El término de búsqueda debe tener al menos 3 caracteres");

            var persons = await _personRepository.SearchPersonsAsync(term);
            return Ok(persons);
        }

    }
}
