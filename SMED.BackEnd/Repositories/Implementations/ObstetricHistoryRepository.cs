using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ObstetricHistoryRepository : IRepository<ObstetricHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public ObstetricHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ObstetricHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.ObstetricHistories.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<ObstetricHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.ObstetricHistories.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<ObstetricHistoryDTO> AddAsync(ObstetricHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.ObstetricHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<ObstetricHistoryDTO?> UpdateAsync(ObstetricHistoryDTO dto)
        {
            var entity = await _context.ObstetricHistories.FindAsync(dto.ObstetricHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.CurrentPregnancy = dto.CurrentPregnancy;
            entity.PreviousPregnancies = dto.PreviousPregnancies;
            entity.Deliveries = dto.Deliveries;
            entity.Abortions = dto.Abortions;
            entity.CSections = dto.CSections;
            entity.LiveBirths = dto.LiveBirths;
            entity.Stillbirths = dto.Stillbirths;
            entity.LivingChildren = dto.LivingChildren;
            entity.Breastfeeding = dto.Breastfeeding;
            entity.DiseaseId = dto.DiseaseId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ObstetricHistories.FindAsync(id);
            if (entity == null) return false;

            _context.ObstetricHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapeo manual
        private ObstetricHistoryDTO MapToDto(ObstetricHistory entity)
        {
            return new ObstetricHistoryDTO
            {
                ObstetricHistoryId = entity.ObstetricHistoryId,
                HistoryNumber = entity.HistoryNumber,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                CurrentPregnancy = entity.CurrentPregnancy,
                PreviousPregnancies = entity.PreviousPregnancies,
                Deliveries = entity.Deliveries,
                Abortions = entity.Abortions,
                CSections = entity.CSections,
                LiveBirths = entity.LiveBirths,
                Stillbirths = entity.Stillbirths,
                LivingChildren = entity.LivingChildren,
                Breastfeeding = entity.Breastfeeding,
                DiseaseId = entity.DiseaseId
            };
        }

        private ObstetricHistory MapToEntity(ObstetricHistoryDTO dto)
        {
            return new ObstetricHistory
            {
                ObstetricHistoryId = dto.ObstetricHistoryId,
                HistoryNumber = dto.HistoryNumber,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                CurrentPregnancy = dto.CurrentPregnancy,
                PreviousPregnancies = dto.PreviousPregnancies,
                Deliveries = dto.Deliveries,
                Abortions = dto.Abortions,
                CSections = dto.CSections,
                LiveBirths = dto.LiveBirths,
                Stillbirths = dto.Stillbirths,
                LivingChildren = dto.LivingChildren,
                Breastfeeding = dto.Breastfeeding,
                DiseaseId = dto.DiseaseId
            };
        }
    }
}
