using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Services.Interface;
using SMED.Shared.DTOs;
using System.Threading.Tasks;

namespace SMED.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var response = await _authService.LoginAsync(loginRequest);
            if (!response.IsAuthenticated)
                return Unauthorized(response);

            return Ok(response);
        }
    }
}
