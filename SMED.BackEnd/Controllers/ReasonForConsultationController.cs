using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReasonForConsultationController : ControllerBase
    {
        private readonly ReasonForConsultationRepository _reasonRepository;

        public ReasonForConsultationController(
            ReasonForConsultationRepository reasonRepository)
        {
            _reasonRepository = reasonRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReasonForConsultationDTO>>> GetAll() =>
            Ok(await _reasonRepository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<ReasonForConsultationDTO>> GetById(int id)
        {
            var dto = await _reasonRepository.GetByIdAsync(id);
            return dto != null ? Ok(dto) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ReasonForConsultationDTO>> Create(ReasonForConsultationDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("El DTO no puede ser nulo");

                if (string.IsNullOrWhiteSpace(dto.Description))
                    return BadRequest("La descripción es requerida");

                if (dto.MedicalCareId <= 0)
                    return BadRequest("MedicalCareId no válido");

                var created = await _reasonRepository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ReasonForConsultationDTO dto)
        {
            if (id != dto.Id) return BadRequest();
            var updated = await _reasonRepository.UpdateAsync(dto);
            return updated != null ? Ok(updated) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _reasonRepository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }


        [HttpGet("ByCare/{medicalCareId}")]
        public async Task<ActionResult<List<ReasonForConsultationDTO>>> GetByCareId(int medicalCareId)
        {
            var result = await _reasonRepository.GetByCareIdAsync(medicalCareId);
            return Ok(result);
        }

        // GET: api/ReasonForConsultation/ByCare/5/First
        [HttpGet("ByCare/{medicalCareId}/First")]
        public async Task<ActionResult<ReasonForConsultationDTO>> GetFirstByCareId(int medicalCareId)
        {
            var result = await _reasonRepository.GetFirstByCareIdAsync(medicalCareId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }

}
