using Infrastructure.Models;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class RelationshipRepository : IRepository<RelationshipDTO, int>
    {
        private readonly SGISContext _context;

        public RelationshipRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<RelationshipDTO>> GetAllAsync()
        {
            var list = await _context.Relationships.ToListAsync();
            return list.Select(MapToDto).ToList();
        }

        public async Task<RelationshipDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Relationships.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<RelationshipDTO> AddAsync(RelationshipDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Relationships.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<RelationshipDTO?> UpdateAsync(RelationshipDTO dto)
        {
            var entity = await _context.Relationships.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Relationships.FindAsync(id);
            if (entity == null) return false;

            _context.Relationships.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private RelationshipDTO MapToDto(Relationship entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name
        };

        private Relationship MapToEntity(RelationshipDTO dto) => new()
        {
            Id = dto.Id,
            Name = dto.Name
        };
    }
}