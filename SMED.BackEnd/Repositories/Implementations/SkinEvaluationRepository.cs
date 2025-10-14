using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class SkinEvaluationRepository : IRepository<SkinEvaluationDTO, int>
    {
        private readonly SGISContext _context;

        public SkinEvaluationRepository(SGISContext context)
        {
            _context = context;
        }

        // ===========================================
        // 🔹 Obtener todas las evaluaciones de piel
        // ===========================================
        public async Task<List<SkinEvaluationDTO>> GetAllAsync()
        {
            var entities = await _context.SkinEvaluations
                .Include(s => s.Color)
                .Include(s => s.Edema)
                .Include(s => s.Status)
                .Include(s => s.Swelling)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // ===========================================
        // 🔹 Obtener una evaluación por ID
        // ===========================================
        public async Task<SkinEvaluationDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.SkinEvaluations
                .Include(s => s.Color)
                .Include(s => s.Edema)
                .Include(s => s.Status)
                .Include(s => s.Swelling)
                .FirstOrDefaultAsync(s => s.SkinEvaluationId == id);

            return entity != null ? MapToDto(entity) : null;
        }

        // ===========================================
        // 🔹 Agregar nueva evaluación
        // ===========================================
        public async Task<SkinEvaluationDTO> AddAsync(SkinEvaluationDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.SkinEvaluations.Add(entity);
            await _context.SaveChangesAsync();

            // Cargar entidades relacionadas
            await _context.Entry(entity).Reference(s => s.Color).LoadAsync();
            await _context.Entry(entity).Reference(s => s.Edema).LoadAsync();
            await _context.Entry(entity).Reference(s => s.Status).LoadAsync();
            await _context.Entry(entity).Reference(s => s.Swelling).LoadAsync();

            return MapToDto(entity);
        }

        // ===========================================
        // 🔹 Actualizar evaluación existente
        // ===========================================
        public async Task<SkinEvaluationDTO?> UpdateAsync(SkinEvaluationDTO dto)
        {
            var entity = await _context.SkinEvaluations.FindAsync(dto.SkinEvaluationId);
            if (entity == null) return null;

            entity.MedicalCareId = dto.MedicalCareId;
            entity.ColorId = dto.ColorId;
            entity.EdemaId = dto.EdemaId;
            entity.StatusId = dto.StatusId;
            entity.SwellingId = dto.SwellingId;
            entity.EvaluationDate = dto.EvaluationDate;

            await _context.SaveChangesAsync();

            // Recargar entidades relacionadas para el DTO actualizado
            await _context.Entry(entity).Reference(s => s.Color).LoadAsync();
            await _context.Entry(entity).Reference(s => s.Edema).LoadAsync();
            await _context.Entry(entity).Reference(s => s.Status).LoadAsync();
            await _context.Entry(entity).Reference(s => s.Swelling).LoadAsync();

            return MapToDto(entity);
        }

        // ===========================================
        // 🔹 Eliminar evaluación
        // ===========================================
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SkinEvaluations.FindAsync(id);
            if (entity == null) return false;

            _context.SkinEvaluations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<SkinEvaluationDTO>> GetByCareIdAsync(int medicalCareId)
        {
            var entities = await _context.SkinEvaluations
                .Include(s => s.Color)
                .Include(s => s.Edema)
                .Include(s => s.Status)
                .Include(s => s.Swelling)
                .Where(s => s.MedicalCareId == medicalCareId)
                .OrderByDescending(s => s.EvaluationDate)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // ===========================================
        // 🔹 Mapeos
        // ===========================================
        private SkinEvaluationDTO MapToDto(SkinEvaluation entity)
        {
            return new SkinEvaluationDTO
            {
                SkinEvaluationId = entity.SkinEvaluationId,
                MedicalCareId = entity.MedicalCareId,
                ColorId = entity.ColorId,
                ColorName = entity.Color?.Name ?? "No registrado",
                EdemaId = entity.EdemaId,
                EdemaName = entity.Edema?.Name ?? "No registrado",
                StatusId = entity.StatusId,
                StatusName = entity.Status?.Name ?? "No registrado",
                SwellingId = entity.SwellingId,
                SwellingName = entity.Swelling?.Name ?? "No registrado",
                EvaluationDate = entity.EvaluationDate
            };
        }

        private SkinEvaluation MapToEntity(SkinEvaluationDTO dto)
        {
            return new SkinEvaluation
            {
                SkinEvaluationId = dto.SkinEvaluationId,
                MedicalCareId = dto.MedicalCareId,
                ColorId = dto.ColorId,
                EdemaId = dto.EdemaId,
                StatusId = dto.StatusId,
                SwellingId = dto.SwellingId,
                EvaluationDate = dto.EvaluationDate
            };
        }
    }
}
