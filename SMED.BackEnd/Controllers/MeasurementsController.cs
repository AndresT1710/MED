using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeasurementsController : ControllerBase
    {
        private readonly MeasurementsRepository _repository;

        public MeasurementsController(MeasurementsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeasurementsDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MeasurementsDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MeasurementsDTO>> Create(MeasurementsDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.MeasurementsId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MeasurementsDTO>> Update(int id, MeasurementsDTO dto)
        {
            if (id != dto.MeasurementsId) return BadRequest("ID mismatch");
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
        public async Task<ActionResult<MeasurementsDTO>> GetByMedicalCareId(int medicalCareId)
        {
            var result = await _repository.GetByMedicalCareIdAsync(medicalCareId);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}