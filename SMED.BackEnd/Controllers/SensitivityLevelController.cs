using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensitivityLevelController : ControllerBase
    {
        private readonly IRepository<SensitivityLevelDTO, int> _repository;

        public SensitivityLevelController(IRepository<SensitivityLevelDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensitivityLevelDTO>>> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SensitivityLevelDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<SensitivityLevelDTO>> Create(SensitivityLevelDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.SensitivityLevelId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SensitivityLevelDTO>> Update(int id, SensitivityLevelDTO dto)
        {
            if (id != dto.SensitivityLevelId) return BadRequest("ID mismatch");

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
