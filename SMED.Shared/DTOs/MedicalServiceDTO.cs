using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class MedicalServiceDTO
    {
        public int ServiceId { get; set; }
        public int? CareId { get; set; }
        public DateTime? ServiceDate { get; set; }
        public string? ServiceType { get; set; }
        public string? Diagnosis { get; set; }
        public string? Observations { get; set; }
        public string? Recommendations { get; set; }

        public int PatientId { get; set; }
        public int HealthProfessionalId { get; set; }
    }

}
