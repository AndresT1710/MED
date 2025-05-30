using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;


namespace SMED.BackEnd.Repositories.Implementations
{
    public class DiseaseTypeRepository : IRepository<DiseaseTypeDTO, int>
    {
        private readonly SGISContext _context;

        public DiseaseTypeRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<DiseaseTypeDTO>> GetAllAsync()
        {
            var entities = await _context.DiseaseTypes.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<DiseaseTypeDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.DiseaseTypes.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<DiseaseTypeDTO> AddAsync(DiseaseTypeDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.DiseaseTypes.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<DiseaseTypeDTO?> UpdateAsync(DiseaseTypeDTO dto)
        {
            var entity = await _context.DiseaseTypes.FindAsync(dto.DiseaseTypeId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.DiseaseTypes.FindAsync(id);
            if (entity == null) return false;

            _context.DiseaseTypes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        //Mapeo
        private DiseaseTypeDTO MapToDto(DiseaseType entity)
        {
            return new DiseaseTypeDTO
            {
                DiseaseTypeId = entity.DiseaseTypeId,
                Name = entity.Name
            };
        }

        private DiseaseType MapToEntity(DiseaseTypeDTO dto)
        {
            return new DiseaseType
            {
                DiseaseTypeId = dto.DiseaseTypeId,
                Name = dto.Name
            };
        }
    }


}
