using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PostnatalHistoryRepository : IRepository<PostnatalHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public PostnatalHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PostnatalHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.PostnatalHistories
                .Include(ph => ph.ClinicalHistory)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<PostnatalHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.PostnatalHistories
                .Include(ph => ph.ClinicalHistory)
                .FirstOrDefaultAsync(ph => ph.PostNatalId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<PostnatalHistoryDTO> AddAsync(PostnatalHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.PostnatalHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<PostnatalHistoryDTO?> UpdateAsync(PostnatalHistoryDTO dto)
        {
            var entity = await _context.PostnatalHistories.FindAsync(dto.PostNatalId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.Description = dto.Description;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PostnatalHistories.FindAsync(id);
            if (entity == null) return false;
            _context.PostnatalHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private PostnatalHistoryDTO MapToDto(PostnatalHistory entity)
        {
            return new PostnatalHistoryDTO
            {
                PostNatalId = entity.PostNatalId,
                HistoryNumber = entity.HistoryNumber,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                Description = entity.Description
            };
        }

        private PostnatalHistory MapToEntity(PostnatalHistoryDTO dto)
        {
            return new PostnatalHistory
            {
                PostNatalId = dto.PostNatalId,
                HistoryNumber = dto.HistoryNumber,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                Description = dto.Description
            };
        }
    }
}