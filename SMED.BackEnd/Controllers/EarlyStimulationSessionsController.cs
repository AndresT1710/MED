using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.BackEnd.Repositories.Implementations;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EarlyStimulationSessionsController : ControllerBase
    {
        private readonly IRepository<EarlyStimulationSessionsDTO, int> _repository;
        private readonly EarlyStimulationSessionsRepository _customRepository;

        public EarlyStimulationSessionsController(
            IRepository<EarlyStimulationSessionsDTO, int> repository,
            EarlyStimulationSessionsRepository customRepository)
        {
            _repository = repository;
            _customRepository = customRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EarlyStimulationSessionsDTO>>> GetAll()
            => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<EarlyStimulationSessionsDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<EarlyStimulationSessionsDTO>> Create(EarlyStimulationSessionsDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.SessionsId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EarlyStimulationSessionsDTO>> Update(int id, EarlyStimulationSessionsDTO dto)
        {
            if (id != dto.SessionsId)
                return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return !deleted ? NotFound() : NoContent();
        }

        [HttpGet("by-care/{medicalCareId}")]
        public async Task<ActionResult<IEnumerable<EarlyStimulationSessionsDTO>>> GetByMedicalCareId(int medicalCareId)
        {
            var result = await _customRepository.GetByMedicalCareIdAsync(medicalCareId);
            return Ok(result);
        }
    }
}
