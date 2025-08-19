using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalProcedureController : ControllerBase
    {
        private readonly IRepository<MedicalProcedureDTO, int> _repository;

        public MedicalProcedureController(IRepository<MedicalProcedureDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<MedicalProcedureDTO>>> GetAll()
        {
            var procedures = await _repository.GetAllAsync();
            return Ok(procedures);
        }

        [HttpGet("by-location/{locationId}")]
        public async Task<ActionResult<List<MedicalProcedureDTO>>> GetByLocation(int locationId)
        {
            if (_repository is SMED.BackEnd.Repositories.Implementations.MedicalProcedureRepository repo)
            {
                var procedures = await repo.GetByLocationAsync(locationId);
                return Ok(procedures);
            }
            return BadRequest("Repository type not supported");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalProcedureDTO>> GetById(int id)
        {
            var procedure = await _repository.GetByIdAsync(id);
            return procedure != null ? Ok(procedure) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<MedicalProcedureDTO>> Create(MedicalProcedureDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ProcedureId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MedicalProcedureDTO dto)
        {
            if (id != dto.ProcedureId) return BadRequest();

            var updated = await _repository.UpdateAsync(dto);
            return updated != null ? Ok(updated) : NotFound();
        }

        [HttpPut("{id}/mark-completed")]
        public async Task<IActionResult> MarkAsCompleted(int id, [FromBody] int treatingPhysicianId)
        {
            var procedure = await _repository.GetByIdAsync(id);
            if (procedure == null) return NotFound();

            procedure.Status = "Realizado";
            procedure.TreatingPhysicianId = treatingPhysicianId;

            var updated = await _repository.UpdateAsync(procedure);
            return updated != null ? Ok(updated) : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpGet("pending/by-location/{locationId}")]
        public async Task<ActionResult<List<MedicalProcedureDTO>>> GetPendingByLocation(int locationId)
        {
            if (_repository is SMED.BackEnd.Repositories.Implementations.MedicalProcedureRepository repo)
            {
                var procedures = await repo.GetByLocationAsync(locationId);
                var pending = procedures.Where(p => p.Status?.ToLower() == "pendiente").ToList();
                return Ok(pending);
            }
            return BadRequest("Repository type not supported");
        }

    }
}
