using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentalFunctionController : ControllerBase
    {
        private readonly IRepository<MentalFunctionDTO, int> _repository;

        public MentalFunctionController(IRepository<MentalFunctionDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<MentalFunctionDTO>>> GetAll()
        {
            var mentalFunctions = await _repository.GetAllAsync();
            return Ok(mentalFunctions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MentalFunctionDTO>> GetById(int id)
        {
            var mentalFunction = await _repository.GetByIdAsync(id);
            if (mentalFunction == null)
                return NotFound();

            return Ok(mentalFunction);
        }

        [HttpPost]
        public async Task<ActionResult<MentalFunctionDTO>> Create(MentalFunctionDTO dto)
        {
            var createdMentalFunction = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdMentalFunction.MentalFunctionId }, createdMentalFunction);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MentalFunctionDTO>> Update(int id, MentalFunctionDTO dto)
        {
            if (id != dto.MentalFunctionId)
                return BadRequest();

            var updatedMentalFunction = await _repository.UpdateAsync(dto);
            if (updatedMentalFunction == null)
                return NotFound();

            return Ok(updatedMentalFunction);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("ByType/{typeOfMentalFunctionId}")]
        public async Task<ActionResult<List<MentalFunctionDTO>>> GetByTypeOfMentalFunctionId(int typeOfMentalFunctionId)
        {
            var repository = _repository as MentalFunctionRepository;
            if (repository == null)
                return BadRequest("Repository type not supported");

            var mentalFunctions = await repository.GetByTypeOfMentalFunctionIdAsync(typeOfMentalFunctionId);
            return Ok(mentalFunctions);
        }
    }
}