using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class HospitalizationsHistoryRepository : IRepository<HospitalizationsHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public HospitalizationsHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<HospitalizationsHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.HospitalizationsHistories
                .Include(h => h.ClinicalHistory)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<HospitalizationsHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.HospitalizationsHistories
                .Include(h => h.ClinicalHistory)
                .FirstOrDefaultAsync(h => h.HospitalizationsHistoryId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<HospitalizationsHistoryDTO> AddAsync(HospitalizationsHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.HospitalizationsHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<HospitalizationsHistoryDTO?> UpdateAsync(HospitalizationsHistoryDTO dto)
        {
            var entity = await _context.HospitalizationsHistories.FindAsync(dto.HospitalizationsHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.HospitalizationReason = dto.HospitalizationReason;
            entity.HospitalizationDate = dto.HospitalizationDate;
            entity.HospitalizationPlace = dto.HospitalizationPlace;
            entity.Observations = dto.Observations;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.HospitalizationsHistories.FindAsync(id);
            if (entity == null) return false;
            _context.HospitalizationsHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private HospitalizationsHistoryDTO MapToDto(HospitalizationsHistory entity)
        {
            return new HospitalizationsHistoryDTO
            {
                HospitalizationsHistoryId = entity.HospitalizationsHistoryId,
                HistoryNumber = entity.HistoryNumber,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                HospitalizationReason = entity.HospitalizationReason,
                HospitalizationDate = entity.HospitalizationDate,
                HospitalizationPlace = entity.HospitalizationPlace,
                Observations = entity.Observations
            };
        }

        private HospitalizationsHistory MapToEntity(HospitalizationsHistoryDTO dto)
        {
            return new HospitalizationsHistory
            {
                HospitalizationsHistoryId = dto.HospitalizationsHistoryId,
                HistoryNumber = dto.HistoryNumber,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                HospitalizationReason = dto.HospitalizationReason,
                HospitalizationDate = dto.HospitalizationDate,
                HospitalizationPlace = dto.HospitalizationPlace,
                Observations = dto.Observations
            };
        }
    }
}