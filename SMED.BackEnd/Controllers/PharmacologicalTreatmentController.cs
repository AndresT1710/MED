using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.BackEnd.Repositories.Implementations;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PharmacologicalTreatmentController : ControllerBase
    {
        private readonly IRepository<PharmacologicalTreatmentDTO, int> _repository;
        private readonly PharmacologicalTreatmentRepository _pharmacologicalRepository;

        public PharmacologicalTreatmentController(
            IRepository<PharmacologicalTreatmentDTO, int> repository,
            PharmacologicalTreatmentRepository pharmacologicalRepository)
        {
            _repository = repository;
            _pharmacologicalRepository = pharmacologicalRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<PharmacologicalTreatmentDTO>>> GetAll() =>
            Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<PharmacologicalTreatmentDTO>> GetById(int id)
        {
            var dto = await _repository.GetByIdAsync(id);
            return dto != null ? Ok(dto) : NotFound();
        }

        [HttpGet("by-medical-diagnosis/{medicalDiagnosisId}")]
        public async Task<ActionResult<List<PharmacologicalTreatmentDTO>>> GetByMedicalDiagnosisId(int medicalDiagnosisId)
        {
            try
            {
                var treatments = await _pharmacologicalRepository.GetByMedicalDiagnosisIdAsync(medicalDiagnosisId);
                return Ok(treatments);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener tratamientos farmacológicos: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PharmacologicalTreatmentDTO>> Create(PharmacologicalTreatmentDTO dto)
        {
            try
            {
                // ✅ Validación adicional en el controlador
                if (dto.MedicalDiagnosisId <= 0)
                {
                    return BadRequest("El ID del diagnóstico médico es requerido y debe ser mayor a 0.");
                }

                if (dto.MedicineId <= 0)
                {
                    return BadRequest("El ID del medicamento es requerido y debe ser mayor a 0.");
                }

                if (dto.Dose <= 0)
                {
                    return BadRequest("La dosis debe ser mayor a 0.");
                }

                if (string.IsNullOrWhiteSpace(dto.Frequency))
                {
                    return BadRequest("La frecuencia es requerida.");
                }

                if (string.IsNullOrWhiteSpace(dto.Duration))
                {
                    return BadRequest("La duración es requerida.");
                }

                if (string.IsNullOrWhiteSpace(dto.ViaAdmission))
                {
                    return BadRequest("La vía de administración es requerida.");
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
        public async Task<IActionResult> Update(int id, PharmacologicalTreatmentDTO dto)
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
