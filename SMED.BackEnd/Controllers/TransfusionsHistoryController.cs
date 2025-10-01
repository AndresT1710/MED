using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransfusionsHistoryController : ControllerBase
    {
        private readonly IRepository<TransfusionsHistoryDTO, int> _transfusionsHistoryRepository;

        public TransfusionsHistoryController(IRepository<TransfusionsHistoryDTO, int> transfusionsHistoryRepository)
        {
            _transfusionsHistoryRepository = transfusionsHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransfusionsHistoryDTO>>> GetAll()
        {
            var records = await _transfusionsHistoryRepository.GetAllAsync();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransfusionsHistoryDTO>> GetById(int id)
        {
            var record = await _transfusionsHistoryRepository.GetByIdAsync(id);
            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult<TransfusionsHistoryDTO>> Add(TransfusionsHistoryDTO dto)
        {
            try
            {
                var created = await _transfusionsHistoryRepository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.TransfusionsHistoryId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TransfusionsHistoryDTO>> Update(int id, TransfusionsHistoryDTO dto)
        {
            if (id != dto.TransfusionsHistoryId)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = await _transfusionsHistoryRepository.UpdateAsync(dto);
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
            var success = await _transfusionsHistoryRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}