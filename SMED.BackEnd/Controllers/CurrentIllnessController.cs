using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrentIllnessController : ControllerBase
    {
        private readonly CurrentIllnessRepository _repository;

        public CurrentIllnessController(CurrentIllnessRepository repository) 
        {
            _repository = repository;
        }

        // GET: api/CurrentIllness
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrentIllnessDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/CurrentIllness/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CurrentIllnessDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/CurrentIllness
        [HttpPost]
        public async Task<ActionResult<CurrentIllnessDTO>> Create(CurrentIllnessDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.CurrentIllnessId }, created);
        }

        // PUT: api/CurrentIllness/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CurrentIllnessDTO>> Update(int id, CurrentIllnessDTO dto)
        {
            if (id != dto.CurrentIllnessId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/CurrentIllness/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        // GET: api/CurrentIllness/ByMedicalCare/5
        [HttpGet("ByMedicalCare/{medicalCareId}")]
        public async Task<ActionResult<CurrentIllnessDTO>> GetByMedicalCareId(int medicalCareId)
        {
            var result = await _repository.GetByMedicalCareIdAsync(medicalCareId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("ByCare/{medicalCareId}")]
        public async Task<ActionResult<List<CurrentIllnessDTO>>> GetByCareId(int medicalCareId)
        {
            var result = await _repository.GetByCareIdAsync(medicalCareId);
            return Ok(result);
        }
    }
}