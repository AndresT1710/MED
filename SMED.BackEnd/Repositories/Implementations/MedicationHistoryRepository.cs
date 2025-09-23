using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class MedicationHistoryRepository : IRepository<MedicationHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public MedicationHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<MedicationHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.MedicationHistories
                .Include(mh => mh.Medicine)
                .Include(mh => mh.ClinicalHistory)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<MedicationHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.MedicationHistories
                .Include(mh => mh.Medicine)
                .Include(mh => mh.ClinicalHistory)
                .FirstOrDefaultAsync(mh => mh.MedicationHistoryId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<MedicationHistoryDTO> AddAsync(MedicationHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.MedicationHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<MedicationHistoryDTO?> UpdateAsync(MedicationHistoryDTO dto)
        {
            var entity = await _context.MedicationHistories.FindAsync(dto.MedicationHistoryId);
            if (entity == null) return null;

            entity.MedicineId = dto.MedicineId;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.ConsumptionDate = dto.ConsumptionDate;
            entity.HistoryNumber = dto.HistoryNumber;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MedicationHistories.FindAsync(id);
            if (entity == null) return false;

            _context.MedicationHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private MedicationHistoryDTO MapToDto(MedicationHistory entity)
        {
            return new MedicationHistoryDTO
            {
                MedicationHistoryId = entity.MedicationHistoryId,
                HistoryNumber = entity.HistoryNumber,
                MedicineId = entity.MedicineId,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                ConsumptionDate = entity.ConsumptionDate,
                MedicineName = entity.Medicine?.Name,
                MedicineWeight = entity.Medicine?.Weight
            };
        }

        private MedicationHistory MapToEntity(MedicationHistoryDTO dto)
        {
            return new MedicationHistory
            {
                MedicationHistoryId = dto.MedicationHistoryId,
                HistoryNumber = dto.HistoryNumber,
                MedicineId = dto.MedicineId,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                ConsumptionDate = dto.ConsumptionDate
            };
        }
    }
}