using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShadeController : ControllerBase
    {
        private readonly IRepository<ShadeDTO, int> _repository;

        public ShadeController(IRepository<ShadeDTO, int> repository)
        {
            _repository = repository;
        }

        // GET: api/Shade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShadeDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Shade/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShadeDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/Shade
        [HttpPost]
        public async Task<ActionResult<ShadeDTO>> Create(ShadeDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ShadeId }, created);
        }

        // PUT: api/Shade/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ShadeDTO>> Update(int id, ShadeDTO dto)
        {
            if (id != dto.ShadeId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/Shade/5
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
