using Microsoft.AspNetCore.Mvc;
using SGIS.Models;
using SMED.BackEnd.Services.Implementations;

namespace SMED.BackEnd.Controllers
{
    // Temporalmente en un controller
    [ApiController]
    [Route("api/[controller]")]
    public class MigrationController : ControllerBase
    {
        private readonly SGISContext _context;

        public MigrationController(SGISContext context)
        {
            _context = context;
        }

        [HttpPost("migrate-passwords")]
        public async Task<IActionResult> MigratePasswords()
        {
            var migrationService = new PasswordMigrationService(_context);
            await migrationService.MigratePasswords();
            return Ok("Passwords migrated successfully");
        }
    }
}
