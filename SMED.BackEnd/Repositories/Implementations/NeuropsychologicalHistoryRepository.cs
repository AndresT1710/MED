using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class NeuropsychologicalHistoryRepository : IRepository<NeuropsychologicalHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public NeuropsychologicalHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<NeuropsychologicalHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.NeuropsychologicalHistories
                .Include(n => n.ClinicalHistory)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<NeuropsychologicalHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.NeuropsychologicalHistories
                .Include(n => n.ClinicalHistory)
                .FirstOrDefaultAsync(n => n.NeuropsychologicalHistoryId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<NeuropsychologicalHistoryDTO> AddAsync(NeuropsychologicalHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.NeuropsychologicalHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<NeuropsychologicalHistoryDTO?> UpdateAsync(NeuropsychologicalHistoryDTO dto)
        {
            var entity = await _context.NeuropsychologicalHistories.FindAsync(dto.NeuropsychologicalHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.Description = dto.Description;
            entity.HomeConduct = dto.HomeConduct;
            entity.SchoolConduct = dto.SchoolConduct;
            entity.Leverage = dto.Leverage;
            entity.HearingObservation = dto.HearingObservation;
            entity.SightObservation = dto.SightObservation;
            entity.SpeechObservation = dto.SpeechObservation;
            entity.DreamObservation = dto.DreamObservation;
            entity.Faintings = dto.Faintings;
            entity.ObservationDifferentAbility = dto.ObservationDifferentAbility;
            entity.Observation = dto.Observation;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.NeuropsychologicalHistories.FindAsync(id);
            if (entity == null) return false;
            _context.NeuropsychologicalHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private NeuropsychologicalHistoryDTO MapToDto(NeuropsychologicalHistory entity)
        {
            return new NeuropsychologicalHistoryDTO
            {
                NeuropsychologicalHistoryId = entity.NeuropsychologicalHistoryId,
                HistoryNumber = entity.HistoryNumber,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                Description = entity.Description,
                HomeConduct = entity.HomeConduct,
                SchoolConduct = entity.SchoolConduct,
                Leverage = entity.Leverage,
                HearingObservation = entity.HearingObservation,
                SightObservation = entity.SightObservation,
                SpeechObservation = entity.SpeechObservation,
                DreamObservation = entity.DreamObservation,
                Faintings = entity.Faintings,
                ObservationDifferentAbility = entity.ObservationDifferentAbility,
                Observation = entity.Observation
            };
        }

        private NeuropsychologicalHistory MapToEntity(NeuropsychologicalHistoryDTO dto)
        {
            return new NeuropsychologicalHistory
            {
                NeuropsychologicalHistoryId = dto.NeuropsychologicalHistoryId,
                HistoryNumber = dto.HistoryNumber,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                Description = dto.Description,
                HomeConduct = dto.HomeConduct,
                SchoolConduct = dto.SchoolConduct,
                Leverage = dto.Leverage,
                HearingObservation = dto.HearingObservation,
                SightObservation = dto.SightObservation,
                SpeechObservation = dto.SpeechObservation,
                DreamObservation = dto.DreamObservation,
                Faintings = dto.Faintings,
                ObservationDifferentAbility = dto.ObservationDifferentAbility,
                Observation = dto.Observation
            };
        }
    }
}