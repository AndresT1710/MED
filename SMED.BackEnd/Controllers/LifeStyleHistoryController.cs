using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LifeStyleHistoryController : ControllerBase
    {
        private readonly IRepository<LifeStyleHistoryDTO, int> _repository;

        public LifeStyleHistoryController(IRepository<LifeStyleHistoryDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LifeStyleHistoryDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LifeStyleHistoryDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<LifeStyleHistoryDTO>> Create(LifeStyleHistoryDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.LifeStyleHistoryId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LifeStyleHistoryDTO>> Update(int id, LifeStyleHistoryDTO dto)
        {
            if (id != dto.LifeStyleHistoryId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("by-clinical-history/{clinicalHistoryId}")]
        public async Task<ActionResult<List<LifeStyleHistoryDTO>>> GetByClinicalHistoryId(int clinicalHistoryId)
        {
            var allRecords = await _repository.GetAllAsync();
            var filtered = allRecords?.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList() ?? new List<LifeStyleHistoryDTO>();
            return Ok(filtered);
        }
    }
}
