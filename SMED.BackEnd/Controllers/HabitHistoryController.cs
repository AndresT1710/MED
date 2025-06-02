using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HabitHistoryController : ControllerBase
    {
        private readonly IRepository<HabitHistoryDTO, int> _repository;

        public HabitHistoryController(IRepository<HabitHistoryDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<HabitHistoryDTO>>> GetAll()
        {
            var histories = await _repository.GetAllAsync();
            return Ok(histories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HabitHistoryDTO>> GetById(int id)
        {
            var history = await _repository.GetByIdAsync(id);
            return history != null ? Ok(history) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<HabitHistoryDTO>> Create(HabitHistoryDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.HabitHistoryId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HabitHistoryDTO dto)
        {
            if (id != dto.HabitHistoryId)
                return BadRequest("ID en la URL no coincide con el ID del historial");

            var updated = await _repository.UpdateAsync(dto);
            return updated != null ? Ok(updated) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        // Endpoint adicional para obtener historiales por ClinicalHistoryId
        [HttpGet("byClinicalHistory/{clinicalHistoryId}")]
        public async Task<ActionResult<List<HabitHistoryDTO>>> GetByClinicalHistoryId(int clinicalHistoryId)
        {
            var histories = await _repository.GetAllAsync();
            var filteredHistories = histories
                .Where(h => h.ClinicalHistoryId == clinicalHistoryId)
                .ToList();

            return Ok(filteredHistories);
        }
    }
}