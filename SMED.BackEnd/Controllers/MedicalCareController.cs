using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalCareController : ControllerBase
    {
        private readonly IRepository<MedicalCareDTO, int> _repository;
        private readonly MedicalCareRepository _medicalCareRepository;

        public MedicalCareController(IRepository<MedicalCareDTO, int> repository, MedicalCareRepository medicalCareRepository)
        {
            _repository = repository;
            _medicalCareRepository = medicalCareRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<MedicalCareDTO>>> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("nursing")]
        public async Task<ActionResult<List<MedicalCareDTO>>> GetNursingCare()
        {
            try
            {
                var result = await _medicalCareRepository.GetNursingCareAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener atenciones de enfermería: {ex.Message}");
            }
        }

        [HttpGet("by-area-and-date")]
        public async Task<ActionResult<List<MedicalCareDTO>>> GetByAreaAndDate([FromQuery] string area, [FromQuery] DateTime? date = null)
        {
            try
            {
                var result = await _medicalCareRepository.GetByAreaAndDateAsync(area, date);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener atenciones por área y fecha: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalCareDTO>> GetById(int id) =>
            await _repository.GetByIdAsync(id) is { } dto ? Ok(dto) : NotFound();

        [HttpGet("by-document/{documentNumber}")]
        public async Task<ActionResult<List<MedicalCareDTO>>> GetByPatientDocument(string documentNumber)
        {
            try
            {
                var result = await _medicalCareRepository.GetByPatientDocumentAsync(documentNumber);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al buscar atenciones por cédula: {ex.Message}");
            }
        }

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
