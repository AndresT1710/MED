using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class SensitivityLevelRepository : IRepository<SensitivityLevelDTO, int>
    {
        private readonly SGISContext _context;

        public SensitivityLevelRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<SensitivityLevelDTO>> GetAllAsync()
        {
            var entities = await _context.SensitivityLevels.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<SensitivityLevelDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.SensitivityLevels.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<SensitivityLevelDTO> AddAsync(SensitivityLevelDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.SensitivityLevels.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<SensitivityLevelDTO?> UpdateAsync(SensitivityLevelDTO dto)
        {
            var entity = await _context.SensitivityLevels.FindAsync(dto.SensitivityLevelId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SensitivityLevels.FindAsync(id);
            if (entity == null) return false;

            _context.SensitivityLevels.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private SensitivityLevelDTO MapToDto(SensitivityLevel entity)
        {
            return new SensitivityLevelDTO
            {
                SensitivityLevelId = entity.SensitivityLevelId,
                Name = entity.Name
            };
        }

        private SensitivityLevel MapToEntity(SensitivityLevelDTO dto)
        {
            return new SensitivityLevel
            {
                SensitivityLevelId = dto.SensitivityLevelId,
                Name = dto.Name
            };
        }
    }
}
