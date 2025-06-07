using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DietaryHabitsHistoryController : ControllerBase
    {
        private readonly IRepository<DietaryHabitsHistoryDTO, int> _repository;

        public DietaryHabitsHistoryController(IRepository<DietaryHabitsHistoryDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DietaryHabitsHistoryDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DietaryHabitsHistoryDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DietaryHabitsHistoryDTO>> Create(DietaryHabitsHistoryDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.DietaryHabitHistoryId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DietaryHabitsHistoryDTO>> Update(int id, DietaryHabitsHistoryDTO dto)
        {
            if (id != dto.DietaryHabitHistoryId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("by-clinical-history/{clinicalHistoryId}")]
        public async Task<ActionResult<List<DietaryHabitsHistoryDTO>>> GetByClinicalHistoryId(int clinicalHistoryId)
        {
            var allRecords = await _repository.GetAllAsync();
            var filtered = allRecords?.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList() ?? new List<DietaryHabitsHistoryDTO>();
            return Ok(filtered);
        }
    }
}
