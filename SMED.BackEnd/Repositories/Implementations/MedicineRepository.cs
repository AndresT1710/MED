using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class MedicineRepository : IRepository<MedicineDTO, int>
    {
        private readonly SGISContext _context;

        public MedicineRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<MedicineDTO>> GetAllAsync()
        {
            var medicines = await _context.Medicines.ToListAsync();
            return medicines.Select(MapToDto).ToList();
        }

        public async Task<MedicineDTO?> GetByIdAsync(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            return medicine == null ? null : MapToDto(medicine);
        }

        public async Task<MedicineDTO> AddAsync(MedicineDTO dto)
        {
            var entity = new Medicine
            {
                Name = dto.Name,
                Weight = dto.Weight
            };

            _context.Medicines.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<MedicineDTO?> UpdateAsync(MedicineDTO dto)
        {
            var entity = await _context.Medicines.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            entity.Weight = dto.Weight;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Medicines.FindAsync(id);
            if (entity == null) return false;

            _context.Medicines.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static MedicineDTO MapToDto(Medicine medicine) => new MedicineDTO
        {
            Id = medicine.Id,
            Name = medicine.Name,
            Weight = medicine.Weight
        };
    }

}
