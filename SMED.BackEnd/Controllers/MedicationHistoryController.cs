using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicationHistoryController : ControllerBase
    {
        private readonly IRepository<MedicationHistoryDTO, int> _medicationHistoryRepository;

        public MedicationHistoryController(IRepository<MedicationHistoryDTO, int> medicationHistoryRepository)
        {
            _medicationHistoryRepository = medicationHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicationHistoryDTO>>> GetAll()
        {
            var medicationHistories = await _medicationHistoryRepository.GetAllAsync();
            return Ok(medicationHistories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicationHistoryDTO>> GetById(int id)
        {
            var medicationHistory = await _medicationHistoryRepository.GetByIdAsync(id);
            if (medicationHistory == null)
                return NotFound();

            return Ok(medicationHistory);
        }

        [HttpPost]
        public async Task<ActionResult<MedicationHistoryDTO>> Add(MedicationHistoryDTO dto)
        {
            try
            {
                var created = await _medicationHistoryRepository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.MedicationHistoryId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MedicationHistoryDTO>> Update(int id, MedicationHistoryDTO dto)
        {
            if (id != dto.MedicationHistoryId)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = await _medicationHistoryRepository.UpdateAsync(dto);
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
            var success = await _medicationHistoryRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("by-clinical-history/{clinicalHistoryId}")]
        public async Task<ActionResult<List<MedicationHistoryDTO>>> GetByClinicalHistoryId(int clinicalHistoryId)
        {
            var allRecords = await _medicationHistoryRepository.GetAllAsync();
            var filtered = allRecords?.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList() ?? new List<MedicationHistoryDTO>();
            return Ok(filtered);
        }
    }
}