using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class NonPharmacologicalTreatmentDTO
    {
        public int Id { get; set; }
        public int MedicalDiagnosisId { get; set; }
        public string? Description { get; set; }
    }
}
