using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PhysicalExamTypeRepository : IRepository<PhysicalExamTypeDTO, int>
    {
        private readonly SGISContext _context;

        public PhysicalExamTypeRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PhysicalExamTypeDTO>> GetAllAsync()
        {
            var types = await _context.PhysicalExamTypes.ToListAsync();
            return types.Select(MapToDto).ToList();
        }

        public async Task<PhysicalExamTypeDTO?> GetByIdAsync(int id)
        {
            var type = await _context.PhysicalExamTypes.FindAsync(id);
            return type == null ? null : MapToDto(type);
        }

        public async Task<PhysicalExamTypeDTO> AddAsync(PhysicalExamTypeDTO dto)
        {
            var entity = new PhysicalExamType
            {
                Name = dto.Name
            };

            _context.PhysicalExamTypes.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<PhysicalExamTypeDTO?> UpdateAsync(PhysicalExamTypeDTO dto)
        {
            var entity = await _context.PhysicalExamTypes.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PhysicalExamTypes.FindAsync(id);
            if (entity == null) return false;

            _context.PhysicalExamTypes.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static PhysicalExamTypeDTO MapToDto(PhysicalExamType type) => new PhysicalExamTypeDTO
        {
            Id = type.Id,
            Name = type.Name
        };
    }

}
