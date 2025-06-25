using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class MedicalCareDTO
    {
        public int CareId { get; set; }
        public int LocationId { get; set; }
        public int PatientId { get; set; }
        public int HealthProfessionalId { get; set; }
    }
}
