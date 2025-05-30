using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GynecologicalHistoryController : ControllerBase
    {
        private readonly IRepository<GynecologicalHistoryDTO, int> _repository;

        public GynecologicalHistoryController(IRepository<GynecologicalHistoryDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<GynecologicalHistoryDTO>>> GetAll() =>
        Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<GynecologicalHistoryDTO>> GetById(int id)
        {
            var dto = await _repository.GetByIdAsync(id);
            return dto != null ? Ok(dto) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<GynecologicalHistoryDTO>> Create(GynecologicalHistoryDTO dto)
        {
            var result = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.GynecologicalHistoryId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GynecologicalHistoryDTO dto)
        {
            if (id != dto.GynecologicalHistoryId) return BadRequest();
            var updated = await _repository.UpdateAsync(dto);
            return updated != null ? Ok(updated) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }

}
