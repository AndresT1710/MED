using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalCareController : ControllerBase
    {
        private readonly IRepository<MedicalCareDTO, int> _repository;

        public MedicalCareController(IRepository<MedicalCareDTO, int> repository) => _repository = repository;

        [HttpGet]
        public async Task<ActionResult<List<MedicalCareDTO>>> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalCareDTO>> GetById(int id) =>
            await _repository.GetByIdAsync(id) is { } dto ? Ok(dto) : NotFound();

        [HttpPost]
        public async Task<ActionResult<MedicalCareDTO>> Create(MedicalCareDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.CareId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MedicalCareDTO dto)
        {
            if (id != dto.CareId) return BadRequest();
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
