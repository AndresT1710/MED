using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class AdditionalDataRepository : IRepository<AdditionalDataDTO, int>
    {
        private readonly SGISContext _context;

        public AdditionalDataRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<AdditionalDataDTO>> GetAllAsync()
        {
            var entities = await _context.AdditionalData.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<AdditionalDataDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.AdditionalData.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<AdditionalDataDTO> AddAsync(AdditionalDataDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.AdditionalData.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<AdditionalDataDTO?> UpdateAsync(AdditionalDataDTO dto)
        {
            var entity = await _context.AdditionalData.FindAsync(dto.AdditionalDataId);
            if (entity == null) return null;

            entity.Observacion = dto.Observacion;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.AdditionalData.FindAsync(id);
            if (entity == null) return false;

            _context.AdditionalData.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private AdditionalDataDTO MapToDto(AdditionalData entity)
        {
            return new AdditionalDataDTO
            {
                AdditionalDataId = entity.AdditionalDataId,
                Observacion = entity.Observacion,
                MedicalCareId = entity.MedicalCareId
            };
        }

        private AdditionalData MapToEntity(AdditionalDataDTO dto)
        {
            return new AdditionalData
            {
                AdditionalDataId = dto.AdditionalDataId,
                Observacion = dto.Observacion,
                MedicalCareId = dto.MedicalCareId
            };
        }
    }

}
