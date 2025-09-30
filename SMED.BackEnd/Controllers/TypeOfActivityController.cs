using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeOfActivityController : ControllerBase
    {
        private readonly IRepository<TypeOfActivityDTO, int> _repository;

        public TypeOfActivityController(IRepository<TypeOfActivityDTO, int> repository)
        {
            _repository = repository;
        }

        // GET: api/TypeOfActivity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeOfActivityDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/TypeOfActivity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeOfActivityDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/TypeOfActivity
        [HttpPost]
        public async Task<ActionResult<TypeOfActivityDTO>> Create(TypeOfActivityDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.TypeOfActivityId }, created);
        }

        // PUT: api/TypeOfActivity/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TypeOfActivityDTO>> Update(int id, TypeOfActivityDTO dto)
        {
            if (id != dto.TypeOfActivityId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/TypeOfActivity/5
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
