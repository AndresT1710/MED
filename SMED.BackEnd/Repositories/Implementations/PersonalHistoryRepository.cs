using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PersonalHistoryRepository : IRepository<PersonalHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public PersonalHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PersonalHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.PersonalHistories
                .Include(ph => ph.DiseaseNavigation)
                    .ThenInclude(d => d.DiseaseTypeNavigation)
                .Include(ph => ph.MedicalRecordNavigation)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<PersonalHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.PersonalHistories
                .Include(ph => ph.DiseaseNavigation)
                    .ThenInclude(d => d.DiseaseTypeNavigation)
                .Include(ph => ph.MedicalRecordNavigation)
                .FirstOrDefaultAsync(ph => ph.PersonalHistoryId == id);

            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<PersonalHistoryDTO> AddAsync(PersonalHistoryDTO dto)
        {
            var clinicalHistoryExists = await _context.ClinicalHistories
                .AnyAsync(ch => ch.ClinicalHistoryId == dto.ClinicalHistoryId);

            if (!clinicalHistoryExists)
                throw new ArgumentException($"No existe ClinicalHistory con ID {dto.ClinicalHistoryId}");

            var entity = MapToEntity(dto);
            _context.PersonalHistories.Add(entity);
            await _context.SaveChangesAsync();

            // Cargar relaciones para devolver DTO completo con nombres
            await _context.Entry(entity)
                .Reference(ph => ph.DiseaseNavigation)
                .LoadAsync();
            if (entity.DiseaseNavigation != null)
            {
                await _context.Entry(entity.DiseaseNavigation)
                    .Reference(d => d.DiseaseTypeNavigation)
                    .LoadAsync();
            }

            return MapToDto(entity);
        }

        public async Task<PersonalHistoryDTO?> UpdateAsync(PersonalHistoryDTO dto)
        {
            var entity = await _context.PersonalHistories.FindAsync(dto.PersonalHistoryId);
            if (entity == null) return null;

            if (entity.ClinicalHistoryId != dto.ClinicalHistoryId)
            {
                var clinicalHistoryExists = await _context.ClinicalHistories
                    .AnyAsync(ch => ch.ClinicalHistoryId == dto.ClinicalHistoryId);

                if (!clinicalHistoryExists)
                    throw new ArgumentException($"No existe ClinicalHistory con ID {dto.ClinicalHistoryId}");
            }

            entity.MedicalRecordNumber = dto.MedicalRecordNumber;
            entity.Description = dto.Description;
            entity.RegistrationDate = dto.RegistrationDate;
            entity.DiseaseId = dto.DiseaseId;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;

            await _context.SaveChangesAsync();

            // Cargar relaciones para devolver DTO completo con nombres
            await _context.Entry(entity)
                .Reference(ph => ph.DiseaseNavigation)
                .LoadAsync();
            if (entity.DiseaseNavigation != null)
            {
                await _context.Entry(entity.DiseaseNavigation)
                    .Reference(d => d.DiseaseTypeNavigation)
                    .LoadAsync();
            }

            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PersonalHistories.FindAsync(id);
            if (entity == null) return false;

            _context.PersonalHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Map entity to DTO
        private PersonalHistoryDTO MapToDto(PersonalHistory entity)
        {
            return new PersonalHistoryDTO
            {
                PersonalHistoryId = entity.PersonalHistoryId,
                MedicalRecordNumber = entity.MedicalRecordNumber,
                Description = entity.Description,
                RegistrationDate = entity.RegistrationDate,
                DiseaseId = entity.DiseaseId,
                ClinicalHistoryId = entity.ClinicalHistoryId,

                DiseaseName = entity.DiseaseNavigation?.Name,
                DiseaseTypeName = entity.DiseaseNavigation?.DiseaseTypeNavigation?.Name
            };
        }

        // Map DTO to entity
        private PersonalHistory MapToEntity(PersonalHistoryDTO dto)
        {
            return new PersonalHistory
            {
                PersonalHistoryId = dto.PersonalHistoryId,
                MedicalRecordNumber = dto.MedicalRecordNumber,
                Description = dto.Description,
                RegistrationDate = dto.RegistrationDate,
                DiseaseId = dto.DiseaseId,
                ClinicalHistoryId = dto.ClinicalHistoryId
            };
        }
    }
}
