using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PharmacologicalTreatmentDTO : TreatmentDTO
    {
        public int Dose { get; set; }
        public string? Frequency { get; set; }
        public string? Duration { get; set; }
        public string? ViaAdmission { get; set; }
        public int MedicineId { get; set; }
    }

}
