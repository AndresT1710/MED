using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class SystemsDevicesRepository : IRepository<SystemsDevicesDTO, int>
    {
        private readonly SGISContext _context;

        public SystemsDevicesRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<SystemsDevicesDTO>> GetAllAsync()
        {
            var devices = await _context.SystemsDevices.ToListAsync();
            return devices.Select(MapToDto).ToList();
        }

        public async Task<SystemsDevicesDTO?> GetByIdAsync(int id)
        {
            var device = await _context.SystemsDevices.FindAsync(id);
            return device == null ? null : MapToDto(device);
        }

        public async Task<SystemsDevicesDTO> AddAsync(SystemsDevicesDTO dto)
        {
            var entity = new SystemsDevices
            {
                Name = dto.Name
            };

            _context.SystemsDevices.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<SystemsDevicesDTO?> UpdateAsync(SystemsDevicesDTO dto)
        {
            var entity = await _context.SystemsDevices.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SystemsDevices.FindAsync(id);
            if (entity == null) return false;

            _context.SystemsDevices.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static SystemsDevicesDTO MapToDto(SystemsDevices device) => new SystemsDevicesDTO
        {
            Id = device.Id,
            Name = device.Name
        };
    }

}
