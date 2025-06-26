using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class LaboratoryOrdersRepository : IRepository<LaboratoryOrdersDTO, int>
    {
        private readonly SGISContext _context;

        public LaboratoryOrdersRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<LaboratoryOrdersDTO>> GetAllAsync()
        {
            var orders = await _context.LaboratoryOrders.ToListAsync();
            return orders.Select(MapToDto).ToList();
        }

        public async Task<LaboratoryOrdersDTO?> GetByIdAsync(int id)
        {
            var order = await _context.LaboratoryOrders.FindAsync(id);
            return order == null ? null : MapToDto(order);
        }

        public async Task<LaboratoryOrdersDTO> AddAsync(LaboratoryOrdersDTO dto)
        {
            var entity = new LaboratoryOrders
            {
                Nombre = dto.Nombre,
                OrderDate = dto.OrderDate,
                Name = dto.Name
            };

            _context.LaboratoryOrders.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<LaboratoryOrdersDTO?> UpdateAsync(LaboratoryOrdersDTO dto)
        {
            var entity = await _context.LaboratoryOrders.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Nombre = dto.Nombre;
            entity.OrderDate = dto.OrderDate;
            entity.Name = dto.Name;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.LaboratoryOrders.FindAsync(id);
            if (entity == null) return false;

            _context.LaboratoryOrders.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static LaboratoryOrdersDTO MapToDto(LaboratoryOrders order) => new LaboratoryOrdersDTO
        {
            Id = order.Id,
            Nombre = order.Nombre,
            OrderDate = order.OrderDate,
            Name = order.Name
        };
    }

}
