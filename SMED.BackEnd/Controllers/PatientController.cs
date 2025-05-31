using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IRepository<PatientDTO, int> _repository;
        private readonly IClinicalHistoryPatientRepository _historyRepository;

        public PatientController(
            IRepository<PatientDTO, int> repository,
            IClinicalHistoryPatientRepository historyRepository)
        {
            _repository = repository;
            _historyRepository = historyRepository;
        }

        // GET: api/patient
        [HttpGet]
        public async Task<ActionResult<List<PatientDTO>>> GetAll()
        {
            var patients = await _repository.GetAllAsync();
            return Ok(patients);
        }

        // GET: api/patient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDTO>> GetById(int id)
        {
            var patient = await _repository.GetByIdAsync(id);
            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        // POST: api/patient
        [HttpPost]
        public async Task<ActionResult<PatientDTO>> Create(PatientDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.PersonId }, created);
        }

        // PUT: api/patient
        [HttpPut]
        public async Task<ActionResult<PatientDTO>> Update(PatientDTO dto)
        {
            var updated = await _repository.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/patient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        // ✅ Reemplaza este método anterior
        [HttpGet("WithClinicalHistory")]
        public async Task<ActionResult<List<PatientDTO>>> GetPatientsWithHistory()
        {
            var withHistory = await _historyRepository.GetPatientsWithHistoryAsync();
            return Ok(withHistory);
        }
    }
}
