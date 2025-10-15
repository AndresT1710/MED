using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class GenderRepository : IRepository<GenderDTO, int>
    {
        private readonly SGISContext _context;

        public GenderRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<GenderDTO>> GetAllAsync()
        {
            var entities = await _context.Genders.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<GenderDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Genders.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<GenderDTO> AddAsync(GenderDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Genders.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<GenderDTO?> UpdateAsync(GenderDTO dto)
        {
            var entity = await _context.Genders.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Genders.FindAsync(id);
            if (entity == null) return false;

            _context.Genders.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private GenderDTO MapToDto(Gender entity)
        {
            return new GenderDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        private Gender MapToEntity(GenderDTO dto)
        {
            return new Gender
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}