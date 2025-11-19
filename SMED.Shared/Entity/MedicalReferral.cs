using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public class MedicalReferral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? DateOfReferral { get; set; }
        public string Description { get; set; } = null!;
        public string? AdditionalNotes { get; set; }
        public string? Status { get; set; } = "Pendiente";
        public bool IsUrgent { get; set; }
        public DateTime? AttendedDate { get; set; }
        public int? AttendedByProfessionalId { get; set; }

        // Relación con MedicalCare
        public int MedicalCareId { get; set; }

        // Nueva relación directa con Location
        public int? LocationId { get; set; }

        [ForeignKey("MedicalCareId")]
        [InverseProperty("MedicalReferrals")]
        public virtual MedicalCare MedicalCare { get; set; } = null!;

        [ForeignKey("AttendedByProfessionalId")]
        [InverseProperty("AttendedMedicalReferrals")]
        public virtual HealthProfessional? AttendedByProfessional { get; set; }

        // Nueva navegación a Location
        [ForeignKey("LocationId")]
        [InverseProperty("MedicalReferrals")]
        public virtual Location Location { get; set; } = null!;
    }
}