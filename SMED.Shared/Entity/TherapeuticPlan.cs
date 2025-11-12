using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class TherapeuticPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TherapeuticPlanId { get; set; }

        [Required]
        public int PsychologicalDiagnosisId { get; set; }

        [Required]
        [StringLength(1000)]
        public string CaseSummary { get; set; } = null!;

        [Required]
        [StringLength(1000)]
        public string TherapeuticObjective { get; set; } = null!;

        [Required]
        [StringLength(1000)]
        public string StrategyApproach { get; set; } = null!;

        public int NumberOfSessions { get; set; }

        // Relación con PsychologicalDiagnosis (1:Many - un diagnóstico psicológico tiene muchos planes terapéuticos)
        [ForeignKey("PsychologicalDiagnosisId")]
        [InverseProperty("TherapeuticPlans")]
        public virtual PsychologicalDiagnosis PsychologicalDiagnosis { get; set; } = null!;
    }
}
