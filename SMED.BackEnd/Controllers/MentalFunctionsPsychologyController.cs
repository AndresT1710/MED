using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MentalFunctionsPsychologyController : ControllerBase
    {
        private readonly MentalFunctionsPsychologyRepository _repository;

        public MentalFunctionsPsychologyController(MentalFunctionsPsychologyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MentalFunctionsPsychologyDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MentalFunctionsPsychologyDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MentalFunctionsPsychologyDTO>> Create(MentalFunctionsPsychologyDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.MentalFunctionsPsychologyId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MentalFunctionsPsychologyDTO>> Update(int id, MentalFunctionsPsychologyDTO dto)
        {
            if (id != dto.MentalFunctionsPsychologyId) return BadRequest("ID mismatch");

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

        [HttpGet("ByMedicalCare/{medicalCareId}")]
        public async Task<ActionResult<List<MentalFunctionsPsychologyDTO>>> GetByMedicalCareId(int medicalCareId)
        {
            var result = await _repository.GetByMedicalCareIdAsync(medicalCareId);
            return Ok(result);
        }
    }
}
