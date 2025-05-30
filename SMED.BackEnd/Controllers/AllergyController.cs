using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergyController : ControllerBase
    {
        private readonly IRepository<AllergyDTO, int> _repository;

        public AllergyController(IRepository<AllergyDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<AllergyDTO>>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AllergyDTO>> GetById(int id)
        {
            var allergy = await _repository.GetByIdAsync(id);
            if (allergy == null) return NotFound();
            return Ok(allergy);
        }

        [HttpPost]
        public async Task<ActionResult<AllergyDTO>> Add(AllergyDTO dto)
        {
            var added = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = added.AllergyId }, added);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AllergyDTO>> Update(int id, AllergyDTO dto)
        {
            if (id != dto.AllergyId) return BadRequest("ID mismatch.");
            var updated = await _repository.UpdateAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
