using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class MentalFunctionRepository : IRepository<MentalFunctionDTO, int>
    {
        private readonly SGISContext _context;

        public MentalFunctionRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<MentalFunctionDTO>> GetAllAsync()
        {
            var entities = await _context.MentalFunctions
                .Include(mf => mf.TypeOfMentalFunction)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<MentalFunctionDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.MentalFunctions
                .Include(mf => mf.TypeOfMentalFunction)
                .FirstOrDefaultAsync(mf => mf.MentalFunctionId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<MentalFunctionDTO> AddAsync(MentalFunctionDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.MentalFunctions.Add(entity);
            await _context.SaveChangesAsync();

            // Reload with related data
            await _context.Entry(entity).Reference(mf => mf.TypeOfMentalFunction).LoadAsync();
            return MapToDto(entity);
        }

        public async Task<MentalFunctionDTO?> UpdateAsync(MentalFunctionDTO dto)
        {
            var entity = await _context.MentalFunctions.FindAsync(dto.MentalFunctionId);
            if (entity == null) return null;

            entity.MentalFunctionName = dto.FunctionName;
            entity.TypeOfMentalFunctionId = dto.TypeOfMentalFunctionId;

            await _context.SaveChangesAsync();

            // Reload with related data
            await _context.Entry(entity).Reference(mf => mf.TypeOfMentalFunction).LoadAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MentalFunctions.FindAsync(id);
            if (entity == null) return false;

            _context.MentalFunctions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MentalFunctionDTO>> GetByTypeOfMentalFunctionIdAsync(int typeOfMentalFunctionId)
        {
            var entities = await _context.MentalFunctions
                .Include(mf => mf.TypeOfMentalFunction)
                .Where(mf => mf.TypeOfMentalFunctionId == typeOfMentalFunctionId)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private MentalFunctionDTO MapToDto(MentalFunction entity)
        {
            return new MentalFunctionDTO
            {
                MentalFunctionId = entity.MentalFunctionId,
                FunctionName = entity.MentalFunctionName,
                TypeOfMentalFunctionId = entity.TypeOfMentalFunctionId,
                TypeOfMentalFunctionName = entity.TypeOfMentalFunction?.Name ?? string.Empty
            };
        }

        private MentalFunction MapToEntity(MentalFunctionDTO dto)
        {
            return new MentalFunction
            {
                MentalFunctionId = dto.MentalFunctionId,
                MentalFunctionName = dto.FunctionName,
                TypeOfMentalFunctionId = dto.TypeOfMentalFunctionId
            };
        }
    }
}