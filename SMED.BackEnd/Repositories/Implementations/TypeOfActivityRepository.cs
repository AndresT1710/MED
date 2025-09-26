using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class TypeOfActivityRepository : IRepository<TypeOfActivityDTO, int>
    {
        private readonly SGISContext _context;

        public TypeOfActivityRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<TypeOfActivityDTO>> GetAllAsync()
        {
            var entities = await _context.TypeOfActivities.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<TypeOfActivityDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.TypeOfActivities.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<TypeOfActivityDTO> AddAsync(TypeOfActivityDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.TypeOfActivities.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<TypeOfActivityDTO?> UpdateAsync(TypeOfActivityDTO dto)
        {
            var entity = await _context.TypeOfActivities.FindAsync(dto.TypeOfActivityId);
            if (entity == null) return null;

            entity.Name = dto.Name;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TypeOfActivities.FindAsync(id);
            if (entity == null) return false;

            _context.TypeOfActivities.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private TypeOfActivityDTO MapToDto(TypeOfActivity entity)
        {
            return new TypeOfActivityDTO
            {
                TypeOfActivityId = entity.TypeOfActivityId,
                Name = entity.Name
            };
        }

        private TypeOfActivity MapToEntity(TypeOfActivityDTO dto)
        {
            return new TypeOfActivity
            {
                TypeOfActivityId = dto.TypeOfActivityId,
                Name = dto.Name
            };
        }
    }
}
