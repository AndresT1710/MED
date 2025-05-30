using Infrastructure.Models;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class AllergyRepository : IRepository<AllergyDTO, int>
    {
        private readonly SGISContext _context;

        public AllergyRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<AllergyDTO> AddAsync(AllergyDTO dto)
        {
            var entity = new Allergy
            {
                Name = dto.Name
            };
            _context.Allergies.Add(entity);
            await _context.SaveChangesAsync();
            dto.AllergyId = entity.AllergyId;
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Allergies.FindAsync(id);
            if (entity == null) return false;
            _context.Allergies.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<AllergyDTO>> GetAllAsync()
        {
            return await _context.Allergies
                .Select(a => new AllergyDTO
                {
                    AllergyId = a.AllergyId,
                    Name = a.Name
                })
                .ToListAsync();
        }

        public async Task<AllergyDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Allergies.FindAsync(id);
            return entity == null ? null : new AllergyDTO
            {
                AllergyId = entity.AllergyId,
                Name = entity.Name
            };
        }

        public async Task<AllergyDTO?> UpdateAsync(AllergyDTO dto)
        {
            var entity = await _context.Allergies.FindAsync(dto.AllergyId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();

            return dto;
        }
    }
}
