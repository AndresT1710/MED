using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.BackEnd.Repositories.Implementations;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalDiagnosisController : ControllerBase
    {
        private readonly IRepository<MedicalDiagnosisDTO, int> _repository;
        private readonly MedicalDiagnosisRepository _diagnosisRepository;

        public MedicalDiagnosisController(IRepository<MedicalDiagnosisDTO, int> repository, MedicalDiagnosisRepository diagnosisRepository)
        {
            _repository = repository;
            _diagnosisRepository = diagnosisRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<MedicalDiagnosisDTO>>> GetAll() =>
            Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalDiagnosisDTO>> GetById(int id)
        {
            var dto = await _repository.GetByIdAsync(id);
            return dto != null ? Ok(dto) : NotFound();
        }

        [HttpGet("by-medical-care/{medicalCareId}")]
        public async Task<ActionResult<List<MedicalDiagnosisDTO>>> GetByMedicalCareId(int medicalCareId)
        {
            var diagnoses = await _diagnosisRepository.GetByMedicalCareIdAsync(medicalCareId);
            return Ok(diagnoses);
        }

        [HttpPost]
        public async Task<ActionResult<MedicalDiagnosisDTO>> Create(MedicalDiagnosisDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MedicalDiagnosisDTO dto)
        {
            if (id != dto.Id) return BadRequest();
            var updated = await _repository.UpdateAsync(dto);
            return updated != null ? Ok(updated) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        // ✅ Endpoint adicional para asignar tratamientos
        [HttpPost("{diagnosisId}/assign-treatments")]
        public async Task<IActionResult> AssignTreatments(int diagnosisId, [FromBody] List<int> treatmentIds)
        {
            var result = await _diagnosisRepository.AssignTreatmentsAsync(diagnosisId, treatmentIds);
            return result ? Ok(new { Message = "Tratamientos asignados exitosamente" }) : NotFound();
        }
    }
}
