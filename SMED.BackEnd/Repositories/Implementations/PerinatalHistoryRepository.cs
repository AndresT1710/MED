using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PerinatalHistoryRepository : IRepository<PerinatalHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public PerinatalHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PerinatalHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.PerinatalHistories
                .Include(p => p.ClinicalHistory)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<PerinatalHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.PerinatalHistories
                .Include(p => p.ClinicalHistory)
                .FirstOrDefaultAsync(p => p.PerinatalHistoryId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<PerinatalHistoryDTO> AddAsync(PerinatalHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.PerinatalHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<PerinatalHistoryDTO?> UpdateAsync(PerinatalHistoryDTO dto)
        {
            var entity = await _context.PerinatalHistories.FindAsync(dto.PerinatalHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.Description = dto.Description;
            entity.TypeOfBirth = dto.TypeOfBirth;
            entity.Apgar = dto.Apgar;
            entity.AuditoryScreen = dto.AuditoryScreen;
            entity.ResuscitationManeuvers = dto.ResuscitationManeuvers;
            entity.PlaceOfCare = dto.PlaceOfCare;
            entity.NumberOfWeeks = dto.NumberOfWeeks;
            entity.BirthCry = dto.BirthCry;
            entity.MetabolicScreen = dto.MetabolicScreen;
            entity.ComplicationsDuringChildbirth = dto.ComplicationsDuringChildbirth;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PerinatalHistories.FindAsync(id);
            if (entity == null) return false;
            _context.PerinatalHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private PerinatalHistoryDTO MapToDto(PerinatalHistory entity)
        {
            return new PerinatalHistoryDTO
            {
                PerinatalHistoryId = entity.PerinatalHistoryId,
                HistoryNumber = entity.HistoryNumber,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                Description = entity.Description,
                TypeOfBirth = entity.TypeOfBirth,
                Apgar = entity.Apgar,
                AuditoryScreen = entity.AuditoryScreen,
                ResuscitationManeuvers = entity.ResuscitationManeuvers,
                PlaceOfCare = entity.PlaceOfCare,
                NumberOfWeeks = entity.NumberOfWeeks,
                BirthCry = entity.BirthCry,
                MetabolicScreen = entity.MetabolicScreen,
                ComplicationsDuringChildbirth = entity.ComplicationsDuringChildbirth
            };
        }

        private PerinatalHistory MapToEntity(PerinatalHistoryDTO dto)
        {
            return new PerinatalHistory
            {
                PerinatalHistoryId = dto.PerinatalHistoryId,
                HistoryNumber = dto.HistoryNumber,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                Description = dto.Description,
                TypeOfBirth = dto.TypeOfBirth,
                Apgar = dto.Apgar,
                AuditoryScreen = dto.AuditoryScreen,
                ResuscitationManeuvers = dto.ResuscitationManeuvers,
                PlaceOfCare = dto.PlaceOfCare,
                NumberOfWeeks = dto.NumberOfWeeks,
                BirthCry = dto.BirthCry,
                MetabolicScreen = dto.MetabolicScreen,
                ComplicationsDuringChildbirth = dto.ComplicationsDuringChildbirth
            };
        }
    }
}