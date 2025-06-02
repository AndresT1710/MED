using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FamilyHistoryDetailController : ControllerBase
    {
        private readonly IRepository<FamilyHistoryDetailDTO, int> _repository;

        public FamilyHistoryDetailController(IRepository<FamilyHistoryDetailDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<FamilyHistoryDetailDTO>>> GetAll() =>
            Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<FamilyHistoryDetailDTO>> GetById(int id)
        {
            var dto = await _repository.GetByIdAsync(id);
            return dto != null ? Ok(dto) : NotFound();
        }

        [HttpGet("by-clinical-history/{clinicalHistoryId}")]
        public async Task<ActionResult<List<FamilyHistoryDetailDTO>>> GetByClinicalHistoryId(int clinicalHistoryId)
        {
            var allRecords = await _repository.GetAllAsync();
            var filtered = allRecords?.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList() ?? new List<FamilyHistoryDetailDTO>();
            return Ok(filtered);
        }

        [HttpPost]
        public async Task<ActionResult<FamilyHistoryDetailDTO>> Create(FamilyHistoryDetailDTO dto)
        {
            var result = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.FamilyHistoryDetailId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FamilyHistoryDetailDTO dto)
        {
            if (id != dto.FamilyHistoryDetailId) return BadRequest();

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