using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiametersController : ControllerBase
    {
        private readonly DiametersRepository _repository;

        public DiametersController(DiametersRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiametersDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiametersDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DiametersDTO>> Create(DiametersDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.DiametersId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DiametersDTO>> Update(int id, DiametersDTO dto)
        {
            if (id != dto.DiametersId) return BadRequest("ID mismatch");
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
        public async Task<ActionResult<DiametersDTO>> GetByMeasurementsId(int measurementsId)
        {
            var result = await _repository.GetByMeasurementsIdAsync(measurementsId);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}