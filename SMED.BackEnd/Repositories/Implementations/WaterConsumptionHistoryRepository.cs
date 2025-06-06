using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class WaterConsumptionHistoryRepository : IRepository<WaterConsumptionHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public WaterConsumptionHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<WaterConsumptionHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.WaterConsumptionHistories.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<WaterConsumptionHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.WaterConsumptionHistories.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<WaterConsumptionHistoryDTO> AddAsync(WaterConsumptionHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.WaterConsumptionHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<WaterConsumptionHistoryDTO?> UpdateAsync(WaterConsumptionHistoryDTO dto)
        {
            var entity = await _context.WaterConsumptionHistories.FindAsync(dto.WaterConsumptionHistoryId);
            if (entity == null) return null;

            entity.MedicalRecordNumber = dto.MedicalRecordNumber;
            entity.Amount = dto.Amount;
            entity.Frequency = dto.Frequency;
            entity.RegistrationDate = dto.RegistrationDate;
            entity.Description = dto.Description;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.WaterConsumptionHistories.FindAsync(id);
            if (entity == null) return false;

            _context.WaterConsumptionHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private WaterConsumptionHistoryDTO MapToDto(WaterConsumptionHistory entity)
        {
            return new WaterConsumptionHistoryDTO
            {
                WaterConsumptionHistoryId = entity.WaterConsumptionHistoryId,
                MedicalRecordNumber = entity.MedicalRecordNumber,
                Amount = entity.Amount,
                Frequency = entity.Frequency,
                RegistrationDate = entity.RegistrationDate,
                Description = entity.Description,
                ClinicalHistoryId = entity.ClinicalHistoryId
            };
        }

        private WaterConsumptionHistory MapToEntity(WaterConsumptionHistoryDTO dto)
        {
            return new WaterConsumptionHistory
            {
                WaterConsumptionHistoryId = dto.WaterConsumptionHistoryId,
                MedicalRecordNumber = dto.MedicalRecordNumber,
                Amount = dto.Amount,
                Frequency = dto.Frequency,
                RegistrationDate = dto.RegistrationDate,
                Description = dto.Description,
                ClinicalHistoryId = dto.ClinicalHistoryId
            };
        }
    }
}
