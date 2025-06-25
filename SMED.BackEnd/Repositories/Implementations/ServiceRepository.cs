using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ServiceRepository : IRepository<ServiceDTO, int>
    {
        private readonly SGISContext _context;

        public ServiceRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceDTO>> GetAllAsync()
        {
            var services = await _context.Services
                .Include(s => s.TypeOfService)
                .Include(s => s.CostOfService)
                .ToListAsync();

            return services.Select(MapToDto).ToList();
        }

        public async Task<ServiceDTO?> GetByIdAsync(int id)
        {
            var service = await _context.Services
                .Include(s => s.TypeOfService)
                .Include(s => s.CostOfService)
                .FirstOrDefaultAsync(s => s.Id == id);

            return service == null ? null : MapToDto(service);
        }

        public async Task<ServiceDTO> AddAsync(ServiceDTO dto)
        {
            var entity = new Service
            {
                Name = dto.Name,
                TypeOfServiceId = dto.TypeOfServiceId,
                CostOfService = new CostOfService { Value = dto.Cost }
            };

            _context.Services.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<ServiceDTO?> UpdateAsync(ServiceDTO dto)
        {
            var entity = await _context.Services
                .Include(s => s.CostOfService)
                .FirstOrDefaultAsync(s => s.Id == dto.Id);

            if (entity == null) return null;

            entity.Name = dto.Name;
            entity.TypeOfServiceId = dto.TypeOfServiceId;
            entity.CostOfService.Value = dto.Cost;

            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Services.FindAsync(id);
            if (entity == null) return false;

            _context.Services.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static ServiceDTO MapToDto(Service s) => new ServiceDTO
        {
            Id = s.Id,
            Name = s.Name,
            TypeOfServiceId = s.TypeOfServiceId,
            TypeOfServiceName = s.TypeOfService?.Name,
            Cost = s.CostOfService?.Value ?? 0f
        };
    }
}
