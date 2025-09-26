using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhysiotherapyTreatmentController : ControllerBase
    {
        private readonly IRepository<PhysiotherapyTreatmentDTO, int> _repository;

        public PhysiotherapyTreatmentController(IRepository<PhysiotherapyTreatmentDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhysiotherapyTreatmentDTO>>> GetAll()
            => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<PhysiotherapyTreatmentDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PhysiotherapyTreatmentDTO>> Create(PhysiotherapyTreatmentDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.PhysiotherapyTreatmentId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PhysiotherapyTreatmentDTO>> Update(int id, PhysiotherapyTreatmentDTO dto)
        {
            if (id != dto.PhysiotherapyTreatmentId) return BadRequest("ID mismatch");

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
