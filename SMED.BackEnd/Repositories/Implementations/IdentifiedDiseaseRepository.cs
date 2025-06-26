using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class IdentifiedDiseaseRepository : IRepository<IdentifiedDiseaseDTO, int>
    {
        private readonly SGISContext _context;

        public IdentifiedDiseaseRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<IdentifiedDiseaseDTO>> GetAllAsync()
        {
            var diseases = await _context.IdentifiedDiseases.ToListAsync();
            return diseases.Select(MapToDto).ToList();
        }

        public async Task<IdentifiedDiseaseDTO?> GetByIdAsync(int id)
        {
            var disease = await _context.IdentifiedDiseases.FindAsync(id);
            return disease == null ? null : MapToDto(disease);
        }

        public async Task<IdentifiedDiseaseDTO> AddAsync(IdentifiedDiseaseDTO dto)
        {
            var entity = new IdentifiedDisease
            {
                Description = dto.Description,
                DiseaseId = dto.DiseaseId,
                MedicalCareId = dto.MedicalCareId
            };

            _context.IdentifiedDiseases.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<IdentifiedDiseaseDTO?> UpdateAsync(IdentifiedDiseaseDTO dto)
        {
            var entity = await _context.IdentifiedDiseases.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Description = dto.Description;
            entity.DiseaseId = dto.DiseaseId;
            entity.MedicalCareId = dto.MedicalCareId;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.IdentifiedDiseases.FindAsync(id);
            if (entity == null) return false;

            _context.IdentifiedDiseases.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static IdentifiedDiseaseDTO MapToDto(IdentifiedDisease disease) => new IdentifiedDiseaseDTO
        {
            Id = disease.Id,
            Description = disease.Description,
            DiseaseId = disease.DiseaseId,
            MedicalCareId = disease.MedicalCareId
        };
    }

}
