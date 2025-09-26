using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalEvaluationMembersController : ControllerBase
    {
        private readonly IRepository<MedicalEvaluationMembersDTO, int> _repository;

        public MedicalEvaluationMembersController(IRepository<MedicalEvaluationMembersDTO, int> repository)
        {
            _repository = repository;
        }

        // GET: api/MedicalEvaluationMembers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalEvaluationMembersDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/MedicalEvaluationMembers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalEvaluationMembersDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/MedicalEvaluationMembers
        [HttpPost]
        public async Task<ActionResult<MedicalEvaluationMembersDTO>> Create(MedicalEvaluationMembersDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.MedicalEvaluationMembersId }, created);
        }

        // PUT: api/MedicalEvaluationMembers/5
        [HttpPut("{id}")]
        public async Task<ActionResult<MedicalEvaluationMembersDTO>> Update(int id, MedicalEvaluationMembersDTO dto)
        {
            if (id != dto.MedicalEvaluationMembersId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/MedicalEvaluationMembers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
