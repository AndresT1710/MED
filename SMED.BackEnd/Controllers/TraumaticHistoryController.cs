using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TraumaticHistoryController : ControllerBase
    {
        private readonly IRepository<TraumaticHistoryDTO, int> _traumaticHistoryRepository;

        public TraumaticHistoryController(IRepository<TraumaticHistoryDTO, int> traumaticHistoryRepository)
        {
            _traumaticHistoryRepository = traumaticHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TraumaticHistoryDTO>>> GetAll()
        {
            var records = await _traumaticHistoryRepository.GetAllAsync();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TraumaticHistoryDTO>> GetById(int id)
        {
            var record = await _traumaticHistoryRepository.GetByIdAsync(id);
            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult<TraumaticHistoryDTO>> Add(TraumaticHistoryDTO dto)
        {
            try
            {
                var created = await _traumaticHistoryRepository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.TraumaticHistoryId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TraumaticHistoryDTO>> Update(int id, TraumaticHistoryDTO dto)
        {
            if (id != dto.TraumaticHistoryId)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = await _traumaticHistoryRepository.UpdateAsync(dto);
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
            var success = await _traumaticHistoryRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}