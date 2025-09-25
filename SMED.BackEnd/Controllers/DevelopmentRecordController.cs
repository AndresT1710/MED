using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevelopmentRecordController : ControllerBase
    {
        private readonly IRepository<DevelopmentRecordDTO, int> _developmentRecordRepository;

        public DevelopmentRecordController(IRepository<DevelopmentRecordDTO, int> developmentRecordRepository)
        {
            _developmentRecordRepository = developmentRecordRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DevelopmentRecordDTO>>> GetAll()
        {
            var records = await _developmentRecordRepository.GetAllAsync();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DevelopmentRecordDTO>> GetById(int id)
        {
            var record = await _developmentRecordRepository.GetByIdAsync(id);
            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult<DevelopmentRecordDTO>> Add(DevelopmentRecordDTO dto)
        {
            try
            {
                var created = await _developmentRecordRepository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.DevelopmentRecordId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DevelopmentRecordDTO>> Update(int id, DevelopmentRecordDTO dto)
        {
            if (id != dto.DevelopmentRecordId)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = await _developmentRecordRepository.UpdateAsync(dto);
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
            var success = await _developmentRecordRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}