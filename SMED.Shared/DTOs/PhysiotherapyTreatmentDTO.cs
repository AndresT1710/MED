using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PhysiotherapyTreatmentDTO
    {
        public int PhysiotherapyTreatmentId { get; set; }
        public string Recommendations { get; set; }

        public int? PhysiotherapyDiagnosticId { get; set; }
    }
}
