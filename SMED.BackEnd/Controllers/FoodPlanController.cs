using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodPlanController : ControllerBase
    {
        private readonly FoodPlanRepository _repository;

        public FoodPlanController(FoodPlanRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodPlanDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FoodPlanDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<FoodPlanDTO>> Create(FoodPlanDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.FoodPlanId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FoodPlanDTO>> Update(int id, FoodPlanDTO dto)
        {
            if (id != dto.FoodPlanId) return BadRequest("ID mismatch");
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

        [HttpGet("ByCare/{careId}")]
        public async Task<ActionResult<List<FoodPlanDTO>>> GetByCareId(int careId)
        {
            var result = await _repository.GetByCareIdAsync(careId);
            return Ok(result);
        }

        [HttpGet("ByRestriction/{restrictionId}")]
        public async Task<ActionResult<FoodPlanDTO>> GetByRestrictionId(int restrictionId)
        {
            var result = await _repository.GetByRestrictionIdAsync(restrictionId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("ByRecommendedFood/{recommendedFoodId}")]
        public async Task<ActionResult<List<FoodPlanDTO>>> GetByRecommendedFoodId(int recommendedFoodId)
        {
            var result = await _repository.GetByRecommendedFoodIdAsync(recommendedFoodId);
            return Ok(result);
        }
    }
}