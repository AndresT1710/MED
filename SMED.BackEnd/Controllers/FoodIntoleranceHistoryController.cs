using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodIntoleranceHistoryController : ControllerBase
    {
        private readonly IRepository<FoodIntoleranceHistoryDTO, int> _repository;

        public FoodIntoleranceHistoryController(IRepository<FoodIntoleranceHistoryDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<FoodIntoleranceHistoryDTO>>> GetAll() =>
        Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<FoodIntoleranceHistoryDTO>> GetById(int id)
        {
            var dto = await _repository.GetByIdAsync(id);
            return dto != null ? Ok(dto) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<FoodIntoleranceHistoryDTO>> Create(FoodIntoleranceHistoryDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.FoodIntoleranceHistoryId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FoodIntoleranceHistoryDTO dto)
        {
            if (id != dto.FoodIntoleranceHistoryId) return BadRequest();
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
