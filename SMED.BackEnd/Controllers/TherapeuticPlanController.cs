using Microsoft.AspNetCore.Mvc;
using SGIS.Models;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TherapeuticPlanController : ControllerBase
    {
        private readonly IRepository<TherapeuticPlanDTO, int> _repository;
        private readonly SGISContext _context;

        public TherapeuticPlanController(IRepository<TherapeuticPlanDTO, int> repository, SGISContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TherapeuticPlanDTO>>> GetAll()
        {
            var therapeuticPlans = await _repository.GetAllAsync();
            return Ok(therapeuticPlans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TherapeuticPlanDTO>> GetById(int id)
        {
            var therapeuticPlan = await _repository.GetByIdAsync(id);
            if (therapeuticPlan == null)
                return NotFound();

            return Ok(therapeuticPlan);
        }

        [HttpPost]
        public async Task<ActionResult<TherapeuticPlanDTO>> Create(TherapeuticPlanDTO dto)
        {
            // Validar que el PsychologicalDiagnosisId exista
            var psychologicalDiagnosisExists = await _context.PsychologicalDiagnoses
                .AnyAsync(pd => pd.PsychologicalDiagnosisId == dto.PsychologicalDiagnosisId);
            if (!psychologicalDiagnosisExists)
                return BadRequest("El diagnóstico psicológico especificado no existe");

            var createdTherapeuticPlan = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdTherapeuticPlan.TherapeuticPlanId }, createdTherapeuticPlan);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TherapeuticPlanDTO>> Update(int id, TherapeuticPlanDTO dto)
        {
            if (id != dto.TherapeuticPlanId)
                return BadRequest();

            // Validar que el PsychologicalDiagnosisId exista
            var psychologicalDiagnosisExists = await _context.PsychologicalDiagnoses
                .AnyAsync(pd => pd.PsychologicalDiagnosisId == dto.PsychologicalDiagnosisId);
            if (!psychologicalDiagnosisExists)
                return BadRequest("El diagnóstico psicológico especificado no existe");

            var updatedTherapeuticPlan = await _repository.UpdateAsync(dto);
            if (updatedTherapeuticPlan == null)
                return NotFound();

            return Ok(updatedTherapeuticPlan);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("ByPsychologicalDiagnosis/{psychologicalDiagnosisId}")]
        public async Task<ActionResult<List<TherapeuticPlanDTO>>> GetByPsychologicalDiagnosisId(int psychologicalDiagnosisId)
        {
            var repository = _repository as TherapeuticPlanRepository;
            if (repository == null)
                return BadRequest("Repository type not supported");

            var therapeuticPlans = await repository.GetByPsychologicalDiagnosisIdAsync(psychologicalDiagnosisId);
            return Ok(therapeuticPlans);
        }

        [HttpGet("ByMedicalCare/{medicalCareId}")]
        public async Task<ActionResult<List<TherapeuticPlanDTO>>> GetByMedicalCareId(int medicalCareId)
        {
            var repository = _repository as TherapeuticPlanRepository;
            if (repository == null)
                return BadRequest("Repository type not supported");

            var therapeuticPlans = await repository.GetByMedicalCareIdAsync(medicalCareId);
            return Ok(therapeuticPlans);
        }

        [HttpGet("ByPatient/{patientId}")]
        public async Task<ActionResult<List<TherapeuticPlanDTO>>> GetByPatientId(int patientId)
        {
            var repository = _repository as TherapeuticPlanRepository;
            if (repository == null)
                return BadRequest("Repository type not supported");

            var therapeuticPlans = await repository.GetByPatientIdAsync(patientId);
            return Ok(therapeuticPlans);
        }
    }
}