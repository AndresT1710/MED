using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SleepHabitHistoryController : ControllerBase
    {
        private readonly IRepository<SleepHabitHistoryDTO, int> _repository;

        public SleepHabitHistoryController(IRepository<SleepHabitHistoryDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SleepHabitHistoryDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SleepHabitHistoryDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<SleepHabitHistoryDTO>> Create(SleepHabitHistoryDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.HabitSleepHistoryId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SleepHabitHistoryDTO>> Update(int id, SleepHabitHistoryDTO dto)
        {
            if (id != dto.HabitSleepHistoryId)
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
        public async Task<ActionResult<List<SleepHabitHistoryDTO>>> GetByClinicalHistoryId(int clinicalHistoryId)
        {
            var allRecords = await _repository.GetAllAsync();
            var filtered = allRecords?.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList() ?? new List<SleepHabitHistoryDTO>();
            return Ok(filtered);
        }
    }
}
