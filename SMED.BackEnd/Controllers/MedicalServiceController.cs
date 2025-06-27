using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalServiceController : ControllerBase
    {
        private readonly IRepository<MedicalServiceDTO, int> _repository;

        public MedicalServiceController(IRepository<MedicalServiceDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<MedicalServiceDTO>>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalServiceDTO>> GetById(int id)
        {
            var medicalService = await _repository.GetByIdAsync(id);
            if (medicalService == null) return NotFound();
            return Ok(medicalService);
        }

        [HttpPost]
        public async Task<ActionResult<MedicalServiceDTO>> Add(MedicalServiceDTO dto)
        {
            var added = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = added.ServiceId }, added);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MedicalServiceDTO>> Update(int id, MedicalServiceDTO dto)
        {
            if (id != dto.ServiceId) return BadRequest("ID mismatch.");
            var updated = await _repository.UpdateAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
