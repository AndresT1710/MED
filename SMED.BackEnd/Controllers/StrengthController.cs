using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StrengthController : ControllerBase
    {
        private readonly IRepository<StrengthDTO, int> _repository;

        public StrengthController(IRepository<StrengthDTO, int> repository)
        {
            _repository = repository;
        }

        // GET: api/Strength
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StrengthDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Strength/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StrengthDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/Strength
        [HttpPost]
        public async Task<ActionResult<StrengthDTO>> Create(StrengthDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.StrengthId }, created);
        }

        // PUT: api/Strength/5
        [HttpPut("{id}")]
        public async Task<ActionResult<StrengthDTO>> Update(int id, StrengthDTO dto)
        {
            if (id != dto.StrengthId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/Strength/5
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
