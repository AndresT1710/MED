using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class MedicalReferralRepository : IRepository<MedicalReferralDTO, int>
    {
        private readonly SGISContext _context;

        public MedicalReferralRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<MedicalReferralDTO>> GetAllAsync()
        {
            var referrals = await _context.MedicalReferrals
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                        .ThenInclude(p => p.PersonNavigation)
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.HealthProfessional)
                        .ThenInclude(hp => hp.PersonNavigation)
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.LocationNavigation)
                .Include(r => r.AttendedByProfessional)
                    .ThenInclude(abp => abp.PersonNavigation)
                .Include(r => r.Location) // Incluir la ubicación
                .ToListAsync();

            return referrals.Select(MapToDto).ToList();
        }

        public async Task<MedicalReferralDTO?> GetByIdAsync(int id)
        {
            var referral = await _context.MedicalReferrals
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                        .ThenInclude(p => p.PersonNavigation)
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.HealthProfessional)
                        .ThenInclude(hp => hp.PersonNavigation)
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.LocationNavigation)
                .Include(r => r.AttendedByProfessional)
                    .ThenInclude(abp => abp.PersonNavigation)
                .Include(r => r.Location) // Incluir la ubicación
                .FirstOrDefaultAsync(r => r.Id == id);

            return referral == null ? null : MapToDto(referral);
        }

        public async Task<MedicalReferralDTO> AddAsync(MedicalReferralDTO dto)
        {
            var entity = new MedicalReferral
            {
                DateOfReferral = dto.DateOfReferral ?? DateTime.Now,
                Description = dto.Description ?? string.Empty,
                AdditionalNotes = dto.AdditionalNotes,
                MedicalCareId = dto.MedicalCareId,
                LocationId = dto.LocationId, // Asignar LocationId
                Status = dto.Status,
                IsUrgent = dto.IsUrgent,
                AttendedDate = dto.AttendedDate,
                AttendedByProfessionalId = dto.AttendedByProfessionalId
            };

            _context.MedicalReferrals.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return await GetByIdAsync(entity.Id) ?? dto; // Retornar el DTO completo
        }

        public async Task<MedicalReferralDTO?> UpdateAsync(MedicalReferralDTO dto)
        {
            var entity = await _context.MedicalReferrals
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                        .ThenInclude(p => p.PersonNavigation)
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.HealthProfessional)
                        .ThenInclude(hp => hp.PersonNavigation)
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.LocationNavigation)
                .Include(r => r.AttendedByProfessional)
                    .ThenInclude(abp => abp.PersonNavigation)
                .Include(r => r.Location) // Incluir la ubicación
                .FirstOrDefaultAsync(r => r.Id == dto.Id);

            if (entity == null) return null;

            entity.DateOfReferral = dto.DateOfReferral;
            entity.Description = dto.Description ?? string.Empty;
            entity.AdditionalNotes = dto.AdditionalNotes;
            entity.MedicalCareId = dto.MedicalCareId;
            entity.LocationId = dto.LocationId; // Actualizar LocationId
            entity.Status = dto.Status;
            entity.IsUrgent = dto.IsUrgent;
            entity.AttendedDate = dto.AttendedDate;
            entity.AttendedByProfessionalId = dto.AttendedByProfessionalId;

            await _context.SaveChangesAsync();

            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MedicalReferrals.FindAsync(id);
            if (entity == null) return false;

            _context.MedicalReferrals.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<MedicalReferralDTO>> GetByLocationAsync(int locationId)
        {
            var referrals = await _context.MedicalReferrals
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                        .ThenInclude(p => p.PersonNavigation)
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.HealthProfessional)
                        .ThenInclude(hp => hp.PersonNavigation)
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.LocationNavigation)
                .Include(r => r.AttendedByProfessional)
                    .ThenInclude(abp => abp.PersonNavigation)
                .Include(r => r.Location) // Incluir la ubicación
                .Where(r => r.LocationId == locationId) // Cambiar a la relación directa
                .ToListAsync();

            return referrals.Select(MapToDto).ToList();
        }

        public async Task<List<MedicalReferralDTO>> GetPendingReferralsAsync()
        {
            var referrals = await _context.MedicalReferrals
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                        .ThenInclude(p => p.PersonNavigation)
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.HealthProfessional)
                        .ThenInclude(hp => hp.PersonNavigation)
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.LocationNavigation)
                .Include(r => r.AttendedByProfessional)
                    .ThenInclude(abp => abp.PersonNavigation)
                .Include(r => r.Location) // Incluir la ubicación
                .Where(r => r.Status == "Pendiente")
                .OrderBy(r => r.DateOfReferral)
                .ToListAsync();

            return referrals.Select(MapToDto).ToList();
        }

        public async Task<List<MedicalReferralDTO>> GetUrgentReferralsAsync()
        {
            var referrals = await _context.MedicalReferrals
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                        .ThenInclude(p => p.PersonNavigation)
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.HealthProfessional)
                        .ThenInclude(hp => hp.PersonNavigation)
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.LocationNavigation)
                .Include(r => r.AttendedByProfessional)
                    .ThenInclude(abp => abp.PersonNavigation)
                .Include(r => r.Location) // Incluir la ubicación
                .Where(r => r.IsUrgent && r.Status == "Pendiente")
                .OrderBy(r => r.DateOfReferral)
                .ToListAsync();

            return referrals.Select(MapToDto).ToList();
        }

        public async Task<bool> UpdateStatusAsync(int id, string status, int? attendedByProfessionalId = null)
        {
            var entity = await _context.MedicalReferrals.FindAsync(id);
            if (entity == null) return false;

            entity.Status = status;
            entity.AttendedByProfessionalId = attendedByProfessionalId;

            if (status == "Atendido" && !entity.AttendedDate.HasValue)
            {
                entity.AttendedDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MedicalReferralDTO>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            var referrals = await _context.MedicalReferrals
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                        .ThenInclude(p => p.PersonNavigation)
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.HealthProfessional)
                        .ThenInclude(hp => hp.PersonNavigation)
                .Include(r => r.MedicalCare)
                    .ThenInclude(mc => mc.LocationNavigation)
                .Include(r => r.AttendedByProfessional)
                    .ThenInclude(abp => abp.PersonNavigation)
                .Include(r => r.Location) // Incluir la ubicación
                .Where(r => r.MedicalCareId == medicalCareId)
                .ToListAsync();

            return referrals.Select(MapToDto).ToList();
        }

        private static MedicalReferralDTO MapToDto(MedicalReferral referral)
        {
            var dto = new MedicalReferralDTO
            {
                Id = referral.Id,
                DateOfReferral = referral.DateOfReferral,
                Description = referral.Description,
                AdditionalNotes = referral.AdditionalNotes,
                MedicalCareId = referral.MedicalCareId,
                LocationId = referral.LocationId, // Mapear LocationId
                Status = referral.Status ?? "Pendiente",
                IsUrgent = referral.IsUrgent,
                AttendedDate = referral.AttendedDate,
                AttendedByProfessionalId = referral.AttendedByProfessionalId
            };

            // Propiedades enriquecidas desde las relaciones
            if (referral.MedicalCare?.Patient?.PersonNavigation != null)
            {
                var patient = referral.MedicalCare.Patient.PersonNavigation;
                dto.PatientName = $"{patient.FirstName} {patient.LastName}";
                dto.PatientAge = CalculateAge(patient.BirthDate);
            }
            else
            {
                dto.PatientName = "N/A";
                dto.PatientAge = 0;
            }

            dto.MedicalRecordNumber = referral.MedicalCare?.Patient?.PersonId.ToString() ?? "N/A";
            dto.IsForGeneralMedicine = referral.MedicalCare?.LocationNavigation?.Name?.Contains("Medicina General") == true;
            dto.ReferringArea = referral.MedicalCare?.LocationNavigation?.Name ?? "N/A";

            if (referral.MedicalCare?.HealthProfessional?.PersonNavigation != null)
            {
                var professional = referral.MedicalCare.HealthProfessional.PersonNavigation;
                dto.ReferringProfessional = $"{professional.FirstName} {professional.LastName}";
            }
            else
            {
                dto.ReferringProfessional = "N/A";
            }

            if (referral.AttendedByProfessional?.PersonNavigation != null)
            {
                var attendedBy = referral.AttendedByProfessional.PersonNavigation;
                dto.AttendedBy = $"{attendedBy.FirstName} {attendedBy.LastName}";
            }

            // Nueva propiedad para el nombre de la ubicación
            dto.LocationName = referral.Location?.Name ?? "N/A";

            return dto;
        }

        private static int CalculateAge(DateTime? dateOfBirth)
        {
            if (!dateOfBirth.HasValue) return 0;

            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Value.Year;
            if (dateOfBirth.Value.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}