using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PerimetersDTO
    {
        public int PerimetersId { get; set; }
        public string? Cephalic { get; set; }
        public string? Neck { get; set; }
        public string? RelaxedArmHalf { get; set; }
        public string? Forearm { get; set; }
        public string? Wrist { get; set; }
        public int? MeasurementsId { get; set; }
    }
}
