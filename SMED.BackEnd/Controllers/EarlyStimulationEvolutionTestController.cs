using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EarlyStimulationEvolutionTestController : ControllerBase
    {
        private readonly IRepository<EarlyStimulationEvolutionTestDTO, int> _repository;
        private readonly EarlyStimulationEvolutionTestRepository _customRepository;


        public EarlyStimulationEvolutionTestController(IRepository<EarlyStimulationEvolutionTestDTO, int> repository, EarlyStimulationEvolutionTestRepository customRepository)
        {
            _repository = repository;
            _customRepository = customRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EarlyStimulationEvolutionTestDTO>>> GetAll()
            => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<EarlyStimulationEvolutionTestDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("medical-care/{medicalCareId}")]
        public async Task<ActionResult<IEnumerable<EarlyStimulationEvolutionTestDTO>>> GetByMedicalCareId(int medicalCareId)
        {
            var result = await _customRepository.GetByMedicalCareIdAsync(medicalCareId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<EarlyStimulationEvolutionTestDTO>> Create(EarlyStimulationEvolutionTestDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.TestId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EarlyStimulationEvolutionTestDTO>> Update(int id, EarlyStimulationEvolutionTestDTO dto)
        {
            if (id != dto.TestId)
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
    }
}