using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PainScaleController : ControllerBase
    {
        private readonly IRepository<PainScaleDTO, int> _repository;
        private readonly PainScaleRepository _painScaleRepository;

        public PainScaleController(
            IRepository<PainScaleDTO, int> repository,
            PainScaleRepository painScaleRepository)
        {
            _repository = repository;
            _painScaleRepository = painScaleRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PainScaleDTO>>> GetAll()
            => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<PainScaleDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PainScaleDTO>> Create(PainScaleDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.PainScaleId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PainScaleDTO>> Update(int id, PainScaleDTO dto)
        {
            if (id != dto.PainScaleId) return BadRequest("ID mismatch");

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
        public async Task<ActionResult<List<PainScaleDTO>>> GetByCareId(int medicalCareId)
        {
            var result = await _painScaleRepository.GetByCareIdAsync(medicalCareId);
            return Ok(result);
        }
    }
}
