using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurgeryHistoryController : ControllerBase
    {
        private readonly IRepository<SurgeryHistoryDTO, int> _repository;

        public SurgeryHistoryController(IRepository<SurgeryHistoryDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<SurgeryHistoryDTO>>> GetAll() =>
        Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<SurgeryHistoryDTO>> GetById(int id)
        {
            var dto = await _repository.GetByIdAsync(id);
            return dto != null ? Ok(dto) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<SurgeryHistoryDTO>> Create(SurgeryHistoryDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.SurgeryHistoryId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SurgeryHistoryDTO dto)
        {
            if (id != dto.SurgeryHistoryId) return BadRequest();
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
