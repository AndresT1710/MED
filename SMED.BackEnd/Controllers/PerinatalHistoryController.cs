using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerinatalHistoryController : ControllerBase
    {
        private readonly IRepository<PerinatalHistoryDTO, int> _perinatalHistoryRepository;

        public PerinatalHistoryController(IRepository<PerinatalHistoryDTO, int> perinatalHistoryRepository)
        {
            _perinatalHistoryRepository = perinatalHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerinatalHistoryDTO>>> GetAll()
        {
            var histories = await _perinatalHistoryRepository.GetAllAsync();
            return Ok(histories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PerinatalHistoryDTO>> GetById(int id)
        {
            var history = await _perinatalHistoryRepository.GetByIdAsync(id);
            if (history == null)
                return NotFound();

            return Ok(history);
        }

        [HttpPost]
        public async Task<ActionResult<PerinatalHistoryDTO>> Add(PerinatalHistoryDTO dto)
        {
            try
            {
                var created = await _perinatalHistoryRepository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.PerinatalHistoryId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PerinatalHistoryDTO>> Update(int id, PerinatalHistoryDTO dto)
        {
            if (id != dto.PerinatalHistoryId)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = await _perinatalHistoryRepository.UpdateAsync(dto);
                if (updated == null)
                    return NotFound();

                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _perinatalHistoryRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}