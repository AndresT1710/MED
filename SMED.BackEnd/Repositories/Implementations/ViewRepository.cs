using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ViewRepository : IRepository<ViewDTO, int>
    {
        private readonly SGISContext _context;

        public ViewRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ViewDTO>> GetAllAsync()
        {
            var entities = await _context.Views.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<ViewDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Views.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<ViewDTO> AddAsync(ViewDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Views.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<ViewDTO?> UpdateAsync(ViewDTO dto)
        {
            var entity = await _context.Views.FindAsync(dto.ViewId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Views.FindAsync(id);
            if (entity == null) return false;

            _context.Views.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapping
        private ViewDTO MapToDto(View entity) => new ViewDTO
        {
            ViewId = entity.ViewId,
            Name = entity.Name
        };

        private View MapToEntity(ViewDTO dto) => new View
        {
            ViewId = dto.ViewId,
            Name = dto.Name
        };
    }
}
