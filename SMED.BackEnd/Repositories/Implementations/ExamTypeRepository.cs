using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ExamTypeRepository : IRepository<ExamTypeDTO, int>
    {
        private readonly SGISContext _context;

        public ExamTypeRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ExamTypeDTO>> GetAllAsync()
        {
            var types = await _context.ExamTypes.ToListAsync();
            return types.Select(MapToDto).ToList();
        }

        public async Task<ExamTypeDTO?> GetByIdAsync(int id)
        {
            var type = await _context.ExamTypes.FindAsync(id);
            return type == null ? null : MapToDto(type);
        }

        public async Task<ExamTypeDTO> AddAsync(ExamTypeDTO dto)
        {
            var entity = new ExamType
            {
                Name = dto.Name
            };

            _context.ExamTypes.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<ExamTypeDTO?> UpdateAsync(ExamTypeDTO dto)
        {
            var entity = await _context.ExamTypes.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ExamTypes.FindAsync(id);
            if (entity == null) return false;

            _context.ExamTypes.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static ExamTypeDTO MapToDto(ExamType type) => new ExamTypeDTO
        {
            Id = type.Id,
            Name = type.Name
        };
    }

}
