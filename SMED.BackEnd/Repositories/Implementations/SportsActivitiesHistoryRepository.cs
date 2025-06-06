using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class SportsActivitiesHistoryRepository : IRepository<SportsActivitiesHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public SportsActivitiesHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<SportsActivitiesHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.SportsActivitiesHistories.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<SportsActivitiesHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.SportsActivitiesHistories.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<SportsActivitiesHistoryDTO> AddAsync(SportsActivitiesHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.SportsActivitiesHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<SportsActivitiesHistoryDTO?> UpdateAsync(SportsActivitiesHistoryDTO dto)
        {
            var entity = await _context.SportsActivitiesHistories.FindAsync(dto.SportActivityHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.Description = dto.Description;
            entity.RegistrationDate = dto.RegistrationDate;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.SportActivityId = dto.SportActivityId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SportsActivitiesHistories.FindAsync(id);
            if (entity == null) return false;

            _context.SportsActivitiesHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private SportsActivitiesHistoryDTO MapToDto(SportsActivitiesHistory entity)
        {
            return new SportsActivitiesHistoryDTO
            {
                SportActivityHistoryId = entity.SportActivityHistoryId,
                HistoryNumber = entity.HistoryNumber,
                Description = entity.Description,
                RegistrationDate = entity.RegistrationDate,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                SportActivityId = entity.SportActivityId
            };
        }

        private SportsActivitiesHistory MapToEntity(SportsActivitiesHistoryDTO dto)
        {
            return new SportsActivitiesHistory
            {
                SportActivityHistoryId = dto.SportActivityHistoryId,
                HistoryNumber = dto.HistoryNumber,
                Description = dto.Description,
                RegistrationDate = dto.RegistrationDate,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                SportActivityId = dto.SportActivityId
            };
        }
    }
}
