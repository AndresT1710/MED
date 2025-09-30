using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeOfMedicalEvaluationController : ControllerBase
    {
        private readonly IRepository<TypeOfMedicalEvaluationDTO, int> _repository;

        public TypeOfMedicalEvaluationController(IRepository<TypeOfMedicalEvaluationDTO, int> repository)
        {
            _repository = repository;
        }

        // GET: api/TypeOfMedicalEvaluation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeOfMedicalEvaluationDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/TypeOfMedicalEvaluation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeOfMedicalEvaluationDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/TypeOfMedicalEvaluation
        [HttpPost]
        public async Task<ActionResult<TypeOfMedicalEvaluationDTO>> Create(TypeOfMedicalEvaluationDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.TypeOfMedicalEvaluationId }, created);
        }

        // PUT: api/TypeOfMedicalEvaluation/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TypeOfMedicalEvaluationDTO>> Update(int id, TypeOfMedicalEvaluationDTO dto)
        {
            if (id != dto.TypeOfMedicalEvaluationId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/TypeOfMedicalEvaluation/5
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
