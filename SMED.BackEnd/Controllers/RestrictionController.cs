using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestrictionController : ControllerBase
    {
        private readonly RestrictionRepository _repository;

        public RestrictionController(RestrictionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestrictionDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestrictionDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<RestrictionDTO>> Create(RestrictionDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.RestrictionId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RestrictionDTO>> Update(int id, RestrictionDTO dto)
        {
            if (id != dto.RestrictionId) return BadRequest("ID mismatch");
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

        [HttpGet("ByFood/{foodId}")]
        public async Task<ActionResult<List<RestrictionDTO>>> GetByFoodId(int foodId)
        {
            var result = await _repository.GetByFoodIdAsync(foodId);
            return Ok(result);
        }
    }
}