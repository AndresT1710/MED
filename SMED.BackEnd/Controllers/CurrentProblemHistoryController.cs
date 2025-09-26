using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrentProblemHistoryController : ControllerBase
    {
        private readonly IRepository<CurrentProblemHistoryDTO, int> _currentProblemHistoryRepository;

        public CurrentProblemHistoryController(IRepository<CurrentProblemHistoryDTO, int> currentProblemHistoryRepository)
        {
            _currentProblemHistoryRepository = currentProblemHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrentProblemHistoryDTO>>> GetAll()
        {
            var currentProblemHistories = await _currentProblemHistoryRepository.GetAllAsync();
            return Ok(currentProblemHistories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CurrentProblemHistoryDTO>> GetById(int id)
        {
            var currentProblemHistory = await _currentProblemHistoryRepository.GetByIdAsync(id);
            if (currentProblemHistory == null)
                return NotFound();

            return Ok(currentProblemHistory);
        }

        [HttpPost]
        public async Task<ActionResult<CurrentProblemHistoryDTO>> Add(CurrentProblemHistoryDTO dto)
        {
            try
            {
                var created = await _currentProblemHistoryRepository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.CurrentProblemHistoryId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CurrentProblemHistoryDTO>> Update(int id, CurrentProblemHistoryDTO dto)
        {
            if (id != dto.CurrentProblemHistoryId)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = await _currentProblemHistoryRepository.UpdateAsync(dto);
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
            var success = await _currentProblemHistoryRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("by-clinical-history/{clinicalHistoryId}")]
        public async Task<ActionResult<List<CurrentProblemHistoryDTO>>> GetByClinicalHistoryId(int clinicalHistoryId)
        {
            var allRecords = await _currentProblemHistoryRepository.GetAllAsync();
            var filtered = allRecords?.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList() ?? new List<CurrentProblemHistoryDTO>();
            return Ok(filtered);
        }
    }
}