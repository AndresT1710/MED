using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;


namespace SMED.BackEnd.Repositories.Implementations
{
    public class DiseaseRepository : IRepository<DiseaseDTO, int>
    {
        private readonly SGISContext _context;

        public DiseaseRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<DiseaseDTO>> GetAllAsync()
        {
            var entities = await _context.Diseases.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<DiseaseDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Diseases.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<DiseaseDTO> AddAsync(DiseaseDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Diseases.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<DiseaseDTO?> UpdateAsync(DiseaseDTO dto)
        {
            var entity = await _context.Diseases.FindAsync(dto.DiseaseId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            entity.DiseaseTypeId = dto.DiseaseTypeId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Diseases.FindAsync(id);
            if (entity == null) return false;

            _context.Diseases.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        //Mapeo manual
        private DiseaseDTO MapToDto(Disease entity)
        {
            return new DiseaseDTO
            {
                DiseaseId = entity.DiseaseId,
                Name = entity.Name,
                DiseaseTypeId = entity.DiseaseTypeId
            };
        }

        private Disease MapToEntity(DiseaseDTO dto)
        {
            return new Disease
            {
                DiseaseId = dto.DiseaseId,
                Name = dto.Name,
                DiseaseTypeId = dto.DiseaseTypeId
            };
        }
    }

}
