using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ImageOrdersRepository : IRepository<ImageOrdersDTO, int>
    {
        private readonly SGISContext _context;

        public ImageOrdersRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ImageOrdersDTO>> GetAllAsync()
        {
            var orders = await _context.ImageOrders.ToListAsync();
            return orders.Select(MapToDto).ToList();
        }

        public async Task<ImageOrdersDTO?> GetByIdAsync(int id)
        {
            var order = await _context.ImageOrders.FindAsync(id);
            return order == null ? null : MapToDto(order);
        }

        public async Task<ImageOrdersDTO> AddAsync(ImageOrdersDTO dto)
        {
            var entity = new ImageOrders
            {
                Nombre = dto.Nombre,
                OrderDate = dto.OrderDate,
                Name = dto.Name
            };

            _context.ImageOrders.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<ImageOrdersDTO?> UpdateAsync(ImageOrdersDTO dto)
        {
            var entity = await _context.ImageOrders.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Nombre = dto.Nombre;
            entity.OrderDate = dto.OrderDate;
            entity.Name = dto.Name;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ImageOrders.FindAsync(id);
            if (entity == null) return false;

            _context.ImageOrders.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static ImageOrdersDTO MapToDto(ImageOrders order) => new ImageOrdersDTO
        {
            Id = order.Id,
            Nombre = order.Nombre,
            OrderDate = order.OrderDate,
            Name = order.Name
        };
    }

}
