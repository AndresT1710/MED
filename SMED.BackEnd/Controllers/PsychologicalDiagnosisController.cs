using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PsychologicalDiagnosisController : ControllerBase
    {
        private readonly IRepository<PsychologicalDiagnosisDTO, int> _repository;
        private readonly SGISContext _context; // ✅ Tu DbContext

        public PsychologicalDiagnosisController(
            IRepository<PsychologicalDiagnosisDTO, int> repository,
            SGISContext context)
        {
            _repository = repository;
            _context = context; // ✅ Inyección del contexto
        }

        [HttpGet]
        public async Task<ActionResult<List<PsychologicalDiagnosisDTO>>> GetAll()
        {
            var psychologicalDiagnoses = await _repository.GetAllAsync();
            return Ok(psychologicalDiagnoses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PsychologicalDiagnosisDTO>> GetById(int id)
        {
            var psychologicalDiagnosis = await _repository.GetByIdAsync(id);
            if (psychologicalDiagnosis == null)
                return NotFound();

            return Ok(psychologicalDiagnosis);
        }

        [HttpPost]
        public async Task<ActionResult<PsychologicalDiagnosisDTO>> Create(PsychologicalDiagnosisDTO dto)
        {
            // ✅ Validar que el tipo de diagnóstico exista
            var diagnosticTypeExists = await _context.DiagnosticTypes
                .AnyAsync(dt => dt.Id == dto.DiagnosticTypeId);

            if (!diagnosticTypeExists)
                return BadRequest("El tipo de diagnóstico especificado no existe.");

            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.PsychologicalDiagnosisId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PsychologicalDiagnosisDTO>> Update(int id, PsychologicalDiagnosisDTO dto)
        {
            if (id != dto.PsychologicalDiagnosisId)
                return BadRequest("El ID no coincide con el diagnóstico enviado.");

            // ✅ Validar que el tipo de diagnóstico exista
            var diagnosticTypeExists = await _context.DiagnosticTypes
                .AnyAsync(dt => dt.Id == dto.DiagnosticTypeId);

            if (!diagnosticTypeExists)
                return BadRequest("El tipo de diagnóstico especificado no existe.");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("ByMedicalCare/{medicalCareId}")]
        public async Task<ActionResult<PsychologicalDiagnosisDTO>> GetByMedicalCareId(int medicalCareId)
        {
            if (_repository is not PsychologicalDiagnosisRepository repository)
                return BadRequest("Repository type not supported.");

            var psychologicalDiagnosis = await repository.GetByMedicalCareIdAsync(medicalCareId);
            if (psychologicalDiagnosis == null)
                return NotFound();

            return Ok(psychologicalDiagnosis);
        }

        [HttpGet("ByCIE10/{cie10}")]
        public async Task<ActionResult<List<PsychologicalDiagnosisDTO>>> GetByCIE10(string cie10)
        {
            if (_repository is not PsychologicalDiagnosisRepository repository)
                return BadRequest("Repository type not supported.");

            var psychologicalDiagnoses = await repository.GetByCIE10Async(cie10);
            return Ok(psychologicalDiagnoses);
        }

        [HttpGet("ByDiagnosticType/{diagnosticTypeId}")]
        public async Task<ActionResult<List<PsychologicalDiagnosisDTO>>> GetByDiagnosticTypeId(int diagnosticTypeId)
        {
            if (_repository is not PsychologicalDiagnosisRepository repository)
                return BadRequest("Repository type not supported.");

            var psychologicalDiagnoses = await repository.GetByDiagnosticTypeIdAsync(diagnosticTypeId);
            return Ok(psychologicalDiagnoses);
        }
    }
}
