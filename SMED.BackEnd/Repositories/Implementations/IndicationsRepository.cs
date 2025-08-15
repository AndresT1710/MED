using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class IndicationsRepository : IRepository<IndicationsDTO, int>
    {
        private readonly SGISContext _context;

        public IndicationsRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<IndicationsDTO>> GetAllAsync()
        {
            var indications = await _context.Indications
                .Include(i => i.Treatment)
                .ToListAsync();
            return indications.Select(MapToDto).ToList();
        }

        public async Task<IndicationsDTO?> GetByIdAsync(int id)
        {
            var indication = await _context.Indications
                .Include(i => i.Treatment)
                .FirstOrDefaultAsync(i => i.Id == id);
            return indication == null ? null : MapToDto(indication);
        }

        public async Task<IndicationsDTO> AddAsync(IndicationsDTO dto)
        {
            // ✅ Validar que el tratamiento existe
            var treatmentExists = await _context.Treatments
                .AnyAsync(t => t.Id == dto.TreatmentId);

            if (!treatmentExists)
            {
                throw new InvalidOperationException($"El tratamiento con ID {dto.TreatmentId} no existe.");
            }

            var entity = new Indications
            {
                Description = dto.Description ?? "Sin descripción",
                TreatmentId = dto.TreatmentId
            };

            _context.Indications.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<IndicationsDTO?> UpdateAsync(IndicationsDTO dto)
        {
            var entity = await _context.Indications.FindAsync(dto.Id);
            if (entity == null) return null;

            // ✅ Validar que el tratamiento existe
            var treatmentExists = await _context.Treatments
                .AnyAsync(t => t.Id == dto.TreatmentId);

            if (!treatmentExists)
            {
                throw new InvalidOperationException($"El tratamiento con ID {dto.TreatmentId} no existe.");
            }

            entity.Description = dto.Description ?? entity.Description;
            entity.TreatmentId = dto.TreatmentId;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Indications.FindAsync(id);
            if (entity == null) return false;

            _context.Indications.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        // ✅ Método para obtener indicaciones por TreatmentId
        public async Task<List<IndicationsDTO>> GetByTreatmentIdAsync(int treatmentId)
        {
            var indications = await _context.Indications
                .Include(i => i.Treatment)
                .Where(i => i.TreatmentId == treatmentId)
                .ToListAsync();

            return indications.Select(MapToDto).ToList();
        }

        // ✅ Método para obtener indicaciones por MedicalDiagnosisId (a través de los tratamientos)
        public async Task<List<IndicationsDTO>> GetByMedicalDiagnosisIdAsync(int medicalDiagnosisId)
        {
            var indications = await _context.Indications
                .Include(i => i.Treatment)
                .Where(i => i.Treatment.MedicalDiagnosisId == medicalDiagnosisId)
                .ToListAsync();

            return indications.Select(MapToDto).ToList();
        }

        // ✅ Método para crear/actualizar indicación para un diagnóstico (usando tratamiento base)
        public async Task<IndicationsDTO> CreateOrUpdateForDiagnosisAsync(int medicalDiagnosisId, string description)
        {
            // Buscar si ya existe un tratamiento base para indicaciones en este diagnóstico
            var baseTreatment = await _context.Treatments
                .FirstOrDefaultAsync(t => t.MedicalDiagnosisId == medicalDiagnosisId &&
                                         t.Description == "Indicaciones generales");

            if (baseTreatment == null)
            {
                // Crear un tratamiento base para las indicaciones
                baseTreatment = new Treatment
                {
                    MedicalDiagnosisId = medicalDiagnosisId,
                    Description = "Indicaciones generales"
                };

                _context.Treatments.Add(baseTreatment);
                await _context.SaveChangesAsync();
            }

            // Buscar si ya existe una indicación para este tratamiento
            var existingIndication = await _context.Indications
                .FirstOrDefaultAsync(i => i.TreatmentId == baseTreatment.Id);

            if (existingIndication != null)
            {
                // Actualizar la indicación existente
                existingIndication.Description = description;
                await _context.SaveChangesAsync();
                return MapToDto(existingIndication);
            }
            else
            {
                // Crear nueva indicación
                var newIndication = new Indications
                {
                    Description = description,
                    TreatmentId = baseTreatment.Id
                };

                _context.Indications.Add(newIndication);
                await _context.SaveChangesAsync();
                return MapToDto(newIndication);
            }
        }

        // ✅ Método para eliminar indicaciones de un diagnóstico
        public async Task<bool> DeleteByMedicalDiagnosisIdAsync(int medicalDiagnosisId)
        {
            var baseTreatment = await _context.Treatments
                .FirstOrDefaultAsync(t => t.MedicalDiagnosisId == medicalDiagnosisId &&
                                         t.Description == "Indicaciones generales");

            if (baseTreatment != null)
            {
                var indications = await _context.Indications
                    .Where(i => i.TreatmentId == baseTreatment.Id)
                    .ToListAsync();

                _context.Indications.RemoveRange(indications);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        private static IndicationsDTO MapToDto(Indications indication) => new IndicationsDTO
        {
            Id = indication.Id,
            Description = indication.Description,
            TreatmentId = indication.TreatmentId
        };
    }
}
