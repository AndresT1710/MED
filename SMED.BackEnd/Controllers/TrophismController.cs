using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrophismController : ControllerBase
    {
        private readonly IRepository<TrophismDTO, int> _repository;

        public TrophismController(IRepository<TrophismDTO, int> repository)
        {
            _repository = repository;
        }

        // GET: api/Trophism
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrophismDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Trophism/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrophismDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/Trophism
        [HttpPost]
        public async Task<ActionResult<TrophismDTO>> Create(TrophismDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.TrophismId }, created);
        }

        // PUT: api/Trophism/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TrophismDTO>> Update(int id, TrophismDTO dto)
        {
            if (id != dto.TrophismId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/Trophism/5
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
