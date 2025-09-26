using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NeurologicalExamTypeController : ControllerBase
    {
        private readonly IRepository<NeurologicalExamTypeDTO, int> _neurologicalExamTypeRepository;

        public NeurologicalExamTypeController(IRepository<NeurologicalExamTypeDTO, int> neurologicalExamTypeRepository)
        {
            _neurologicalExamTypeRepository = neurologicalExamTypeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NeurologicalExamTypeDTO>>> GetAll()
        {
            var examTypes = await _neurologicalExamTypeRepository.GetAllAsync();
            return Ok(examTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NeurologicalExamTypeDTO>> GetById(int id)
        {
            var examType = await _neurologicalExamTypeRepository.GetByIdAsync(id);
            if (examType == null)
                return NotFound();

            return Ok(examType);
        }

        [HttpPost]
        public async Task<ActionResult<NeurologicalExamTypeDTO>> Add(NeurologicalExamTypeDTO dto)
        {
            try
            {
                var created = await _neurologicalExamTypeRepository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.NeurologicalExamTypeId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<NeurologicalExamTypeDTO>> Update(int id, NeurologicalExamTypeDTO dto)
        {
            if (id != dto.NeurologicalExamTypeId)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = await _neurologicalExamTypeRepository.UpdateAsync(dto);
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
            var success = await _neurologicalExamTypeRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}