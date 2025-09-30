using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SMED.BackEnd.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PainScaleRepository : IRepository<PainScaleDTO, int>
    {
        private readonly SGISContext _context;

        public PainScaleRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PainScaleDTO>> GetAllAsync()
        {
            var entities = await _context.PainScales.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<PainScaleDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.PainScales.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<PainScaleDTO> AddAsync(PainScaleDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.PainScales.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<PainScaleDTO?> UpdateAsync(PainScaleDTO dto)
        {
            var entity = await _context.PainScales.FindAsync(dto.PainScaleId);
            if (entity == null) return null;

            entity.Observation = dto.Observation ?? entity.Observation;
            entity.ActionId = dto.ActionId;
            entity.ScaleId = dto.ScaleId;
            entity.PainMomentId = dto.PainMomentId;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PainScales.FindAsync(id);
            if (entity == null) return false;

            _context.PainScales.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapping
        private PainScaleDTO MapToDto(PainScale entity) => new PainScaleDTO
        {
            PainScaleId = entity.PainScaleId,
            Observation = entity.Observation,
            ActionId = entity.ActionId,
            ScaleId = entity.ScaleId,
            PainMomentId = entity.PainMomentId,
            MedicalCareId = entity.MedicalCareId
        };

        private PainScale MapToEntity(PainScaleDTO dto) => new PainScale
        {
            PainScaleId = dto.PainScaleId,
            Observation = dto.Observation ?? string.Empty,
            ActionId = dto.ActionId,
            ScaleId = dto.ScaleId,
            PainMomentId = dto.PainMomentId,
            MedicalCareId = dto.MedicalCareId
        };
    }
}
