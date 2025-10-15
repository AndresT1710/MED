using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenderController : ControllerBase
    {
        private readonly IRepository<GenderDTO, int> _repository;

        public GenderController(IRepository<GenderDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenderDTO>>> GetAll()
        {
            var genders = await _repository.GetAllAsync();
            return Ok(genders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenderDTO>> GetById(int id)
        {
            var gender = await _repository.GetByIdAsync(id);
            if (gender == null) return NotFound();
            return Ok(gender);
        }

        [HttpPost]
        public async Task<ActionResult<GenderDTO>> Create(GenderDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenderDTO>> Update(int id, GenderDTO dto)
        {
            if (id != dto.Id) return BadRequest();
            var updated = await _repository.UpdateAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}