using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkinFoldsController : ControllerBase
    {
        private readonly SkinFoldsRepository _repository;

        public SkinFoldsController(SkinFoldsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkinFoldsDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkinFoldsDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<SkinFoldsDTO>> Create(SkinFoldsDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.SkinFoldsId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SkinFoldsDTO>> Update(int id, SkinFoldsDTO dto)
        {
            if (id != dto.SkinFoldsId) return BadRequest("ID mismatch");
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
        public async Task<ActionResult<SkinFoldsDTO>> GetByMeasurementsId(int measurementsId)
        {
            var result = await _repository.GetByMeasurementsIdAsync(measurementsId);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}