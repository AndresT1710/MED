using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NeuropsychologicalHistoryController : ControllerBase
    {
        private readonly IRepository<NeuropsychologicalHistoryDTO, int> _neuropsychologicalHistoryRepository;

        public NeuropsychologicalHistoryController(IRepository<NeuropsychologicalHistoryDTO, int> neuropsychologicalHistoryRepository)
        {
            _neuropsychologicalHistoryRepository = neuropsychologicalHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NeuropsychologicalHistoryDTO>>> GetAll()
        {
            var histories = await _neuropsychologicalHistoryRepository.GetAllAsync();
            return Ok(histories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NeuropsychologicalHistoryDTO>> GetById(int id)
        {
            var history = await _neuropsychologicalHistoryRepository.GetByIdAsync(id);
            if (history == null)
                return NotFound();

            return Ok(history);
        }

        [HttpPost]
        public async Task<ActionResult<NeuropsychologicalHistoryDTO>> Add(NeuropsychologicalHistoryDTO dto)
        {
            try
            {
                var created = await _neuropsychologicalHistoryRepository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.NeuropsychologicalHistoryId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<NeuropsychologicalHistoryDTO>> Update(int id, NeuropsychologicalHistoryDTO dto)
        {
            if (id != dto.NeuropsychologicalHistoryId)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = await _neuropsychologicalHistoryRepository.UpdateAsync(dto);
                if (updated == null)
                    return NotFound();

                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _neuropsychologicalHistoryRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}