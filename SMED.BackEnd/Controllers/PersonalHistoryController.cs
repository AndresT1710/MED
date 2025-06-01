using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalHistoryController : ControllerBase
    {
        private readonly IRepository<PersonalHistoryDTO, int> _repository;

        public PersonalHistoryController(IRepository<PersonalHistoryDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonalHistoryDTO>>> GetAll() =>
        Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalHistoryDTO>> GetById(int id)
        {
            var dto = await _repository.GetByIdAsync(id);
            return dto != null ? Ok(dto) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PersonalHistoryDTO>> Create(PersonalHistoryDTO dto)
        {
            var result = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.PersonalHistoryId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PersonalHistoryDTO dto)
        {
            if (id != dto.PersonalHistoryId) return BadRequest();

            var updated = await _repository.UpdateAsync(dto);
            return updated != null ? Ok(updated) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }


        [HttpGet("by-history/{clinicalHistoryId}")]
        public async Task<ActionResult<List<PersonalHistoryDTO>>> GetByClinicalHistory(int clinicalHistoryId)
        {
            var all = await _repository.GetAllAsync();
            var filtered = all.Where(ph => ph.ClinicalHistoryId == clinicalHistoryId).ToList();
            return Ok(filtered);
        }


    }

}
