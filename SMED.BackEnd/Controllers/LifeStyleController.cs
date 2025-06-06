using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LifeStyleController : ControllerBase
    {
        private readonly IRepository<LifeStyleDTO, int> _repository;

        public LifeStyleController(IRepository<LifeStyleDTO, int> repository)
        {
            _repository = repository;
        }

        // GET: api/LifeStyle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LifeStyleDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/LifeStyle/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LifeStyleDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/LifeStyle
        [HttpPost]
        public async Task<ActionResult<LifeStyleDTO>> Create(LifeStyleDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.LifeStyleId }, created);
        }

        // PUT: api/LifeStyle/5
        [HttpPut("{id}")]
        public async Task<ActionResult<LifeStyleDTO>> Update(int id, LifeStyleDTO dto)
        {
            if (id != dto.LifeStyleId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/LifeStyle/5
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
