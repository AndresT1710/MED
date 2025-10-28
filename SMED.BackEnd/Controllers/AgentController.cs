using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentController : ControllerBase
    {
        private readonly IRepository<AgentDTO, int> _repository;

        public AgentController(IRepository<AgentDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgentDTO>>> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<AgentDTO>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AgentDTO>> Create(AgentDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.AgentId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AgentDTO>> Update(int id, AgentDTO dto)
        {
            if (id != dto.AgentId) return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return !deleted ? NotFound() : NoContent();
        }

        [HttpGet("ByPatient/{patientId}")]
        public async Task<ActionResult<IEnumerable<AgentDTO>>> GetByPatientId(int patientId)
        {
            if (_repository is AgentRepository repo)
            {
                var result = await repo.GetByPatientIdAsync(patientId);
                return Ok(result);
            }

            return BadRequest("Repositorio no soporta esta operación");
        }

        // NUEVO ENDPOINT: Asignar agente a paciente
        [HttpPut("AssignToPatient")]
        public async Task<IActionResult> AssignAgentToPatient([FromBody] AssignAgentRequest request)
        {
            try
            {
                if (_repository is AgentRepository repo)
                {
                    var result = await repo.AssignAgentToPatientAsync(request.AgentId, request.PatientId);
                    return result ? Ok(new { Message = "Agente asignado correctamente al paciente" })
                                 : NotFound("No se pudo asignar el agente al paciente");
                }
                return BadRequest("Repositorio no soporta esta operación");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error asignando agente: {ex.Message}");
            }
        }


        // NUEVO ENDPOINT: Desvincular agente de paciente
        [HttpPut("UnassignFromPatient")]
        public async Task<IActionResult> UnassignAgentFromPatient([FromBody] AssignAgentRequest request)
        {
            try
            {
                if (_repository is AgentRepository repo)
                {
                    var result = await repo.UnassignAgentFromPatientAsync(request.PatientId, request.AgentId);
                    return result ? Ok(new { Message = "Agente desvinculado correctamente del paciente" })
                                 : NotFound("No se pudo desvincular el agente del paciente");
                }
                return BadRequest("Repositorio no soporta esta operación");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error desvinculando agente: {ex.Message}");
            }
        }



    }

    // DTO para la request de asignación
    public class AssignAgentRequest
    {
        public int PatientId { get; set; }
        public int AgentId { get; set; }
    }
}