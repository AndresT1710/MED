using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrenatalHistoryController : ControllerBase
    {
        private readonly IRepository<PrenatalHistoryDTO, int> _prenatalHistoryRepository;

        public PrenatalHistoryController(IRepository<PrenatalHistoryDTO, int> prenatalHistoryRepository)
        {
            _prenatalHistoryRepository = prenatalHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrenatalHistoryDTO>>> GetAll()
        {
            var records = await _prenatalHistoryRepository.GetAllAsync();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrenatalHistoryDTO>> GetById(int id)
        {
            var record = await _prenatalHistoryRepository.GetByIdAsync(id);
            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult<PrenatalHistoryDTO>> Add(PrenatalHistoryDTO dto)
        {
            try
            {
                var created = await _prenatalHistoryRepository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.PrenatalHistoryId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PrenatalHistoryDTO>> Update(int id, PrenatalHistoryDTO dto)
        {
            if (id != dto.PrenatalHistoryId)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = await _prenatalHistoryRepository.UpdateAsync(dto);
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
            var success = await _prenatalHistoryRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}