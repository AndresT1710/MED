using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HospitalizationsHistoryController : ControllerBase
    {
        private readonly IRepository<HospitalizationsHistoryDTO, int> _hospitalizationsHistoryRepository;

        public HospitalizationsHistoryController(IRepository<HospitalizationsHistoryDTO, int> hospitalizationsHistoryRepository)
        {
            _hospitalizationsHistoryRepository = hospitalizationsHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalizationsHistoryDTO>>> GetAll()
        {
            var records = await _hospitalizationsHistoryRepository.GetAllAsync();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalizationsHistoryDTO>> GetById(int id)
        {
            var record = await _hospitalizationsHistoryRepository.GetByIdAsync(id);
            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult<HospitalizationsHistoryDTO>> Add(HospitalizationsHistoryDTO dto)
        {
            try
            {
                var created = await _hospitalizationsHistoryRepository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.HospitalizationsHistoryId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HospitalizationsHistoryDTO>> Update(int id, HospitalizationsHistoryDTO dto)
        {
            if (id != dto.HospitalizationsHistoryId)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = await _hospitalizationsHistoryRepository.UpdateAsync(dto);
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
            var success = await _hospitalizationsHistoryRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}