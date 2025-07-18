using Microsoft.AspNetCore.Mvc;
using SMED.Shared.DTOs;
using SMED.BackEnd.Repositories.Interface;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhysicalExamTypeController : ControllerBase
    {
        private readonly IRepository<PhysicalExamTypeDTO, int> _repository;

        public PhysicalExamTypeController(IRepository<PhysicalExamTypeDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<PhysicalExamTypeDTO>>> GetAll() =>
            Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<PhysicalExamTypeDTO>> GetById(int id)
        {
            var dto = await _repository.GetByIdAsync(id);
            return dto != null ? Ok(dto) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PhysicalExamTypeDTO>> Create(PhysicalExamTypeDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PhysicalExamTypeDTO dto)
        {
            if (id != dto.Id) return BadRequest();
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
