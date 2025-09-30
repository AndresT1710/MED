using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalEvaluationController : ControllerBase
    {
        private readonly IRepository<MedicalEvaluationDTO, int> _repository;

        public MedicalEvaluationController(IRepository<MedicalEvaluationDTO, int> repository)
        {
            _repository = repository;
        }

        // GET: api/MedicalEvaluation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalEvaluationDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/MedicalEvaluation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalEvaluationDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/MedicalEvaluation
        [HttpPost]
        public async Task<ActionResult<MedicalEvaluationDTO>> Create(MedicalEvaluationDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.MedicalEvaluationId }, created);
        }

        // PUT: api/MedicalEvaluation/5
        [HttpPut("{id}")]
        public async Task<ActionResult<MedicalEvaluationDTO>> Update(int id, MedicalEvaluationDTO dto)
        {
            if (id != dto.MedicalEvaluationId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/MedicalEvaluation/5
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
