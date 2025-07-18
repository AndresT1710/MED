using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhysicalExamController : ControllerBase
    {
        private readonly IRepository<PhysicalExamDTO, int> _repository;
        private readonly PhysicalExamRepository _physicalExamRepository;

        public PhysicalExamController(IRepository<PhysicalExamDTO, int> repository, PhysicalExamRepository physicalExamRepository)
        {
            _repository = repository;
            _physicalExamRepository = physicalExamRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<PhysicalExamDTO>>> GetAll() =>
            Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<PhysicalExamDTO>> GetById(int id)
        {
            var dto = await _repository.GetByIdAsync(id);
            return dto != null ? Ok(dto) : NotFound();
        }

        [HttpGet("by-medical-care/{medicalCareId}")]
        public async Task<ActionResult<List<PhysicalExamDTO>>> GetByMedicalCareId(int medicalCareId)
        {
            try
            {
                var result = await _physicalExamRepository.GetByMedicalCareIdAsync(medicalCareId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener exámenes físicos por atención médica: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PhysicalExamDTO>> Create(PhysicalExamDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.PhysicalExamId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PhysicalExamDTO dto)
        {
            if (id != dto.PhysicalExamId) return BadRequest();
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
