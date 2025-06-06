using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class DietaryHabitsHistoryRepository : IRepository<DietaryHabitsHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public DietaryHabitsHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<DietaryHabitsHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.DietaryHabitHistories.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<DietaryHabitsHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.DietaryHabitHistories.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<DietaryHabitsHistoryDTO> AddAsync(DietaryHabitsHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.DietaryHabitHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<DietaryHabitsHistoryDTO?> UpdateAsync(DietaryHabitsHistoryDTO dto)
        {
            var entity = await _context.DietaryHabitHistories.FindAsync(dto.DietaryHabitHistoryId);
            if (entity == null) return null;

            entity.MedicalRecordNumber = dto.MedicalRecordNumber;
            entity.Description = dto.Description;
            entity.RegistrationDate = dto.RegistrationDate;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.DietaryHabitHistories.FindAsync(id);
            if (entity == null) return false;

            _context.DietaryHabitHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private DietaryHabitsHistoryDTO MapToDto(DietaryHabitsHistory entity)
        {
            return new DietaryHabitsHistoryDTO
            {
                DietaryHabitHistoryId = entity.DietaryHabitHistoryId,
                MedicalRecordNumber = entity.MedicalRecordNumber,
                Description = entity.Description,
                RegistrationDate = entity.RegistrationDate,
                ClinicalHistoryId = entity.ClinicalHistoryId
            };
        }

        private DietaryHabitsHistory MapToEntity(DietaryHabitsHistoryDTO dto)
        {
            return new DietaryHabitsHistory
            {
                DietaryHabitHistoryId = dto.DietaryHabitHistoryId,
                MedicalRecordNumber = dto.MedicalRecordNumber,
                Description = dto.Description,
                RegistrationDate = dto.RegistrationDate,
                ClinicalHistoryId = dto.ClinicalHistoryId
            };
        }
    }
}
