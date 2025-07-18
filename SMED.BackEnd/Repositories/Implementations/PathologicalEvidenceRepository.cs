using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PathologicalEvidenceRepository : IRepository<PathologicalEvidenceDTO, int>
    {
        private readonly SGISContext _context;

        public PathologicalEvidenceRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PathologicalEvidenceDTO>> GetAllAsync()
        {
            var evidences = await _context.PathologicalEvidences
                .OrderBy(p => p.Name)
                .ToListAsync();

            return evidences.Select(MapToDto).ToList();
        }

        public async Task<PathologicalEvidenceDTO?> GetByIdAsync(int id)
        {
            var evidence = await _context.PathologicalEvidences.FindAsync(id);
            return evidence == null ? null : MapToDto(evidence);
        }

        public async Task<PathologicalEvidenceDTO> AddAsync(PathologicalEvidenceDTO dto)
        {
            var entity = new PathologicalEvidence
            {
                Name = dto.Name ?? string.Empty
            };

            _context.PathologicalEvidences.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<PathologicalEvidenceDTO?> UpdateAsync(PathologicalEvidenceDTO dto)
        {
            var entity = await _context.PathologicalEvidences.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Name = dto.Name ?? string.Empty;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PathologicalEvidences.FindAsync(id);
            if (entity == null) return false;

            _context.PathologicalEvidences.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private static PathologicalEvidenceDTO MapToDto(PathologicalEvidence evidence) => new PathologicalEvidenceDTO
        {
            Id = evidence.Id,
            Name = evidence.Name
        };
    }
}
