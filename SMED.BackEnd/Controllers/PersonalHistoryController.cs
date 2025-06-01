using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using Microsoft.Extensions.Logging;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalHistoryController : ControllerBase
    {
        private readonly IRepository<PersonalHistoryDTO, int> _repository;
        private readonly ILogger<PersonalHistoryController> _logger;

        public PersonalHistoryController(
            IRepository<PersonalHistoryDTO, int> repository,
            ILogger<PersonalHistoryController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // Obtener todos los antecedentes personales (quizá para debug o admin)
        [HttpGet]
        public async Task<ActionResult<List<PersonalHistoryDTO>>> GetAll()
        {
            var all = await _repository.GetAllAsync();
            return Ok(all);
        }

        // Obtener antecedente por id
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalHistoryDTO>> GetById(int id)
        {
            var dto = await _repository.GetByIdAsync(id);
            return dto != null ? Ok(dto) : NotFound();
        }

        // Crear antecedente personal
        [HttpPost]
        public async Task<ActionResult<PersonalHistoryDTO>> Create(PersonalHistoryDTO dto)
        {
            // IMPORTANTE: El MedicalRecordNumber debe venir del dto correctamente seteado (desde frontend)
            var result = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.PersonalHistoryId }, result);
        }

        // Actualizar antecedente personal
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PersonalHistoryDTO dto)
        {
            if (id != dto.PersonalHistoryId)
                return BadRequest();

            var updated = await _repository.UpdateAsync(dto);
            return updated != null ? Ok(updated) : NotFound();
        }

        // Eliminar antecedente personal
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        // Obtener antecedentes personales filtrados por ClinicalHistoryId
        [HttpGet("by-history/{clinicalHistoryId}")]
        public async Task<ActionResult<List<PersonalHistoryDTO>>> GetByClinicalHistory(int clinicalHistoryId)
        {
            try
            {
                // Asumiendo que el repositorio tiene método para filtrar, si no, esto deberías implementarlo
                var all = await _repository.GetAllAsync();

                // Filtrar solo los que coinciden con el ClinicalHistoryId solicitado
                var filtered = all
                    .Where(ph => ph.ClinicalHistoryId == clinicalHistoryId)
                    .ToList();

                _logger.LogInformation($"Retrieved {filtered.Count} personal history records for ClinicalHistoryId={clinicalHistoryId}");

                return Ok(filtered);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving personal history for ClinicalHistoryId={clinicalHistoryId}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
