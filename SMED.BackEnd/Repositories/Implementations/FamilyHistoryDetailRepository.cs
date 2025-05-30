using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;


namespace SMED.BackEnd.Repositories.Implementations
{
    public class FamilyHistoryDetailRepository : IRepository<FamilyHistoryDetailDTO, int>
    {
        private readonly SGISContext _context;

        public FamilyHistoryDetailRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<FamilyHistoryDetailDTO>> GetAllAsync()
        {
            var entities = await _context.FamilyHistoryDetails.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<FamilyHistoryDetailDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.FamilyHistoryDetails.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<FamilyHistoryDetailDTO> AddAsync(FamilyHistoryDetailDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.FamilyHistoryDetails.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<FamilyHistoryDetailDTO?> UpdateAsync(FamilyHistoryDetailDTO dto)
        {
            var entity = await _context.FamilyHistoryDetails.FindAsync(dto.FamilyHistoryDetailId);
            if (entity == null) return null;

            entity.MedicalRecordNumber = dto.MedicalRecordNumber;
            entity.Description = dto.Description;
            entity.RegistrationDate = dto.RegistrationDate;
            entity.DiseaseId = dto.DiseaseId;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.FamilyHistoryDetails.FindAsync(id);
            if (entity == null) return false;

            _context.FamilyHistoryDetails.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapeo
        private FamilyHistoryDetailDTO MapToDto(FamilyHistoryDetail entity)
        {
            return new FamilyHistoryDetailDTO
            {
                FamilyHistoryDetailId = entity.FamilyHistoryDetailId,
                MedicalRecordNumber = entity.MedicalRecordNumber,
                Description = entity.Description,
                RegistrationDate = entity.RegistrationDate,
                DiseaseId = entity.DiseaseId,
                ClinicalHistoryId = entity.ClinicalHistoryId
            };
        }

        private FamilyHistoryDetail MapToEntity(FamilyHistoryDetailDTO dto)
        {
            return new FamilyHistoryDetail
            {
                FamilyHistoryDetailId = dto.FamilyHistoryDetailId,
                MedicalRecordNumber = dto.MedicalRecordNumber,
                Description = dto.Description,
                RegistrationDate = dto.RegistrationDate,
                DiseaseId = dto.DiseaseId,
                ClinicalHistoryId = dto.ClinicalHistoryId
            };
        }
    }

}
