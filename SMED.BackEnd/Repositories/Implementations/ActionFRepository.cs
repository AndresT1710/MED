using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ActionFRepository : IRepository<ActionFDTO, int>
    {
        private readonly SGISContext _context;

        public ActionFRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ActionFDTO>> GetAllAsync()
        {
            var entities = await _context.ActionFs.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<ActionFDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.ActionFs.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<ActionFDTO> AddAsync(ActionFDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.ActionFs.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<ActionFDTO?> UpdateAsync(ActionFDTO dto)
        {
            var entity = await _context.ActionFs.FindAsync(dto.ActionId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ActionFs.FindAsync(id);
            if (entity == null) return false;

            _context.ActionFs.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapping
        private ActionFDTO MapToDto(ActionF entity) => new ActionFDTO
        {
            ActionId = entity.ActionId,
            Name = entity.Name
        };

        private ActionF MapToEntity(ActionFDTO dto) => new ActionF
        {
            ActionId = dto.ActionId,
            Name = dto.Name
        };
    }
}
