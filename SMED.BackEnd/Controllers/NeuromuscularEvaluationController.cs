using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NeuromuscularEvaluationController : ControllerBase
    {
        private readonly IRepository<NeuromuscularEvaluationDTO, int> _repository;
        private readonly NeuromuscularEvaluationRepository _neuromuscularRepository;

        public NeuromuscularEvaluationController(
            IRepository<NeuromuscularEvaluationDTO, int> repository,
            NeuromuscularEvaluationRepository neuromuscularRepository)
        {
            _repository = repository;
            _neuromuscularRepository = neuromuscularRepository;
        }

        // GET: api/NeuromuscularEvaluation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NeuromuscularEvaluationDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/NeuromuscularEvaluation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NeuromuscularEvaluationDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/NeuromuscularEvaluation
        [HttpPost]
        public async Task<ActionResult<NeuromuscularEvaluationDTO>> Create(NeuromuscularEvaluationDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.NeuromuscularEvaluationId }, created);
        }

        // PUT: api/NeuromuscularEvaluation/5
        [HttpPut("{id}")]
        public async Task<ActionResult<NeuromuscularEvaluationDTO>> Update(int id, NeuromuscularEvaluationDTO dto)
        {
            if (id != dto.NeuromuscularEvaluationId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/NeuromuscularEvaluation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("ByCare/{medicalCareId}")]
        public async Task<ActionResult<List<NeuromuscularEvaluationDTO>>> GetByCareId(int medicalCareId)
        {
            var result = await _neuromuscularRepository.GetByCareIdAsync(medicalCareId);
            return Ok(result);
        }
    }
}
