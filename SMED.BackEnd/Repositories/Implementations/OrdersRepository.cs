using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class OrdersRepository : IRepository<OrdersDTO, int>
    {
        private readonly SGISContext _context;

        public OrdersRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<OrdersDTO>> GetAllAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            return orders.Select(MapToDto).ToList();
        }

        public async Task<OrdersDTO?> GetByIdAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            return order == null ? null : MapToDto(order);
        }

        public async Task<OrdersDTO> AddAsync(OrdersDTO dto)
        {
            var entity = new Orders
            {
                Nombre = dto.Nombre,
                OrderDate = dto.OrderDate
            };

            _context.Orders.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<OrdersDTO?> UpdateAsync(OrdersDTO dto)
        {
            var entity = await _context.Orders.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Nombre = dto.Nombre;
            entity.OrderDate = dto.OrderDate;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Orders.FindAsync(id);
            if (entity == null) return false;

            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private static OrdersDTO MapToDto(Orders order) => new OrdersDTO
        {
            Id = order.Id,
            Nombre = order.Nombre,
            OrderDate = order.OrderDate
        };
    }

}
