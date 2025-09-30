using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PosturalEvaluationController : ControllerBase
    {
        private readonly IRepository<PosturalEvaluationDTO, int> _repository;

        public PosturalEvaluationController(IRepository<PosturalEvaluationDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosturalEvaluationDTO>>> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<PosturalEvaluationDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PosturalEvaluationDTO>> Create(PosturalEvaluationDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.PosturalEvaluationId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PosturalEvaluationDTO>> Update(int id, PosturalEvaluationDTO dto)
        {
            if (id != dto.PosturalEvaluationId) return BadRequest("ID mismatch");

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
