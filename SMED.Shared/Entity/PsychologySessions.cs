using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public class PsychologySessions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PsychologySessionsId { get; set; }

        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public bool? MedicalDischarge { get; set; }
        public string? Observations { get; set; }
        public string? VoluntaryRegistrationLink { get; set; }
        public string? SummarySession { get; set; }

        public int? MedicalCareId { get; set; }

        // 🔗 Relación con MedicalCare (opcional y 1:N)
        [ForeignKey("MedicalCareId")]
        [InverseProperty("PsychologySessions")]
        public virtual MedicalCare? MedicalCare { get; set; }

        // 🔗 Relación con Activity (1:N)
        [InverseProperty("PsychologySession")]
        public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

        // 🔗 Relación con Advance (1:N)
        [InverseProperty("PsychologySession")]
        public virtual ICollection<Advance> Advances { get; set; } = new List<Advance>();
    }
}
