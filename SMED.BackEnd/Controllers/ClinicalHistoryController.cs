using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClinicalHistoryController : ControllerBase
    {
        private readonly ClinicalHistoryRepository _clinicalHistoryRepository;

        public ClinicalHistoryController(ClinicalHistoryRepository clinicalHistoryRepository)
        {
            _clinicalHistoryRepository = clinicalHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClinicalHistoryDTO>>> GetAll()
        {
            var histories = await _clinicalHistoryRepository.GetAllAsync();
            return Ok(histories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicalHistoryDTO>> GetById(int id)
        {
            var history = await _clinicalHistoryRepository.GetByIdAsync(id);
            if (history == null)
                return NotFound();

            return Ok(history);
        }

        [HttpPost]
        public async Task<ActionResult<ClinicalHistoryDTO>> Add(ClinicalHistoryDTO dto)
        {
            var created = await _clinicalHistoryRepository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ClinicalHistoryId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClinicalHistoryDTO>> Update(int id, ClinicalHistoryDTO dto)
        {
            if (id != dto.ClinicalHistoryId)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = await _clinicalHistoryRepository.UpdateAsync(dto);
                if (updated == null)
                    return NotFound();

                return Ok(updated);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("HistoryNumber already exists"))
            {
                return BadRequest("The HistoryNumber already exists for another record.");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _clinicalHistoryRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
