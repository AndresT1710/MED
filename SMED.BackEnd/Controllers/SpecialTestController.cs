using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecialTestController : ControllerBase
    {
        private readonly IRepository<SpecialTestDTO, int> _repository;
        private readonly SpecialTestRepository _specialTestRepository;

        public SpecialTestController(
            IRepository<SpecialTestDTO, int> repository,
            SpecialTestRepository specialTestRepository)
        {
            _repository = repository;
            _specialTestRepository = specialTestRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialTestDTO>>> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialTestDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<SpecialTestDTO>> Create(SpecialTestDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.SpecialTestId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SpecialTestDTO>> Update(int id, SpecialTestDTO dto)
        {
            if (id != dto.SpecialTestId) return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return !deleted ? NotFound() : NoContent();
        }

        [HttpGet("ByCare/{medicalCareId}")]
        public async Task<ActionResult<List<SpecialTestDTO>>> GetByCareId(int medicalCareId)
        {
            var result = await _specialTestRepository.GetByCareIdAsync(medicalCareId);
            return Ok(result);
        }
    }
}
