using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalCareController : ControllerBase
    {
        private readonly IRepository<MedicalCareDTO, int> _repository;
        private readonly MedicalCareRepository _medicalCareRepository;
        private readonly ILogger<MedicalCareController> _logger;

        public MedicalCareController(
            IRepository<MedicalCareDTO, int> repository,
            MedicalCareRepository medicalCareRepository,
            ILogger<MedicalCareController> logger)
        {
            _repository = repository;
            _medicalCareRepository = medicalCareRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<MedicalCareDTO>>> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalCareDTO>> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Buscando atención médica con ID: {Id}", id);
                var dto = await _repository.GetByIdAsync(id);

                if (dto == null)
                {
                    _logger.LogWarning("No se encontró atención médica con ID: {Id}", id);
                    return NotFound(new { message = $"No se encontró la atención médica con ID {id}" });
                }

                _logger.LogInformation("Atención médica encontrada: {Id}", id);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener atención médica con ID: {Id}", id);
                return StatusCode(500, new { message = "Error interno al obtener la atención médica", error = ex.Message });
            }
        }

        [HttpGet("nursing")]
        public async Task<ActionResult<List<MedicalCareDTO>>> GetNursingCare()
        {
            try
            {
                var result = await _medicalCareRepository.GetNursingCareAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener atenciones de enfermería");
                return BadRequest($"Error al obtener atenciones de enfermería: {ex.Message}");
            }
        }

        [HttpGet("psychology")]
        public async Task<ActionResult<List<MedicalCareDTO>>> GetPsychologyCare()
        {
            try
            {
                var result = await _medicalCareRepository.GetPsychologyCareAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener atenciones de psicología");
                return BadRequest($"Error al obtener atenciones de psicología: {ex.Message}");
            }
        }

        [HttpGet("physiotherapy")]
        public async Task<ActionResult<List<MedicalCareDTO>>> GetPhysiotherapyCare()
        {
            try
            {
                var result = await _medicalCareRepository.GetPhysiotherapyCareAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener atenciones de fisioterapia");
                return BadRequest($"Error al obtener atenciones de fisioterapia: {ex.Message}");
            }
        }

        [HttpGet("early-stimulation")]
        public async Task<ActionResult<List<MedicalCareDTO>>> GetEarlyStimulationCare()
        {
            try
            {
                var result = await _medicalCareRepository.GetEarlyStimulationCareAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener atenciones de estimulación temprana");
                return BadRequest($"Error al obtener atenciones de estimulación temprana: {ex.Message}");
            }
        }

        [HttpGet("general-medicine")]
        public async Task<ActionResult<List<MedicalCareDTO>>> GetGeneralMedicineCare()
        {
            try
            {
                var result = await _medicalCareRepository.GetGeneralMedicineCareAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener atenciones de medicina general");
                return BadRequest($"Error al obtener atenciones de medicina general: {ex.Message}");
            }
        }

        [HttpGet("nutrition")]
        public async Task<ActionResult<List<MedicalCareDTO>>> GetNutritionCare()
        {
            try
            {
                var result = await _medicalCareRepository.GetNutritionCareAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener atenciones de nutrición");
                return BadRequest($"Error al obtener atenciones de nutrición: {ex.Message}");
            }
        }

        [HttpGet("by-area-and-date")]
        public async Task<ActionResult<List<MedicalCareDTO>>> GetByAreaAndDate([FromQuery] string area, [FromQuery] DateTime? date = null)
        {
            try
            {
                var result = await _medicalCareRepository.GetByAreaAndDateAsync(area, date);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener atenciones por área y fecha");
                return BadRequest($"Error al obtener atenciones por área y fecha: {ex.Message}");
            }
        }

        [HttpGet("by-place-and-date")]
        public async Task<ActionResult<List<MedicalCareDTO>>> GetByPlaceOfAttentionAndDate([FromQuery] int? placeOfAttentionId = null, [FromQuery] DateTime? date = null)
        {
            try
            {
                var result = await _medicalCareRepository.GetByPlaceOfAttentionAndDateAsync(placeOfAttentionId, date);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener atenciones por lugar de atención y fecha");
                return BadRequest($"Error al obtener atenciones por lugar de atención y fecha: {ex.Message}");
            }
        }

        [HttpGet("by-document/{documentNumber}")]
        public async Task<ActionResult<List<MedicalCareDTO>>> GetByPatientDocument(string documentNumber)
        {
            try
            {
                var result = await _medicalCareRepository.GetByPatientDocumentAsync(documentNumber);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar atenciones por cédula");
                return BadRequest($"Error al buscar atenciones por cédula: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<MedicalCareDTO>> Create(MedicalCareDTO dto)
        {
            try
            {
                var created = await _repository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.CareId }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear atención médica");
                return BadRequest($"Error al crear atención médica: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MedicalCareDTO dto)
        {
            try
            {
                if (id != dto.CareId)
                    return BadRequest(new { message = "El ID no coincide con el DTO" });

                var updated = await _repository.UpdateAsync(dto);
                return updated != null ? Ok(updated) : NotFound(new { message = $"No se encontró la atención médica con ID {id}" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar atención médica con ID: {Id}", id);
                return BadRequest($"Error al actualizar atención médica: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _repository.DeleteAsync(id);
                return deleted ? NoContent() : NotFound(new { message = $"No se encontró la atención médica con ID {id}" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar atención médica con ID: {Id}", id);
                return BadRequest($"Error al eliminar atención médica: {ex.Message}");
            }
        }
    }
}
