using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkinEvaluationController : ControllerBase
    {
        private readonly IRepository<SkinEvaluationDTO, int> _repository;
        private readonly SkinEvaluationRepository _skinEvaluationRepository;

        public SkinEvaluationController(
            IRepository<SkinEvaluationDTO, int> repository,
            SkinEvaluationRepository skinEvaluationRepository)
        {
            _repository = repository;
            _skinEvaluationRepository = skinEvaluationRepository;
        }

        // GET: api/SkinEvaluation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkinEvaluationDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/SkinEvaluation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkinEvaluationDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/SkinEvaluation
        [HttpPost]
        public async Task<ActionResult<SkinEvaluationDTO>> Create(SkinEvaluationDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.SkinEvaluationId }, created);
        }

        // PUT: api/SkinEvaluation/5
        [HttpPut("{id}")]
        public async Task<ActionResult<SkinEvaluationDTO>> Update(int id, SkinEvaluationDTO dto)
        {
            if (id != dto.SkinEvaluationId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpGet("ByCare/{medicalCareId}")]
        public async Task<ActionResult<List<SkinEvaluationDTO>>> GetByCareId(int medicalCareId)
        {
            var result = await _skinEvaluationRepository.GetByCareIdAsync(medicalCareId);
            return Ok(result);
        }

        // DELETE: api/SkinEvaluation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
