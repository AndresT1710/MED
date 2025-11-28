using SGIS.Models;
using SMED.BackEnd.Services.Interface;

namespace SMED.BackEnd.Services.Implementations
{
    public class PasswordMigrationService
    {
        private readonly SGISContext _context;
        private readonly IPasswordService _passwordService;

        public PasswordMigrationService(SGISContext context)
        {
            _context = context;
            _passwordService = new PasswordService();
        }

        public async Task MigratePasswords()
        {
            var users = _context.Users.Where(u => !u.Password.StartsWith("$2")).ToList(); // BCrypt hashes start with $2

            foreach (var user in users)
            {
                if (!string.IsNullOrEmpty(user.Password))
                {
                    user.Password = _passwordService.HashPassword(user.Password);
                }
            }

            await _context.SaveChangesAsync();
            Console.WriteLine($"Migrated {users.Count} passwords to BCrypt");
        }
    }
}
