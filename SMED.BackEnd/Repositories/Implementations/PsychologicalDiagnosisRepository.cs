using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PsychologicalDiagnosisRepository : IRepository<PsychologicalDiagnosisDTO, int>
    {
        private readonly SGISContext _context;

        public PsychologicalDiagnosisRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PsychologicalDiagnosisDTO>> GetAllAsync()
        {
            var entities = await _context.PsychologicalDiagnoses
                .Include(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.HealthProfessional)
                .Include(pd => pd.DiagnosticTypeNavigation)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<PsychologicalDiagnosisDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.PsychologicalDiagnoses
                .Include(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.HealthProfessional)
                .Include(pd => pd.DiagnosticTypeNavigation)
                .FirstOrDefaultAsync(pd => pd.PsychologicalDiagnosisId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<PsychologicalDiagnosisDTO> AddAsync(PsychologicalDiagnosisDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.PsychologicalDiagnoses.Add(entity);
            await _context.SaveChangesAsync();

            // Reload with related data
            await _context.Entry(entity)
                .Reference(pd => pd.MedicalCare)
                .Query()
                .Include(mc => mc.Patient)
                .Include(mc => mc.HealthProfessional)
                .LoadAsync();

            await _context.Entry(entity)
                .Reference(pd => pd.DiagnosticTypeNavigation)
                .LoadAsync();

            return MapToDto(entity);
        }

        public async Task<PsychologicalDiagnosisDTO?> UpdateAsync(PsychologicalDiagnosisDTO dto)
        {
            var entity = await _context.PsychologicalDiagnoses
                .Include(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.HealthProfessional)
                .Include(pd => pd.DiagnosticTypeNavigation)
                .FirstOrDefaultAsync(pd => pd.PsychologicalDiagnosisId == dto.PsychologicalDiagnosisId);

            if (entity == null) return null;

            entity.CIE10 = dto.CIE10;
            entity.DiagnosticTypeId = dto.DiagnosticTypeId;
            entity.DiagnosisMotivation = dto.DiagnosisMotivation;
            entity.DifferentialDiagnosis = dto.DifferentialDiagnosis;
            entity.Denomination = dto.Denomination;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PsychologicalDiagnoses.FindAsync(id);
            if (entity == null) return false;

            _context.PsychologicalDiagnoses.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PsychologicalDiagnosisDTO?> GetByMedicalCareIdAsync(int medicalCareId)
        {
            var entity = await _context.PsychologicalDiagnoses
                .Include(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.HealthProfessional)
                .Include(pd => pd.DiagnosticTypeNavigation)
                .FirstOrDefaultAsync(pd => pd.MedicalCareId == medicalCareId);

            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<List<PsychologicalDiagnosisDTO>> GetByCIE10Async(string cie10)
        {
            var entities = await _context.PsychologicalDiagnoses
                .Include(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.HealthProfessional)
                .Include(pd => pd.DiagnosticTypeNavigation)
                .Where(pd => pd.CIE10.Contains(cie10))
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<List<PsychologicalDiagnosisDTO>> GetByDiagnosticTypeIdAsync(int diagnosticTypeId)
        {
            var entities = await _context.PsychologicalDiagnoses
                .Include(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.HealthProfessional)
                .Include(pd => pd.DiagnosticTypeNavigation)
                .Where(pd => pd.DiagnosticTypeId == diagnosticTypeId)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private PsychologicalDiagnosisDTO MapToDto(PsychologicalDiagnosis entity)
        {
            var dto = new PsychologicalDiagnosisDTO
            {
                PsychologicalDiagnosisId = entity.PsychologicalDiagnosisId,
                MedicalCareId = entity.MedicalCareId,
                CIE10 = entity.CIE10,
                DiagnosticTypeId = entity.DiagnosticTypeId,
                Denomination = entity.Denomination,
                DifferentialDiagnosis = entity.DifferentialDiagnosis,
                DiagnosisMotivation = entity.DiagnosisMotivation,
                DiagnosticTypeName = entity.DiagnosticTypeNavigation?.Name,
                CareDate = entity.MedicalCare?.CareDate
            };

            // 🔹 Construir PatientName de forma segura
            var patientNameParts = new List<string>();

            if (entity.MedicalCare?.Patient?.PersonNavigation != null)
            {
                if (!string.IsNullOrEmpty(entity.MedicalCare.Patient.PersonNavigation.FirstName))
                    patientNameParts.Add(entity.MedicalCare.Patient.PersonNavigation.FirstName);

                if (!string.IsNullOrEmpty(entity.MedicalCare.Patient.PersonNavigation.LastName))
                    patientNameParts.Add(entity.MedicalCare.Patient.PersonNavigation.LastName);
            }

            dto.PatientName = patientNameParts.Any()
                ? string.Join(" ", patientNameParts)
                : "Paciente no disponible";

            // 🔹 Construir HealthProfessionalName de forma segura
            var professionalNameParts = new List<string>();

            if (entity.MedicalCare?.HealthProfessional?.PersonNavigation != null)
            {
                if (!string.IsNullOrEmpty(entity.MedicalCare.HealthProfessional.PersonNavigation.FirstName))
                    professionalNameParts.Add(entity.MedicalCare.HealthProfessional.PersonNavigation.FirstName);

                if (!string.IsNullOrEmpty(entity.MedicalCare.HealthProfessional.PersonNavigation.LastName))
                    professionalNameParts.Add(entity.MedicalCare.HealthProfessional.PersonNavigation.LastName);
            }

            dto.HealthProfessionalName = professionalNameParts.Any()
                ? string.Join(" ", professionalNameParts)
                : "Profesional no disponible";

            return dto;
        }

        private PsychologicalDiagnosis MapToEntity(PsychologicalDiagnosisDTO dto)
        {
            return new PsychologicalDiagnosis
            {
                PsychologicalDiagnosisId = dto.PsychologicalDiagnosisId,
                MedicalCareId = dto.MedicalCareId,
                CIE10 = dto.CIE10,
                DifferentialDiagnosis = dto.DifferentialDiagnosis,
                DiagnosisMotivation = dto.DiagnosisMotivation,
                DiagnosticTypeId = dto.DiagnosticTypeId,
                Denomination = dto.Denomination
            };
        }
    }
}