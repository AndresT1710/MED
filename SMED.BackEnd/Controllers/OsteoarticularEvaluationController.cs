using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OsteoarticularEvaluationController : ControllerBase
    {
        private readonly IRepository<OsteoarticularEvaluationDTO, int> _repository;
        private readonly OsteoarticularEvaluationRepository _osteoarticularRepository;

        public OsteoarticularEvaluationController(
            IRepository<OsteoarticularEvaluationDTO, int> repository,
            OsteoarticularEvaluationRepository osteoarticularRepository)
        {
            _repository = repository;
            _osteoarticularRepository = osteoarticularRepository;
        }
        // GET: api/OsteoarticularEvaluation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OsteoarticularEvaluationDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/OsteoarticularEvaluation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OsteoarticularEvaluationDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/OsteoarticularEvaluation
        [HttpPost]
        public async Task<ActionResult<OsteoarticularEvaluationDTO>> Create(OsteoarticularEvaluationDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.OsteoarticularEvaluationId }, created);
        }

        // PUT: api/OsteoarticularEvaluation/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OsteoarticularEvaluationDTO>> Update(int id, OsteoarticularEvaluationDTO dto)
        {
            if (id != dto.OsteoarticularEvaluationId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/OsteoarticularEvaluation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("ByCare/{medicalCareId}")]
        public async Task<ActionResult<List<OsteoarticularEvaluationDTO>>> GetByCareId(int medicalCareId)
        {
            var result = await _osteoarticularRepository.GetByCareIdAsync(medicalCareId);
            return Ok(result);
        }
    }
}
