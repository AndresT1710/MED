using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class GynecologicalHistoryRepository : IRepository<GynecologicalHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public GynecologicalHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<GynecologicalHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.GynecologicalHistories.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<GynecologicalHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.GynecologicalHistories.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<GynecologicalHistoryDTO> AddAsync(GynecologicalHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.GynecologicalHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<GynecologicalHistoryDTO?> UpdateAsync(GynecologicalHistoryDTO dto)
        {
            var entity = await _context.GynecologicalHistories.FindAsync(dto.GynecologicalHistoryId);
            if (entity == null) return null;

            entity.MedicalRecordNumber = dto.MedicalRecordNumber;
            entity.GynecologicalDevelopment = dto.GynecologicalDevelopment;
            entity.Menarche = dto.Menarche;
            entity.Pubarche = dto.Pubarche;
            entity.MenstrualCycles = dto.MenstrualCycles;
            entity.LastMenstruation = dto.LastMenstruation;
            entity.ContraceptiveMethods = dto.ContraceptiveMethods;
            entity.DiseaseId = dto.DiseaseId;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.GynecologicalHistories.FindAsync(id);
            if (entity == null) return false;

            _context.GynecologicalHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapeo
        private GynecologicalHistoryDTO MapToDto(GynecologicalHistory entity)
        {
            return new GynecologicalHistoryDTO
            {
                GynecologicalHistoryId = entity.GynecologicalHistoryId,
                MedicalRecordNumber = entity.MedicalRecordNumber,
                GynecologicalDevelopment = entity.GynecologicalDevelopment,
                Menarche = entity.Menarche,
                Pubarche = entity.Pubarche,
                MenstrualCycles = entity.MenstrualCycles,
                LastMenstruation = entity.LastMenstruation,
                ContraceptiveMethods = entity.ContraceptiveMethods,
                DiseaseId = entity.DiseaseId,
                ClinicalHistoryId = entity.ClinicalHistoryId
            };
        }

        private GynecologicalHistory MapToEntity(GynecologicalHistoryDTO dto)
        {
            return new GynecologicalHistory
            {
                GynecologicalHistoryId = dto.GynecologicalHistoryId,
                MedicalRecordNumber = dto.MedicalRecordNumber,
                GynecologicalDevelopment = dto.GynecologicalDevelopment,
                Menarche = dto.Menarche,
                Pubarche = dto.Pubarche,
                MenstrualCycles = dto.MenstrualCycles,
                LastMenstruation = dto.LastMenstruation,
                ContraceptiveMethods = dto.ContraceptiveMethods,
                DiseaseId = dto.DiseaseId,
                ClinicalHistoryId = dto.ClinicalHistoryId
            };
        }
    }

}
