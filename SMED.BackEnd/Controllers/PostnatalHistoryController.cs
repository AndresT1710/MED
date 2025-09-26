using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostnatalHistoryController : ControllerBase
    {
        private readonly IRepository<PostnatalHistoryDTO, int> _postnatalHistoryRepository;

        public PostnatalHistoryController(IRepository<PostnatalHistoryDTO, int> postnatalHistoryRepository)
        {
            _postnatalHistoryRepository = postnatalHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostnatalHistoryDTO>>> GetAll()
        {
            var histories = await _postnatalHistoryRepository.GetAllAsync();
            return Ok(histories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostnatalHistoryDTO>> GetById(int id)
        {
            var history = await _postnatalHistoryRepository.GetByIdAsync(id);
            if (history == null)
                return NotFound();

            return Ok(history);
        }

        [HttpPost]
        public async Task<ActionResult<PostnatalHistoryDTO>> Add(PostnatalHistoryDTO dto)
        {
            try
            {
                var created = await _postnatalHistoryRepository.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.PostNatalId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PostnatalHistoryDTO>> Update(int id, PostnatalHistoryDTO dto)
        {
            if (id != dto.PostNatalId)
                return BadRequest("ID mismatch.");

            try
            {
                var updated = await _postnatalHistoryRepository.UpdateAsync(dto);
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
            var success = await _postnatalHistoryRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}