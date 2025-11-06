using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesOfMentalFunctionsController : ControllerBase
    {
        private readonly IRepository<TypesOfMentalFunctionsDTO, int> _repository;

        public TypesOfMentalFunctionsController(IRepository<TypesOfMentalFunctionsDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TypesOfMentalFunctionsDTO>>> GetAll()
        {
            var typesOfMentalFunctions = await _repository.GetAllAsync();
            return Ok(typesOfMentalFunctions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TypesOfMentalFunctionsDTO>> GetById(int id)
        {
            var typeOfMentalFunction = await _repository.GetByIdAsync(id);
            if (typeOfMentalFunction == null)
                return NotFound();

            return Ok(typeOfMentalFunction);
        }

        [HttpPost]
        public async Task<ActionResult<TypesOfMentalFunctionsDTO>> Create(TypesOfMentalFunctionsDTO dto)
        {
            var createdType = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdType.TypeOfMentalFunctionId }, createdType);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TypesOfMentalFunctionsDTO>> Update(int id, TypesOfMentalFunctionsDTO dto)
        {
            if (id != dto.TypeOfMentalFunctionId)
                return BadRequest();

            var updatedType = await _repository.UpdateAsync(dto);
            if (updatedType == null)
                return NotFound();

            return Ok(updatedType);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _repository.DeleteAsync(id);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("search/{name}")]
        public async Task<ActionResult<List<TypesOfMentalFunctionsDTO>>> GetByName(string name)
        {
            var repository = _repository as TypesOfMentalFunctionsRepository;
            if (repository == null)
                return BadRequest("Repository type not supported");

            var types = await repository.GetByNameAsync(name);
            return Ok(types);
        }
    }
}