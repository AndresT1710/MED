using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AllergyHistoryController : ControllerBase
    {
        private readonly IRepository<AllergyHistoryDTO, int> _repository;

        public AllergyHistoryController(IRepository<AllergyHistoryDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllergyHistoryDTO>>> Get()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AllergyHistoryDTO>> GetById(int id)
        {
            var dto = await _repository.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<AllergyHistoryDTO>> Post(AllergyHistoryDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.AllergyHistoryId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AllergyHistoryDTO>> Put(int id, AllergyHistoryDTO dto)
        {
            if (id != dto.AllergyHistoryId) return BadRequest();
            var updated = await _repository.UpdateAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // ⭐ MÉTODO FALTANTE - AGREGAR ESTE ⭐
        [HttpGet("by-history/{clinicalHistoryId}")]
        public async Task<ActionResult<List<AllergyHistoryDTO>>> GetByClinicalHistoryId(int clinicalHistoryId)
        {
            try
            {
                var allAllergyHistories = await _repository.GetAllAsync();
                var filteredHistories = allAllergyHistories
                    .Where(ah => ah.ClinicalHistoryId == clinicalHistoryId)
                    .ToList();

                return Ok(filteredHistories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
