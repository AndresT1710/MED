using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NeurologicalExamController : ControllerBase
    {
        private readonly IRepository<NeurologicalExamDTO, int> _neurologicalExamRepository;

        public NeurologicalExamController(IRepository<NeurologicalExamDTO, int> neurologicalExamRepository)
        {
            _neurologicalExamRepository = neurologicalExamRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NeurologicalExamDTO>>> GetAll()
        {
            var records = await _neurologicalExamRepository.GetAllAsync();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NeurologicalExamDTO>> GetById(int id)
        {
            var record = await _neurologicalExamRepository.GetByIdAsync(id);
            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult<NeurologicalExamDTO>> Add(NeurologicalExamDTO dto)
        {
            try
            {
                var created = await _neurologicalExamRepository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.NeurologicalExamId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<NeurologicalExamDTO>> Update(int id, NeurologicalExamDTO dto)
        {
            if (id != dto.NeurologicalExamId)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = await _neurologicalExamRepository.UpdateAsync(dto);
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
            var success = await _neurologicalExamRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}