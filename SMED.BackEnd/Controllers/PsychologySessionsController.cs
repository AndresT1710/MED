using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PsychologySessionsController : ControllerBase
    {
        private readonly IRepository<PsychologySessionsDTO, int> _repository;
        private readonly SGISContext _context;

        public PsychologySessionsController(IRepository<PsychologySessionsDTO, int> repository, SGISContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PsychologySessionsDTO>>> GetAll()
        {
            var sessions = await _repository.GetAllAsync();
            return Ok(sessions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PsychologySessionsDTO>> GetById(int id)
        {
            var session = await _repository.GetByIdAsync(id);
            if (session == null)
                return NotFound();

            return Ok(session);
        }

        [HttpPost]
        public async Task<ActionResult<PsychologySessionsDTO>> Create(PsychologySessionsDTO dto)
        {
            // Validar que MedicalCareId exista
            if (dto.MedicalCareId.HasValue)
            {
                var medicalCareExists = await _context.MedicalCares.AnyAsync(mc => mc.CareId == dto.MedicalCareId);
                if (!medicalCareExists)
                    return BadRequest("El MedicalCare especificado no existe");
            }

            var createdSession = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdSession.PsychologySessionsId }, createdSession);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PsychologySessionsDTO>> Update(int id, PsychologySessionsDTO dto)
        {
            if (id != dto.PsychologySessionsId)
                return BadRequest("ID mismatch");

            // Validar que MedicalCareId exista
            if (dto.MedicalCareId.HasValue)
            {
                var medicalCareExists = await _context.MedicalCares.AnyAsync(mc => mc.CareId == dto.MedicalCareId);
                if (!medicalCareExists)
                    return BadRequest("El MedicalCare especificado no existe");
            }

            var updatedSession = await _repository.UpdateAsync(dto);
            if (updatedSession == null)
                return NotFound();

            return Ok(updatedSession);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("ByMedicalCare/{medicalCareId}")]
        public async Task<ActionResult<List<PsychologySessionsDTO>>> GetByMedicalCareId(int medicalCareId)
        {
            var repository = _repository as PsychologySessionsRepository;
            if (repository == null)
                return BadRequest("Repository type not supported");

            var sessions = await repository.GetByMedicalCareIdAsync(medicalCareId);
            return Ok(sessions);
        }

        [HttpGet("ByDateRange")]
        public async Task<ActionResult<List<PsychologySessionsDTO>>> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var repository = _repository as PsychologySessionsRepository;
            if (repository == null)
                return BadRequest("Repository type not supported");

            var sessions = await repository.GetByDateRangeAsync(startDate, endDate);
            return Ok(sessions);
        }

        [HttpGet("ByMedicalDischarge/{medicalDischarge}")]
        public async Task<ActionResult<List<PsychologySessionsDTO>>> GetByMedicalDischarge(bool medicalDischarge)
        {
            var repository = _repository as PsychologySessionsRepository;
            if (repository == null)
                return BadRequest("Repository type not supported");

            var sessions = await repository.GetByMedicalDischargeAsync(medicalDischarge);
            return Ok(sessions);
        }
    }
}