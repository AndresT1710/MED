using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.BackEnd.Repositories.Implementations;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NonPharmacologicalTreatmentController : ControllerBase
    {
        private readonly IRepository<NonPharmacologicalTreatmentDTO, int> _repository;
        private readonly NonPharmacologicalTreatmentRepository _nonPharmacologicalRepository;

        public NonPharmacologicalTreatmentController(
            IRepository<NonPharmacologicalTreatmentDTO, int> repository,
            NonPharmacologicalTreatmentRepository nonPharmacologicalRepository)
        {
            _repository = repository;
            _nonPharmacologicalRepository = nonPharmacologicalRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<NonPharmacologicalTreatmentDTO>>> GetAll() =>
            Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<NonPharmacologicalTreatmentDTO>> GetById(int id)
        {
            var dto = await _repository.GetByIdAsync(id);
            return dto != null ? Ok(dto) : NotFound();
        }

        [HttpGet("by-medical-diagnosis/{medicalDiagnosisId}")]
        public async Task<ActionResult<List<NonPharmacologicalTreatmentDTO>>> GetByMedicalDiagnosisId(int medicalDiagnosisId)
        {
            try
            {
                var treatments = await _nonPharmacologicalRepository.GetByMedicalDiagnosisIdAsync(medicalDiagnosisId);
                return Ok(treatments);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener tratamientos no farmacológicos: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<NonPharmacologicalTreatmentDTO>> Create(NonPharmacologicalTreatmentDTO dto)
        {
            try
            {
                if (dto.MedicalDiagnosisId <= 0)
                {
                    return BadRequest("El ID del diagnóstico médico es requerido y debe ser mayor a 0.");
                }

                if (string.IsNullOrWhiteSpace(dto.Description))
                {
                    return BadRequest("La descripción del tratamiento es requerida.");
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NonPharmacologicalTreatmentDTO dto)
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
    }
}
