using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class TypeOfServiceRepository : IRepository<TypeOfServiceDTO, int>
    {
        private readonly SGISContext _context;

        public TypeOfServiceRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<TypeOfServiceDTO>> GetAllAsync() =>
            await _context.TypeOfServices
                .Select(t => new TypeOfServiceDTO { Id = t.Id, Name = t.Name })
                .ToListAsync();

        public async Task<TypeOfServiceDTO?> GetByIdAsync(int id)
        {
            var t = await _context.TypeOfServices.FindAsync(id);
            return t == null ? null : new TypeOfServiceDTO { Id = t.Id, Name = t.Name };
        }

        public async Task<TypeOfServiceDTO> AddAsync(TypeOfServiceDTO dto)
        {
            var entity = new TypeOfService { Name = dto.Name };
            _context.TypeOfServices.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<TypeOfServiceDTO?> UpdateAsync(TypeOfServiceDTO dto)
        {
            var entity = await _context.TypeOfServices.FindAsync(dto.Id);
            if (entity == null) return null;
            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TypeOfServices.FindAsync(id);
            if (entity == null) return false;
            _context.TypeOfServices.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
