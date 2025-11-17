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
    public class ActivityController : ControllerBase
    {
        private readonly IRepository<ActivityDTO, int> _repository;
        private readonly SGISContext _context;

        public ActivityController(IRepository<ActivityDTO, int> repository, SGISContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ActivityDTO>> Create(ActivityDTO dto)
        {
            if (dto.PsychologySessionId.HasValue)
            {
                var psychologySessionExists = await _context.PsychologySessions
                    .AnyAsync(ps => ps.PsychologySessionsId == dto.PsychologySessionId);

                if (!psychologySessionExists)
                    return BadRequest("La sesión psicológica especificada no existe");
            }

            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ActivityId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ActivityDTO>> Update(int id, ActivityDTO dto)
        {
            if (id != dto.ActivityId)
                return BadRequest("ID mismatch");

            if (dto.PsychologySessionId.HasValue)
            {
                var psychologySessionExists = await _context.PsychologySessions
                    .AnyAsync(ps => ps.PsychologySessionsId == dto.PsychologySessionId);

                if (!psychologySessionExists)
                    return BadRequest("La sesión psicológica especificada no existe");
            }

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("ByPsychologySession/{psychologySessionId}")]
        public async Task<ActionResult<List<ActivityDTO>>> GetByPsychologySessionId(int psychologySessionId)
        {
            var repository = _repository as ActivityRepository;
            if (repository == null)
                return BadRequest("Repository type not supported");

            var activities = await repository.GetByPsychologySessionIdAsync(psychologySessionId);
            return Ok(activities);
        }

        [HttpGet("ByTypeOfActivity/{typeOfActivityId}")]
        public async Task<ActionResult<List<ActivityDTO>>> GetByTypeOfActivityId(int typeOfActivityId)
        {
            var repository = _repository as ActivityRepository;
            if (repository == null)
                return BadRequest("Repository type not supported");

            var activities = await repository.GetByTypeOfActivityIdAsync(typeOfActivityId);
            return Ok(activities);
        }
    }
}
