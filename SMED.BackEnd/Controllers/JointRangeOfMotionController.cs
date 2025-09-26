using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JointRangeOfMotionController : ControllerBase
    {
        private readonly IRepository<JointRangeOfMotionDTO, int> _repository;

        public JointRangeOfMotionController(IRepository<JointRangeOfMotionDTO, int> repository)
        {
            _repository = repository;
        }

        // GET: api/JointRangeOfMotion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JointRangeOfMotionDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/JointRangeOfMotion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JointRangeOfMotionDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/JointRangeOfMotion
        [HttpPost]
        public async Task<ActionResult<JointRangeOfMotionDTO>> Create(JointRangeOfMotionDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.JointRangeOfMotionId }, created);
        }

        // PUT: api/JointRangeOfMotion/5
        [HttpPut("{id}")]
        public async Task<ActionResult<JointRangeOfMotionDTO>> Update(int id, JointRangeOfMotionDTO dto)
        {
            if (id != dto.JointRangeOfMotionId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/JointRangeOfMotion/5
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
