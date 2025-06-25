using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class CostOfServiceRepository : IRepository<CostOfServiceDTO, int>
    {
        private readonly SGISContext _context;

        public CostOfServiceRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<CostOfServiceDTO>> GetAllAsync()
        {
            return await _context.CostOfServices
                .Include(c => c.Service)
                .Select(c => new CostOfServiceDTO
                {
                    Id = c.CostOfServiceId,
                    Value = c.Value,
                    ServiceId = c.ServiceId,
                    ServiceName = c.Service.Name
                })
                .ToListAsync();
        }

        public async Task<CostOfServiceDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.CostOfServices.Include(c => c.Service).FirstOrDefaultAsync(c => c.CostOfServiceId == id);
            return entity == null ? null : new CostOfServiceDTO
            {
                Id = entity.CostOfServiceId,
                Value = entity.Value,
                ServiceId = entity.ServiceId,
                ServiceName = entity.Service.Name
            };
        }

        public async Task<CostOfServiceDTO> AddAsync(CostOfServiceDTO dto)
        {
            var entity = new CostOfService
            {
                Value = dto.Value,
                ServiceId = dto.ServiceId
            };

            _context.CostOfServices.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.CostOfServiceId;
            return dto;
        }

        public async Task<CostOfServiceDTO?> UpdateAsync(CostOfServiceDTO dto)
        {
            var entity = await _context.CostOfServices.FindAsync(dto.Id);
            if (entity == null) return null;
            entity.Value = dto.Value;
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.CostOfServices.FindAsync(id);
            if (entity == null) return false;
            _context.CostOfServices.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
