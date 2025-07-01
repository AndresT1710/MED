using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClinicalHistoryController : ControllerBase
    {
        private readonly IClinicalHistoryRepository _clinicalHistoryRepository;

        public ClinicalHistoryController(IClinicalHistoryRepository clinicalHistoryRepository)
        {
            _clinicalHistoryRepository = clinicalHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClinicalHistoryDTO>>> GetAll()
        {
            var histories = await _clinicalHistoryRepository.GetAllAsync();
            return Ok(histories);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ClinicalHistoryDTO>>> Search(
         [FromQuery] string term,
         [FromQuery] bool byIdNumber = false)
        {
            if (string.IsNullOrWhiteSpace(term))
                return BadRequest("Search term is required");

            var results = await _clinicalHistoryRepository.SearchAsync(term, byIdNumber);

            if (!results.Any())
                return NotFound("No se encontraron registros");

            return Ok(results);
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
        public async Task<ActionResult<ClinicalHistoryDTO>> Add(ClinicalHistoryCreateDTO createDto)
        {
            try
            {
                var clinicalHistoryDto = new ClinicalHistoryDTO
                {
                    HistoryNumber = createDto.HistoryNumber,
                    CreationDate = createDto.CreationDate ?? DateTime.Now,
                    IsActive = createDto.IsActive ?? true,
                    GeneralObservations = createDto.GeneralObservations,
                    Patient = new PatientDTO { PersonId = createDto.Patient.PersonId }
                };

                var created = await _clinicalHistoryRepository.AddAsync(clinicalHistoryDto);
                return CreatedAtAction(nameof(GetById), new { id = created.ClinicalHistoryId }, created);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("ya tiene una historia clínica"))
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClinicalHistoryDTO>> Update(int id, BasicClinicalHistroyDTO dto)
        {
            if (id != dto.ClinicalHistoryId)
                return BadRequest("ID mismatch.");

            try
            {
                var clinicalDto = new ClinicalHistoryDTO
                {
                    ClinicalHistoryId = dto.ClinicalHistoryId,
                    HistoryNumber = dto.HistoryNumber,
                    CreationDate = dto.CreationDate,
                    IsActive = dto.IsActive,
                    GeneralObservations = dto.GeneralObservations,
                    Patient = new PatientDTO { PersonId = dto.Patient.PersonId }
                };

                var updated = await _clinicalHistoryRepository.UpdateAsync(clinicalDto);
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


        [HttpGet("check-patient/{personId}")]
        public async Task<ActionResult<object>> CheckPatientHistory(int personId)
        {
            try
            {
                var hasHistory = await _clinicalHistoryRepository.PatientHasClinicalHistoryAsync(personId);

                if (hasHistory)
                {
                    var existingHistory = await _clinicalHistoryRepository.GetByPatientIdAsync(personId);
                    return Ok(new
                    {
                        hasHistory = true,
                        message = "Este paciente ya tiene una historia clínica activa.",
                        existingHistory = existingHistory
                    });
                }

                return Ok(new { hasHistory = false });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al verificar historia clínica: {ex.Message}");
            }
        }


    }
}
