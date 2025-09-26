using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IRepository<ActivityDTO, int> _repository;

        public ActivityController(IRepository<ActivityDTO, int> repository)
        {
            _repository = repository;
        }

        // GET: api/Activity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Activity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/Activity
        [HttpPost]
        public async Task<ActionResult<ActivityDTO>> Create(ActivityDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ActivityId }, created);
        }

        // PUT: api/Activity/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ActivityDTO>> Update(int id, ActivityDTO dto)
        {
            if (id != dto.ActivityId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/Activity/5
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
