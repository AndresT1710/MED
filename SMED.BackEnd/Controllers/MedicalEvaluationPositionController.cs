using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalEvaluationPositionController : ControllerBase
    {
        private readonly IRepository<MedicalEvaluationPositionDTO, int> _repository;

        public MedicalEvaluationPositionController(IRepository<MedicalEvaluationPositionDTO, int> repository)
        {
            _repository = repository;
        }

        // GET: api/MedicalEvaluationPosition
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalEvaluationPositionDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/MedicalEvaluationPosition/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalEvaluationPositionDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/MedicalEvaluationPosition
        [HttpPost]
        public async Task<ActionResult<MedicalEvaluationPositionDTO>> Create(MedicalEvaluationPositionDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.MedicalEvaluationPositionId }, created);
        }

        // PUT: api/MedicalEvaluationPosition/5
        [HttpPut("{id}")]
        public async Task<ActionResult<MedicalEvaluationPositionDTO>> Update(int id, MedicalEvaluationPositionDTO dto)
        {
            if (id != dto.MedicalEvaluationPositionId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/MedicalEvaluationPosition/5
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
