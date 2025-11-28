using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.BackEnd.Services.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class UserRepository : IRepository<UserDTO, int>
    {
        private readonly SGISContext _context;
        private readonly IPasswordService _passwordService;

        public UserRepository(SGISContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var users = await _context.Users
                .Include(u => u.PersonNavigation)
                    .ThenInclude(p => p.HealthProfessional)
                        .ThenInclude(hp => hp.HealthProfessionalTypeNavigation)
                .ToListAsync();
            return users.Select(u => MapToDTO(u)).ToList();
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.PersonNavigation)
                    .ThenInclude(p => p.HealthProfessional)
                        .ThenInclude(hp => hp.HealthProfessionalTypeNavigation)
                .FirstOrDefaultAsync(u => u.Id == id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<UserDTO?> GetUserByPersonIdAsync(int personId)
        {
            var user = await _context.Users
                .Include(u => u.PersonNavigation)
                .FirstOrDefaultAsync(u => u.PersonId == personId);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<UserDTO> AddAsync(UserDTO dto)
        {
            var hashedPassword = _passwordService.HashPassword(dto.Password);

            var user = new User
            {
                PersonId = dto.PersonId,
                Password = hashedPassword,
                IsActive = dto.IsActive
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            dto.Id = user.Id;
            dto.Password = null;
            return dto;
        }

        public async Task<UserDTO?> UpdateAsync(UserDTO dto)
        {
            var user = await _context.Users
                .Include(u => u.PersonNavigation)
                .FirstOrDefaultAsync(u => u.Id == dto.Id);
            if (user == null) return null;

            user.PersonId = dto.PersonId;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                user.Password = _passwordService.HashPassword(dto.Password);
            }

            user.IsActive = dto.IsActive;
            await _context.SaveChangesAsync();

            var result = MapToDTO(user);
            result.Password = null;
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        private static UserDTO MapToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                PersonId = user.PersonId,
                Password = null,
                IsActive = user.IsActive,
                Email = user.PersonNavigation?.Email,
                Name = user.PersonNavigation != null ? $"{user.PersonNavigation.FirstName} {user.PersonNavigation.LastName}" : null,
                HealthProfessionalTypeName = user.PersonNavigation?.HealthProfessional?.HealthProfessionalTypeNavigation?.Name
            };
        }
    }
}
