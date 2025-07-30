using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Implementations;
using SMED.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet("by-person/{personId}")]
        public async Task<ActionResult<UserDTO>> GetByPersonId(int personId)
        {
            var user = await _userRepository.GetUserByPersonIdAsync(personId);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Create(UserDTO userDto)
        {
            var created = await _userRepository.AddAsync(userDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut]
        public async Task<ActionResult<UserDTO>> Update(UserDTO userDto)
        {
            var updated = await _userRepository.UpdateAsync(userDto);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userRepository.DeleteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
