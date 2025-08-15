using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PharmacologicalTreatmentDTO
    {
        public int Id { get; set; }
        public int MedicalDiagnosisId { get; set; }
        public string? Description { get; set; }
        public int Dose { get; set; }
        public string Frequency { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public string ViaAdmission { get; set; } = null!;
        public int MedicineId { get; set; }
        public string? MedicineName { get; set; }
    }
}
