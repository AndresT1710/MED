using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class SpecialTestRepository : IRepository<SpecialTestDTO, int>
    {
        private readonly SGISContext _context;

        public SpecialTestRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<SpecialTestDTO>> GetAllAsync()
        {
            var entities = await _context.SpecialTests.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<SpecialTestDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.SpecialTests.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<SpecialTestDTO> AddAsync(SpecialTestDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.SpecialTests.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<SpecialTestDTO?> UpdateAsync(SpecialTestDTO dto)
        {
            var entity = await _context.SpecialTests.FindAsync(dto.SpecialTestId);
            if (entity == null) return null;

            entity.Test = dto.Test;
            entity.Observations = dto.Observations;
            entity.ResultTypeId = dto.ResultTypeId;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SpecialTests.FindAsync(id);
            if (entity == null) return false;

            _context.SpecialTests.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapping
        private SpecialTestDTO MapToDto(SpecialTest entity) => new SpecialTestDTO
        {
            SpecialTestId = entity.SpecialTestId,
            Test = entity.Test,
            Observations = entity.Observations,
            ResultTypeId = entity.ResultTypeId,
            MedicalCareId = entity.MedicalCareId
        };

        private SpecialTest MapToEntity(SpecialTestDTO dto) => new SpecialTest
        {
            SpecialTestId = dto.SpecialTestId,
            Test = dto.Test ?? string.Empty,
            Observations = dto.Observations ?? string.Empty,
            ResultTypeId = dto.ResultTypeId,
            MedicalCareId = dto.MedicalCareId
        };
    }
}
