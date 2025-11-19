using System;

namespace SMED.Shared.DTOs
{
    public class MedicalReferralDTO
    {
        public int Id { get; set; }
        public int MedicalCareId { get; set; }
        public int? LocationId { get; set; } //LOCATION
        public DateTime? DateOfReferral { get; set; }
        public string? Description { get; set; }
        public string? AdditionalNotes { get; set; }
        public string Status { get; set; } = "Pendiente";
        public bool IsUrgent { get; set; }
        public DateTime? AttendedDate { get; set; }
        public int? AttendedByProfessionalId { get; set; }
        public string? AttendedBy { get; set; }

        // Propiedades adicionales para la UI
        public string? PatientName { get; set; }
        public string? MedicalRecordNumber { get; set; }
        public int PatientAge { get; set; }
        public bool IsForGeneralMedicine { get; set; }
        public string? ReferringArea { get; set; }
        public string? ReferringProfessional { get; set; }

        // Nueva propiedad para mostrar información de la ubicación
        public string? LocationName { get; set; }
    }
}