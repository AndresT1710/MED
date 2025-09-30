using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ResultTypeRepository : IRepository<ResultTypeDTO, int>
    {
        private readonly SGISContext _context;

        public ResultTypeRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ResultTypeDTO>> GetAllAsync()
        {
            var entities = await _context.ResultTypes.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<ResultTypeDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.ResultTypes.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<ResultTypeDTO> AddAsync(ResultTypeDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.ResultTypes.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<ResultTypeDTO?> UpdateAsync(ResultTypeDTO dto)
        {
            var entity = await _context.ResultTypes.FindAsync(dto.ResultTypeId);
            if (entity == null) return null;

            entity.Name = dto.Name ?? entity.Name;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ResultTypes.FindAsync(id);
            if (entity == null) return false;

            _context.ResultTypes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapping
        private ResultTypeDTO MapToDto(ResultType entity) => new ResultTypeDTO
        {
            ResultTypeId = entity.ResultTypeId,
            Name = entity.Name
        };

        private ResultType MapToEntity(ResultTypeDTO dto) => new ResultType
        {
            ResultTypeId = dto.ResultTypeId,
            Name = dto.Name ?? string.Empty
        };
    }
}
