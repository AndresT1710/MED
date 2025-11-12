using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public partial class PsychologicalDiagnosis
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PsychologicalDiagnosisId { get; set; }

        public int MedicalCareId { get; set; }

        [Required]
        [StringLength(10)]
        public string CIE10 { get; set; } = null!;

        [Required]
        public int DiagnosticTypeId { get; set; }

        [Required]
        [StringLength(500)]
        public string Denomination { get; set; } = null!;

        public string DiagnosisMotivation { get; set; } = null!;

        public string Differential { get; set; } = null!;

        // 🔗 Relación con MedicalCare (N:1) - UN diagnóstico pertenece a UNA atención médica
        [ForeignKey("MedicalCareId")]
        [InverseProperty("PsychologicalDiagnoses")] // ← Apunta a la colección en MedicalCare
        public virtual MedicalCare MedicalCare { get; set; } = null!;

        // 🔗 Relación con DiagnosticType
        [ForeignKey("DiagnosticTypeId")]
        [InverseProperty("PsychologicalDiagnoses")]
        public virtual DiagnosticType DiagnosticTypeNavigation { get; set; } = null!;

        [InverseProperty("PsychologicalDiagnosis")]
        public virtual ICollection<TherapeuticPlan> TherapeuticPlans { get; set; } = new List<TherapeuticPlan>();
    }
}