using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaritalStatusController : ControllerBase
    {
        private readonly IRepository<MaritalStatusDTO, int> _repository;

        public MaritalStatusController(IRepository<MaritalStatusDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<MaritalStatusDTO>>> GetAll()
        {
            var maritalStatuses = await _repository.GetAllAsync();
            return Ok(maritalStatuses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaritalStatusDTO>> GetById(int id)
        {
            var maritalStatus = await _repository.GetByIdAsync(id);
            if (maritalStatus == null) return NotFound();
            return Ok(maritalStatus);
        }

        [HttpPost]
        public async Task<ActionResult<MaritalStatusDTO>> Create(MaritalStatusDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MaritalStatusDTO>> Update(int id, MaritalStatusDTO dto)
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