using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplementaryExamsController : ControllerBase
    {
        private readonly ComplementaryExamsRepository _repository;

        public ComplementaryExamsController(ComplementaryExamsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/ComplementaryExams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComplementaryExamsDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/ComplementaryExams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComplementaryExamsDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/ComplementaryExams
        [HttpPost]
        public async Task<ActionResult<ComplementaryExamsDTO>> Create(ComplementaryExamsDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ComplementaryExamsId }, created);
        }

        // PUT: api/ComplementaryExams/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ComplementaryExamsDTO>> Update(int id, ComplementaryExamsDTO dto)
        {
            if (id != dto.ComplementaryExamsId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/ComplementaryExams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("ByCare/{medicalCareId}")]
        public async Task<ActionResult<List<ComplementaryExamsDTO>>> GetByCareId(int medicalCareId)
        {
            var result = await _repository.GetByCareIdAsync(medicalCareId);
            return Ok(result);
        }
    }
}
