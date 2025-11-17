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
    public class AdvanceController : ControllerBase
    {
        private readonly IRepository<AdvanceDTO, int> _repository;
        private readonly SGISContext _context;

        public AdvanceController(IRepository<AdvanceDTO, int> repository, SGISContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<AdvanceDTO>>> GetAll()
        {
            var advances = await _repository.GetAllAsync();
            return Ok(advances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdvanceDTO>> GetById(int id)
        {
            var advance = await _repository.GetByIdAsync(id);
            if (advance == null)
                return NotFound();

            return Ok(advance);
        }

        [HttpPost]
        public async Task<ActionResult<AdvanceDTO>> Create(AdvanceDTO dto)
        {
            // Validar que la sesión psicológica esté especificada
            if (!dto.PsychologySessionId.HasValue)
                return BadRequest("Debe especificar PsychologySessionId");

            // Validar que la sesión psicológica exista
            if (dto.PsychologySessionId.HasValue)
            {
                var psychologySessionExists = await _context.PsychologySessions.AnyAsync(ps => ps.PsychologySessionsId == dto.PsychologySessionId);
                if (!psychologySessionExists)
                    return BadRequest("La sesión psicológica especificada no existe");
            }

            var createdAdvance = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdAdvance.AdvanceId }, createdAdvance);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AdvanceDTO>> Update(int id, AdvanceDTO dto)
        {
            if (id != dto.AdvanceId)
                return BadRequest();

            // Validaciones similares a Create
            if (!dto.PsychologySessionId.HasValue)
                return BadRequest("Debe especificar PsychologySessionId");

            if (dto.PsychologySessionId.HasValue)
            {
                var psychologySessionExists = await _context.PsychologySessions.AnyAsync(ps => ps.PsychologySessionsId == dto.PsychologySessionId);
                if (!psychologySessionExists)
                    return BadRequest("La sesión psicológica especificada no existe");
            }

            var updatedAdvance = await _repository.UpdateAsync(dto);
            if (updatedAdvance == null)
                return NotFound();

            return Ok(updatedAdvance);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("ByPsychologySession/{psychologySessionId}")]
        public async Task<ActionResult<List<AdvanceDTO>>> GetByPsychologySessionId(int psychologySessionId)
        {
            var repository = _repository as AdvanceRepository;
            if (repository == null)
                return BadRequest("Repository type not supported");

            var advances = await repository.GetByPsychologySessionIdAsync(psychologySessionId);
            return Ok(advances);
        }

        [HttpGet("ByDateRange")]
        public async Task<ActionResult<List<AdvanceDTO>>> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var repository = _repository as AdvanceRepository;
            if (repository == null)
                return BadRequest("Repository type not supported");

            var advances = await repository.GetByDateRangeAsync(startDate, endDate);
            return Ok(advances);
        }

        [HttpGet("ByMedicalCare/{medicalCareId}")]
        public async Task<ActionResult<List<AdvanceDTO>>> GetByMedicalCareId(int medicalCareId)
        {
            var repository = _repository as AdvanceRepository;
            if (repository == null)
                return BadRequest("Repository type not supported");

            var advances = await repository.GetByMedicalCareIdAsync(medicalCareId);
            return Ok(advances);
        }
    }
}