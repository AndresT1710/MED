using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PainScaleDTO
    {
        public int PainScaleId { get; set; }
        public string? Observation { get; set; }
        public int? ActionId { get; set; }
        public int? ScaleId { get; set; }
        public int? PainMomentId { get; set; }
        public int? MedicalCareId { get; set; }
    }
}
