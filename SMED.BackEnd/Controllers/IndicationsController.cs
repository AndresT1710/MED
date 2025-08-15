using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.BackEnd.Repositories.Implementations;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndicationsController : ControllerBase
    {
        private readonly IRepository<IndicationsDTO, int> _repository;
        private readonly IndicationsRepository _indicationsRepository;

        public IndicationsController(
            IRepository<IndicationsDTO, int> repository,
            IndicationsRepository indicationsRepository)
        {
            _repository = repository;
            _indicationsRepository = indicationsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<IndicationsDTO>>> GetAll() =>
            Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<IndicationsDTO>> GetById(int id)
        {
            var dto = await _repository.GetByIdAsync(id);
            return dto != null ? Ok(dto) : NotFound();
        }

        [HttpGet("by-medical-diagnosis/{medicalDiagnosisId}")]
        public async Task<ActionResult<List<IndicationsDTO>>> GetByMedicalDiagnosisId(int medicalDiagnosisId)
        {
            try
            {
                var indications = await _indicationsRepository.GetByMedicalDiagnosisIdAsync(medicalDiagnosisId);
                return Ok(indications);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener indicaciones: {ex.Message}");
            }
        }

        [HttpGet("by-treatment/{treatmentId}")]
        public async Task<ActionResult<List<IndicationsDTO>>> GetByTreatmentId(int treatmentId)
        {
            try
            {
                var indications = await _indicationsRepository.GetByTreatmentIdAsync(treatmentId);
                return Ok(indications);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener indicaciones por tratamiento: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<IndicationsDTO>> Create(IndicationsDTO dto)
        {
            try
            {
                if (dto.TreatmentId <= 0)
                {
                    return BadRequest("El ID del tratamiento es requerido.");
                }

                var created = await _repository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("for-diagnosis/{medicalDiagnosisId}")]
        public async Task<ActionResult<IndicationsDTO>> CreateForDiagnosis(int medicalDiagnosisId, [FromBody] string description)
        {
            try
            {
                if (medicalDiagnosisId <= 0)
                {
                    return BadRequest("El ID del diagnóstico médico es requerido.");
                }

                if (string.IsNullOrWhiteSpace(description))
                {
                    return BadRequest("La descripción es requerida.");
                }

                var created = await _indicationsRepository.CreateOrUpdateForDiagnosisAsync(medicalDiagnosisId, description);
                return Ok(created);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, IndicationsDTO dto)
        {
            try
            {
                if (id != dto.Id) return BadRequest("El ID no coincide.");

                var updated = await _repository.UpdateAsync(dto);
                return updated != null ? Ok(updated) : NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _repository.DeleteAsync(id);
                return deleted ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("by-medical-diagnosis/{medicalDiagnosisId}")]
        public async Task<IActionResult> DeleteByMedicalDiagnosisId(int medicalDiagnosisId)
        {
            try
            {
                var deleted = await _indicationsRepository.DeleteByMedicalDiagnosisIdAsync(medicalDiagnosisId);
                return deleted ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
