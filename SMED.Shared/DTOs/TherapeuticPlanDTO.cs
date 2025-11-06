using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class TherapeuticPlanDTO
    {
        public int TherapeuticPlanId { get; set; }
        public int PsychologicalDiagnosisId { get; set; }
        public string CaseSummary { get; set; } = null!;
        public string TherapeuticObjective { get; set; } = null!;
        public string StrategyApproach { get; set; } = null!;
        public string AssignedTasks { get; set; } = null!;

        // Datos relacionados para mostrar en frontend
        public string? CIE10 { get; set; }
        public string? Denomination { get; set; }
        public string? DiagnosticTypeName { get; set; }
        public string? PatientName { get; set; }
        public int? MedicalCareId { get; set; }
        public DateTime? CareDate { get; set; }
    }
}
