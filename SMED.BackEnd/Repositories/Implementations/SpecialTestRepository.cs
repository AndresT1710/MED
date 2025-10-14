using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class SpecialTestRepository : IRepository<SpecialTestDTO, int>
    {
        private readonly SGISContext _context;

        public SpecialTestRepository(SGISContext context)
        {
            _context = context;
        }

        // ====================================
        // 🔹 GET ALL
        // ====================================
        public async Task<List<SpecialTestDTO>> GetAllAsync()
        {
            var entities = await _context.SpecialTests
                .Include(x => x.ResultType)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // ====================================
        // 🔹 GET BY ID
        // ====================================
        public async Task<SpecialTestDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.SpecialTests
                .Include(x => x.ResultType)
                .FirstOrDefaultAsync(x => x.SpecialTestId == id);

            return entity != null ? MapToDto(entity) : null;
        }

        // ====================================
        // 🔹 ADD
        // ====================================
        public async Task<SpecialTestDTO> AddAsync(SpecialTestDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.SpecialTests.Add(entity);
            await _context.SaveChangesAsync();

            // Devuelve el DTO actualizado con la relación cargada
            return await GetByIdAsync(entity.SpecialTestId) ?? MapToDto(entity);
        }

        // ====================================
        // 🔹 UPDATE
        // ====================================
        public async Task<SpecialTestDTO?> UpdateAsync(SpecialTestDTO dto)
        {
            var entity = await _context.SpecialTests.FindAsync(dto.SpecialTestId);
            if (entity == null) return null;

            entity.Test = dto.Test;
            entity.Observations = dto.Observations;
            entity.ResultTypeId = dto.ResultTypeId;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.SpecialTestId);
        }

        // ====================================
        // 🔹 DELETE
        // ====================================
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SpecialTests.FindAsync(id);
            if (entity == null) return false;

            _context.SpecialTests.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<SpecialTestDTO>> GetByCareIdAsync(int medicalCareId)
        {
            var entities = await _context.SpecialTests
                .Include(x => x.ResultType)
                .Where(x => x.MedicalCareId == medicalCareId)
                .OrderByDescending(x => x.SpecialTestId)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // ====================================
        // 🔹 MAPEO ENTITY → DTO
        // ====================================
        private SpecialTestDTO MapToDto(SpecialTest entity)
        {
            return new SpecialTestDTO
            {
                SpecialTestId = entity.SpecialTestId,
                Test = entity.Test,
                Observations = entity.Observations,
                ResultTypeId = entity.ResultTypeId,
                ResultTypeName = entity.ResultType?.Name, // 🔹 nombre del tipo de resultado
                MedicalCareId = entity.MedicalCareId
            };
        }

        // ====================================
        // 🔹 MAPEO DTO → ENTITY
        // ====================================
        private SpecialTest MapToEntity(SpecialTestDTO dto)
        {
            return new SpecialTest
            {
                SpecialTestId = dto.SpecialTestId,
                Test = dto.Test ?? string.Empty,
                Observations = dto.Observations ?? string.Empty,
                ResultTypeId = dto.ResultTypeId,
                MedicalCareId = dto.MedicalCareId
            };
        }
    }
}
