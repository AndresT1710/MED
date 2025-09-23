using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkHistoryController : ControllerBase
    {
        private readonly IRepository<WorkHistoryDTO, int> _workHistoryRepository;

        public WorkHistoryController(IRepository<WorkHistoryDTO, int> workHistoryRepository)
        {
            _workHistoryRepository = workHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkHistoryDTO>>> GetAll()
        {
            var workHistories = await _workHistoryRepository.GetAllAsync();
            return Ok(workHistories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkHistoryDTO>> GetById(int id)
        {
            var workHistory = await _workHistoryRepository.GetByIdAsync(id);
            if (workHistory == null)
                return NotFound();

            return Ok(workHistory);
        }

        [HttpPost]
        public async Task<ActionResult<WorkHistoryDTO>> Add(WorkHistoryDTO dto)
        {
            try
            {
                var created = await _workHistoryRepository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.WorkHistoryId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WorkHistoryDTO>> Update(int id, WorkHistoryDTO dto)
        {
            if (id != dto.WorkHistoryId)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = await _workHistoryRepository.UpdateAsync(dto);
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
            var success = await _workHistoryRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("by-clinical-history/{clinicalHistoryId}")]
        public async Task<ActionResult<List<WorkHistoryDTO>>> GetByClinicalHistoryId(int clinicalHistoryId)
        {
            var allRecords = await _workHistoryRepository.GetAllAsync();
            var filtered = allRecords?.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList() ?? new List<WorkHistoryDTO>();
            return Ok(filtered);
        }
    }
}