using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class DiagnosisTreatmentDTO
    {
        public int Id { get; set; }
        public int DiagnosisId { get; set; }
        public int TreatmentId { get; set; }
    }

}
