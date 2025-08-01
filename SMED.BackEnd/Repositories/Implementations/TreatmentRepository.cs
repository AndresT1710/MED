using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class TreatmentRepository : IRepository<TreatmentDTO, int>
    {
        private readonly SGISContext _context;

        public TreatmentRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<TreatmentDTO>> GetAllAsync()
        {
            var treatments = await _context.Treatments
                .Include(t => t.Diagnoses) // ✅ Incluir diagnósticos
                .ToListAsync();

            return treatments.Select(MapToDto).ToList();
        }

        public async Task<TreatmentDTO?> GetByIdAsync(int id)
        {
            var treatment = await _context.Treatments
                .Include(t => t.Diagnoses) // ✅ Incluir diagnósticos
                .FirstOrDefaultAsync(t => t.Id == id);

            return treatment == null ? null : MapToDto(treatment);
        }

        public async Task<TreatmentDTO> AddAsync(TreatmentDTO dto)
        {
            var entity = new Treatment
            {
                Recommendations = dto.Recommendations
            };

            // ✅ Asignar diagnósticos si se proporcionan
            if (dto.DiagnosisIds.Any())
            {
                var diagnoses = await _context.Diagnosis
                    .Where(d => dto.DiagnosisIds.Contains(d.Id))
                    .ToListAsync();

                entity.Diagnoses = diagnoses;
            }

            _context.Treatments.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<TreatmentDTO?> UpdateAsync(TreatmentDTO dto)
        {
            var entity = await _context.Treatments
                .Include(t => t.Diagnoses)
                .FirstOrDefaultAsync(t => t.Id == dto.Id);

            if (entity == null) return null;

            entity.Recommendations = dto.Recommendations;

            // ✅ Actualizar diagnósticos
            entity.Diagnoses.Clear();
            if (dto.DiagnosisIds.Any())
            {
                var diagnoses = await _context.Diagnosis
                    .Where(d => dto.DiagnosisIds.Contains(d.Id))
                    .ToListAsync();

                foreach (var diagnosis in diagnoses)
                {
                    entity.Diagnoses.Add(diagnosis);
                }
            }

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Treatments
                .Include(t => t.Diagnoses)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null) return false;

            _context.Treatments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ Método adicional para asignar diagnósticos
        public async Task<bool> AssignDiagnosesAsync(int treatmentId, List<int> diagnosisIds)
        {
            var treatment = await _context.Treatments
                .Include(t => t.Diagnoses)
                .FirstOrDefaultAsync(t => t.Id == treatmentId);

            if (treatment == null) return false;

            var diagnoses = await _context.Diagnosis
                .Where(d => diagnosisIds.Contains(d.Id))
                .ToListAsync();

            // Limpiar diagnósticos existentes y agregar los nuevos
            treatment.Diagnoses.Clear();
            foreach (var diagnosis in diagnoses)
            {
                treatment.Diagnoses.Add(diagnosis);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ Mapeo simple con solo IDs
        private static TreatmentDTO MapToDto(Treatment treatment) => new TreatmentDTO
        {
            Id = treatment.Id,
            Recommendations = treatment.Recommendations,
            DiagnosisIds = treatment.Diagnoses.Select(d => d.Id).ToList()
        };
    }
}
