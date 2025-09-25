using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class NeuropsychologicalHistoryRepository : IRepository<NeuropsychologicalHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public NeuropsychologicalHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<NeuropsychologicalHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.NeuropsychologicalHistories
                .Include(nh => nh.ClinicalHistory)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<NeuropsychologicalHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.NeuropsychologicalHistories
                .Include(nh => nh.ClinicalHistory)
                .FirstOrDefaultAsync(nh => nh.NeuropsychologicalHistoryId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<NeuropsychologicalHistoryDTO> AddAsync(NeuropsychologicalHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.NeuropsychologicalHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<NeuropsychologicalHistoryDTO?> UpdateAsync(NeuropsychologicalHistoryDTO dto)
        {
            var entity = await _context.NeuropsychologicalHistories.FindAsync(dto.NeuropsychologicalHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.Description = dto.Description;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.NeuropsychologicalHistories.FindAsync(id);
            if (entity == null) return false;
            _context.NeuropsychologicalHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private NeuropsychologicalHistoryDTO MapToDto(NeuropsychologicalHistory entity)
        {
            return new NeuropsychologicalHistoryDTO
            {
                NeuropsychologicalHistoryId = entity.NeuropsychologicalHistoryId,
                HistoryNumber = entity.HistoryNumber,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                Description = entity.Description
            };
        }

        private NeuropsychologicalHistory MapToEntity(NeuropsychologicalHistoryDTO dto)
        {
            return new NeuropsychologicalHistory
            {
                NeuropsychologicalHistoryId = dto.NeuropsychologicalHistoryId,
                HistoryNumber = dto.HistoryNumber,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                Description = dto.Description
            };
        }
    }
}