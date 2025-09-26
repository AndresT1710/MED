using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class NeurologicalExamTypeRepository : IRepository<NeurologicalExamTypeDTO, int>
    {
        private readonly SGISContext _context;

        public NeurologicalExamTypeRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<NeurologicalExamTypeDTO>> GetAllAsync()
        {
            var entities = await _context.NeurologicalExamTypes.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<NeurologicalExamTypeDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.NeurologicalExamTypes.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<NeurologicalExamTypeDTO> AddAsync(NeurologicalExamTypeDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.NeurologicalExamTypes.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<NeurologicalExamTypeDTO?> UpdateAsync(NeurologicalExamTypeDTO dto)
        {
            var entity = await _context.NeurologicalExamTypes.FindAsync(dto.NeurologicalExamTypeId);
            if (entity == null) return null;

            entity.Name = dto.Name;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.NeurologicalExamTypes.FindAsync(id);
            if (entity == null) return false;
            _context.NeurologicalExamTypes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private NeurologicalExamTypeDTO MapToDto(NeurologicalExamType entity)
        {
            return new NeurologicalExamTypeDTO
            {
                NeurologicalExamTypeId = entity.NeurologicalExamTypeId,
                Name = entity.Name
            };
        }

        private NeurologicalExamType MapToEntity(NeurologicalExamTypeDTO dto)
        {
            return new NeurologicalExamType
            {
                NeurologicalExamTypeId = dto.NeurologicalExamTypeId,
                Name = dto.Name
            };
        }
    }
}