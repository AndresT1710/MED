using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActionFController : ControllerBase
    {
        private readonly IRepository<ActionFDTO, int> _repository;

        public ActionFController(IRepository<ActionFDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActionFDTO>>> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<ActionFDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ActionFDTO>> Create(ActionFDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ActionId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ActionFDTO>> Update(int id, ActionFDTO dto)
        {
            if (id != dto.ActionId) return BadRequest("ID mismatch");

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
