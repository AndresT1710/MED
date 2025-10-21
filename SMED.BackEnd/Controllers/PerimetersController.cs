using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerimetersController : ControllerBase
    {
        private readonly PerimetersRepository _repository;

        public PerimetersController(PerimetersRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerimetersDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PerimetersDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PerimetersDTO>> Create(PerimetersDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.PerimetersId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PerimetersDTO>> Update(int id, PerimetersDTO dto)
        {
            if (id != dto.PerimetersId) return BadRequest("ID mismatch");
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

        [HttpGet("ByMeasurements/{measurementsId}")]
        public async Task<ActionResult<PerimetersDTO>> GetByMeasurementsId(int measurementsId)
        {
            var result = await _repository.GetByMeasurementsIdAsync(measurementsId);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}