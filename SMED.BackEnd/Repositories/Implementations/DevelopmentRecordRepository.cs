using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class DevelopmentRecordRepository : IRepository<DevelopmentRecordDTO, int>
    {
        private readonly SGISContext _context;

        public DevelopmentRecordRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<DevelopmentRecordDTO>> GetAllAsync()
        {
            var entities = await _context.DevelopmentRecords
                .Include(dr => dr.ClinicalHistory)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<DevelopmentRecordDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.DevelopmentRecords
                .Include(dr => dr.ClinicalHistory)
                .FirstOrDefaultAsync(dr => dr.DevelopmentRecordId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<DevelopmentRecordDTO> AddAsync(DevelopmentRecordDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.DevelopmentRecords.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<DevelopmentRecordDTO?> UpdateAsync(DevelopmentRecordDTO dto)
        {
            var entity = await _context.DevelopmentRecords.FindAsync(dto.DevelopmentRecordId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.DevelopmentMilestone = dto.DevelopmentMilestone;
            entity.AgeRange = dto.AgeRange;
            entity.Observations = dto.Observations;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.DevelopmentRecords.FindAsync(id);
            if (entity == null) return false;
            _context.DevelopmentRecords.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private DevelopmentRecordDTO MapToDto(DevelopmentRecord entity)
        {
            return new DevelopmentRecordDTO
            {
                DevelopmentRecordId = entity.DevelopmentRecordId,
                HistoryNumber = entity.HistoryNumber,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                DevelopmentMilestone = entity.DevelopmentMilestone,
                AgeRange = entity.AgeRange,
                Observations = entity.Observations
            };
        }

        private DevelopmentRecord MapToEntity(DevelopmentRecordDTO dto)
        {
            return new DevelopmentRecord
            {
                DevelopmentRecordId = dto.DevelopmentRecordId,
                HistoryNumber = dto.HistoryNumber,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                DevelopmentMilestone = dto.DevelopmentMilestone,
                AgeRange = dto.AgeRange,
                Observations = dto.Observations
            };
        }
    }
}