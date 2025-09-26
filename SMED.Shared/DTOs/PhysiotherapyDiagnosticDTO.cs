using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PhysiotherapyDiagnosticDTO
    {
        public int PhysiotherapyDiagnosticId { get; set; }
        public string? Description { get; set; }
        public int? MedicalCareId { get; set; }
    }
}
