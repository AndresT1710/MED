using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForbiddenFoodController : ControllerBase
    {
        private readonly IRepository<ForbiddenFoodDTO, int> _repository;

        public ForbiddenFoodController(IRepository<ForbiddenFoodDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ForbiddenFoodDTO>>> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<ForbiddenFoodDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ForbiddenFoodDTO>> Create(ForbiddenFoodDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ForbiddenFoodId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ForbiddenFoodDTO>> Update(int id, ForbiddenFoodDTO dto)
        {
            if (id != dto.ForbiddenFoodId) return BadRequest("ID mismatch");

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
