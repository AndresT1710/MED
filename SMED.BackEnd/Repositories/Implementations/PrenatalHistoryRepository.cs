using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PrenatalHistoryRepository : IRepository<PrenatalHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public PrenatalHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PrenatalHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.PrenatalHistories
                .Include(p => p.ClinicalHistory)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<PrenatalHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.PrenatalHistories
                .Include(p => p.ClinicalHistory)
                .FirstOrDefaultAsync(p => p.PrenatalHistoryId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<PrenatalHistoryDTO> AddAsync(PrenatalHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.PrenatalHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<PrenatalHistoryDTO?> UpdateAsync(PrenatalHistoryDTO dto)
        {
            var entity = await _context.PrenatalHistories.FindAsync(dto.PrenatalHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.Description = dto.Description;
            entity.PlannedPregnancy = dto.PlannedPregnancy;
            entity.MedicationsOrVitamins = dto.MedicationsOrVitamins;
            entity.RadiationExposure = dto.RadiationExposure;
            entity.NumberOfControls = dto.NumberOfControls;
            entity.NumberOfUltrasounds = dto.NumberOfUltrasounds;
            entity.FetalSuffering = dto.FetalSuffering;
            entity.ComplicationsDuringPregnancy = dto.ComplicationsDuringPregnancy;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PrenatalHistories.FindAsync(id);
            if (entity == null) return false;
            _context.PrenatalHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private PrenatalHistoryDTO MapToDto(PrenatalHistory entity)
        {
            return new PrenatalHistoryDTO
            {
                PrenatalHistoryId = entity.PrenatalHistoryId,
                HistoryNumber = entity.HistoryNumber,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                Description = entity.Description,
                PlannedPregnancy = entity.PlannedPregnancy,
                MedicationsOrVitamins = entity.MedicationsOrVitamins,
                RadiationExposure = entity.RadiationExposure,
                NumberOfControls = entity.NumberOfControls,
                NumberOfUltrasounds = entity.NumberOfUltrasounds,
                FetalSuffering = entity.FetalSuffering,
                ComplicationsDuringPregnancy = entity.ComplicationsDuringPregnancy
            };
        }

        private PrenatalHistory MapToEntity(PrenatalHistoryDTO dto)
        {
            return new PrenatalHistory
            {
                PrenatalHistoryId = dto.PrenatalHistoryId,
                HistoryNumber = dto.HistoryNumber,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                Description = dto.Description,
                PlannedPregnancy = dto.PlannedPregnancy,
                MedicationsOrVitamins = dto.MedicationsOrVitamins,
                RadiationExposure = dto.RadiationExposure,
                NumberOfControls = dto.NumberOfControls,
                NumberOfUltrasounds = dto.NumberOfUltrasounds,
                FetalSuffering = dto.FetalSuffering,
                ComplicationsDuringPregnancy = dto.ComplicationsDuringPregnancy
            };
        }
    }
}