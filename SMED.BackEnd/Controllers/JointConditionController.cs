using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JointConditionController : ControllerBase
    {
        private readonly IRepository<JointConditionDTO, int> _repository;

        public JointConditionController(IRepository<JointConditionDTO, int> repository)
        {
            _repository = repository;
        }

        // GET: api/JointCondition
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JointConditionDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/JointCondition/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JointConditionDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/JointCondition
        [HttpPost]
        public async Task<ActionResult<JointConditionDTO>> Create(JointConditionDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.JointConditionId }, created);
        }

        // PUT: api/JointCondition/5
        [HttpPut("{id}")]
        public async Task<ActionResult<JointConditionDTO>> Update(int id, JointConditionDTO dto)
        {
            if (id != dto.JointConditionId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/JointCondition/5
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
