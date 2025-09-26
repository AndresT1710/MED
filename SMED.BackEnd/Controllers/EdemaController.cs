using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EdemaController : ControllerBase
    {
        private readonly IRepository<EdemaDTO, int> _repository;

        public EdemaController(IRepository<EdemaDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EdemaDTO>>> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<EdemaDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<EdemaDTO>> Create(EdemaDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.EdemaId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EdemaDTO>> Update(int id, EdemaDTO dto)
        {
            if (id != dto.EdemaId) return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return !deleted ? NotFound() : NoContent();
        }
    }
}
