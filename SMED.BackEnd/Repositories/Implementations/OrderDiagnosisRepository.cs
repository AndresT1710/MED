using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class OrderDiagnosisRepository : IRepository<OrderDiagnosisDTO, int>
    {
        private readonly SGISContext _context;

        public OrderDiagnosisRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDiagnosisDTO>> GetAllAsync()
        {
            var orderDiagnoses = await _context.OrderDiagnosis.ToListAsync();
            return orderDiagnoses.Select(MapToDto).ToList();
        }

        public async Task<OrderDiagnosisDTO?> GetByIdAsync(int id)
        {
            var orderDiagnosis = await _context.OrderDiagnosis.FindAsync(id);
            return orderDiagnosis == null ? null : MapToDto(orderDiagnosis);
        }

        public async Task<OrderDiagnosisDTO> AddAsync(OrderDiagnosisDTO dto)
        {
            var entity = new OrderDiagnosis
            {
                OrderId = dto.OrderId,
                DiagnosisId = dto.DiagnosisId
            };

            _context.OrderDiagnosis.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<OrderDiagnosisDTO?> UpdateAsync(OrderDiagnosisDTO dto)
        {
            var entity = await _context.OrderDiagnosis.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.OrderId = dto.OrderId;
            entity.DiagnosisId = dto.DiagnosisId;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.OrderDiagnosis.FindAsync(id);
            if (entity == null) return false;

            _context.OrderDiagnosis.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static OrderDiagnosisDTO MapToDto(OrderDiagnosis orderDiagnosis) => new OrderDiagnosisDTO
        {
            Id = orderDiagnosis.Id,
            OrderId = orderDiagnosis.OrderId,
            DiagnosisId = orderDiagnosis.DiagnosisId
        };
    }

}
