using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class MedicalDiagnosisDTO
    {
        public int Id { get; set; }
        public string Cie10 { get; set; } = null!;
        public string Denomination { get; set; } = null!;
        public int DiagnosticTypeId { get; set; }
        public string Recurrence { get; set; } = null!;
        public string DiagnosisMotivation { get; set; } = null!;
        public int MedicalCareId { get; set; }
        public int? DiseaseId { get; set; }

        public string? DiseaseName { get; set; }
        public List<int>? OrderIds { get; set; }
        public List<int>? TreatmentIds { get; set; }
        public List<int>? InterconsultationIds { get; set; }
    }

}
