using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class DocumentTypeRepository : IRepository<DocumentTypeDTO, int>
    {
        private readonly SGISContext _context;

        public DocumentTypeRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<DocumentTypeDTO>> GetAllAsync()
        {
            var entities = await _context.DocumentTypes.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<DocumentTypeDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.DocumentTypes.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<DocumentTypeDTO> AddAsync(DocumentTypeDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.DocumentTypes.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<DocumentTypeDTO?> UpdateAsync(DocumentTypeDTO dto)
        {
            var entity = await _context.DocumentTypes.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.DocumentTypes.FindAsync(id);
            if (entity == null) return false;

            _context.DocumentTypes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private DocumentTypeDTO MapToDto(DocumentType entity)
        {
            return new DocumentTypeDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        private DocumentType MapToEntity(DocumentTypeDTO dto)
        {
            return new DocumentType
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}