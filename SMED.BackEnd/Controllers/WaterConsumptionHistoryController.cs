using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WaterConsumptionHistoryController : ControllerBase
    {
        private readonly IRepository<WaterConsumptionHistoryDTO, int> _repository;

        public WaterConsumptionHistoryController(IRepository<WaterConsumptionHistoryDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WaterConsumptionHistoryDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WaterConsumptionHistoryDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<WaterConsumptionHistoryDTO>> Create(WaterConsumptionHistoryDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.WaterConsumptionHistoryId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WaterConsumptionHistoryDTO>> Update(int id, WaterConsumptionHistoryDTO dto)
        {
            if (id != dto.WaterConsumptionHistoryId)
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
    }
}
