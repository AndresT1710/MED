using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class TypeOfProceduresRepository : IRepository<TypeOfProceduresDTO, int>
    {
        private readonly SGISContext _context;

        public TypeOfProceduresRepository(SGISContext context) => _context = context;

        public async Task<List<TypeOfProceduresDTO>> GetAllAsync() =>
            await _context.TypeOfProcedures
                .Select(t => new TypeOfProceduresDTO { Id = t.Id, Name = t.Name })
                .ToListAsync();

        public async Task<TypeOfProceduresDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.TypeOfProcedures.FindAsync(id);
            return entity == null ? null : new TypeOfProceduresDTO { Id = entity.Id, Name = entity.Name };
        }

        public async Task<TypeOfProceduresDTO> AddAsync(TypeOfProceduresDTO dto)
        {
            var entity = new TypeOfProcedures { Name = dto.Name };
            _context.TypeOfProcedures.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<TypeOfProceduresDTO?> UpdateAsync(TypeOfProceduresDTO dto)
        {
            var entity = await _context.TypeOfProcedures.FindAsync(dto.Id);
            if (entity == null) return null;
            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TypeOfProcedures.FindAsync(id);
            if (entity == null) return false;
            _context.TypeOfProcedures.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
