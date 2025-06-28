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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
