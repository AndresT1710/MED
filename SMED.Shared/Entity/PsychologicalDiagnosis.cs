using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public partial class PsychologicalDiagnosis
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PsychologicalDiagnosisId { get; set; }

        [Required]
        public int MedicalCareId { get; set; }

        [Required]
        [StringLength(10)]
        public string CIE10 { get; set; } = null!;

        [Required]
        public int DiagnosticTypeId { get; set; }

        [Required]
        [StringLength(500)]
        public string Denomination { get; set; } = null!;

        // Relación con MedicalCare (1:1 - una atención médica tiene un diagnóstico psicológico)
        [ForeignKey("MedicalCareId")]
        [InverseProperty("PsychologicalDiagnosis")]
        public virtual MedicalCare MedicalCare { get; set; } = null!;

        // Relación con DiagnosticType
        [ForeignKey("DiagnosticTypeId")]
        [InverseProperty("PsychologicalDiagnoses")]
        public virtual DiagnosticType DiagnosticTypeNavigation { get; set; } = null!;

        [InverseProperty("PsychologicalDiagnosis")]
        public virtual ICollection<TherapeuticPlan> TherapeuticPlans { get; set; } = new List<TherapeuticPlan>();
    }
}