using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PsychosexualHistoryController : ControllerBase
    {
        private readonly IRepository<PsychosexualHistoryDTO, int> _psychosexualHistoryRepository;

        public PsychosexualHistoryController(IRepository<PsychosexualHistoryDTO, int> psychosexualHistoryRepository)
        {
            _psychosexualHistoryRepository = psychosexualHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PsychosexualHistoryDTO>>> GetAll()
        {
            var psychosexualHistories = await _psychosexualHistoryRepository.GetAllAsync();
            return Ok(psychosexualHistories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PsychosexualHistoryDTO>> GetById(int id)
        {
            var psychosexualHistory = await _psychosexualHistoryRepository.GetByIdAsync(id);
            if (psychosexualHistory == null)
                return NotFound();

            return Ok(psychosexualHistory);
        }

        [HttpPost]
        public async Task<ActionResult<PsychosexualHistoryDTO>> Add(PsychosexualHistoryDTO dto)
        {
            try
            {
                var created = await _psychosexualHistoryRepository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.PsychosexualHistoryId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PsychosexualHistoryDTO>> Update(int id, PsychosexualHistoryDTO dto)
        {
            if (id != dto.PsychosexualHistoryId)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = await _psychosexualHistoryRepository.UpdateAsync(dto);
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
            var success = await _psychosexualHistoryRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("by-clinical-history/{clinicalHistoryId}")]
        public async Task<ActionResult<List<PsychosexualHistoryDTO>>> GetByClinicalHistoryId(int clinicalHistoryId)
        {
            var allRecords = await _psychosexualHistoryRepository.GetAllAsync();
            var filtered = allRecords?.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList() ?? new List<PsychosexualHistoryDTO>();
            return Ok(filtered);
        }
    }
}