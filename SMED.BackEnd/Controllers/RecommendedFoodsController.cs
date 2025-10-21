using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendedFoodsController : ControllerBase
    {
        private readonly RecommendedFoodsRepository _repository;

        public RecommendedFoodsController(RecommendedFoodsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecommendedFoodsDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecommendedFoodsDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<RecommendedFoodsDTO>> Create(RecommendedFoodsDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.RecommendedFoodId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RecommendedFoodsDTO>> Update(int id, RecommendedFoodsDTO dto)
        {
            if (id != dto.RecommendedFoodId) return BadRequest("ID mismatch");
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
        public async Task<ActionResult<List<RecommendedFoodsDTO>>> GetByFoodId(int foodId)
        {
            var result = await _repository.GetByFoodIdAsync(foodId);
            return Ok(result);
        }

        [HttpGet("ByFrequency/{frequency}")]
        public async Task<ActionResult<List<RecommendedFoodsDTO>>> GetByFrequency(string frequency)
        {
            var result = await _repository.GetByFrequencyAsync(frequency);
            return Ok(result);
        }
    }
}