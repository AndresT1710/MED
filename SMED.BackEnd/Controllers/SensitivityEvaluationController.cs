using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensitivityEvaluationController : ControllerBase
    {
        private readonly IRepository<SensitivityEvaluationDTO, int> _repository;

        public SensitivityEvaluationController(IRepository<SensitivityEvaluationDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensitivityEvaluationDTO>>> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SensitivityEvaluationDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<SensitivityEvaluationDTO>> Create(SensitivityEvaluationDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.SensitivityEvaluationId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SensitivityEvaluationDTO>> Update(int id, SensitivityEvaluationDTO dto)
        {
            if (id != dto.SensitivityEvaluationId) return BadRequest("ID mismatch");

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
