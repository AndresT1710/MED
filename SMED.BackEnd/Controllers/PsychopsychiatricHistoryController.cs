using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PsychopsychiatricHistoryController : ControllerBase
    {
        private readonly IRepository<PsychopsychiatricHistoryDTO, int> _psychopsychiatricHistoryRepository;

        public PsychopsychiatricHistoryController(IRepository<PsychopsychiatricHistoryDTO, int> psychopsychiatricHistoryRepository)
        {
            _psychopsychiatricHistoryRepository = psychopsychiatricHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PsychopsychiatricHistoryDTO>>> GetAll()
        {
            var psychopsychiatricHistories = await _psychopsychiatricHistoryRepository.GetAllAsync();
            return Ok(psychopsychiatricHistories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PsychopsychiatricHistoryDTO>> GetById(int id)
        {
            var psychopsychiatricHistory = await _psychopsychiatricHistoryRepository.GetByIdAsync(id);
            if (psychopsychiatricHistory == null)
                return NotFound();

            return Ok(psychopsychiatricHistory);
        }

        [HttpPost]
        public async Task<ActionResult<PsychopsychiatricHistoryDTO>> Add(PsychopsychiatricHistoryDTO dto)
        {
            try
            {
                var created = await _psychopsychiatricHistoryRepository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.PsychopsychiatricHistoryId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PsychopsychiatricHistoryDTO>> Update(int id, PsychopsychiatricHistoryDTO dto)
        {
            if (id != dto.PsychopsychiatricHistoryId)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = await _psychopsychiatricHistoryRepository.UpdateAsync(dto);
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
            var success = await _psychopsychiatricHistoryRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("by-clinical-history/{clinicalHistoryId}")]
        public async Task<ActionResult<List<PsychopsychiatricHistoryDTO>>> GetByClinicalHistoryId(int clinicalHistoryId)
        {
            var allRecords = await _psychopsychiatricHistoryRepository.GetAllAsync();
            var filtered = allRecords?.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList() ?? new List<PsychopsychiatricHistoryDTO>();
            return Ok(filtered);
        }
    }
}