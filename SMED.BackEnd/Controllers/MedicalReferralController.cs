using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalReferralController : ControllerBase
    {
        private readonly IRepository<MedicalReferralDTO, int> _repository;

        public MedicalReferralController(IRepository<MedicalReferralDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<MedicalReferralDTO>>> GetAll() =>
            Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalReferralDTO>> GetById(int id)
        {
            var dto = await _repository.GetByIdAsync(id);
            return dto != null ? Ok(dto) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<MedicalReferralDTO>> Create(MedicalReferralDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MedicalReferralDTO dto)
        {
            if (id != dto.Id) return BadRequest();
            var updated = await _repository.UpdateAsync(dto);
            return updated != null ? Ok(updated) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        // En MedicalReferralController.cs, agrega estos endpoints:
        [HttpGet("medical-care/{medicalCareId}")]
        public async Task<ActionResult<List<MedicalReferralDTO>>> GetByMedicalCareId(int medicalCareId)
        {
            var repository = _repository as MedicalReferralRepository;
            if (repository == null) return BadRequest("Repository type not supported");

            var referrals = await repository.GetByMedicalCareIdAsync(medicalCareId);
            return Ok(referrals);
        }

        [HttpGet("pending")]
        public async Task<ActionResult<List<MedicalReferralDTO>>> GetPendingReferrals()
        {
            var repository = _repository as MedicalReferralRepository;
            if (repository == null) return BadRequest("Repository type not supported");

            var referrals = await repository.GetPendingReferralsAsync();
            return Ok(referrals);
        }

        [HttpGet("urgent")]
        public async Task<ActionResult<List<MedicalReferralDTO>>> GetUrgentReferrals()
        {
            var repository = _repository as MedicalReferralRepository;
            if (repository == null) return BadRequest("Repository type not supported");

            var referrals = await repository.GetUrgentReferralsAsync();
            return Ok(referrals);
        }

        [HttpGet("location/{locationId}")]
        public async Task<ActionResult<List<MedicalReferralDTO>>> GetByLocation(int locationId)
        {
            var repository = _repository as MedicalReferralRepository;
            if (repository == null) return BadRequest("Repository type not supported");

            var referrals = await repository.GetByLocationAsync(locationId);
            return Ok(referrals);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateReferralStatusRequest request)
        {
            var repository = _repository as MedicalReferralRepository;
            if (repository == null) return BadRequest("Repository type not supported");

            var updated = await repository.UpdateStatusAsync(id, request.Status, request.AttendedByProfessionalId);
            return updated ? Ok() : NotFound();
        }

        public class UpdateReferralStatusRequest
        {
            public string Status { get; set; } = null!;
            public int? AttendedByProfessionalId { get; set; }
        }
    }

}
