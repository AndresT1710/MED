using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class DiametersDTO
    {
        public int DiametersId { get; set; }
        public string? Humerus { get; set; }
        public string? Femur { get; set; }
        public string? Wrist { get; set; }
        public int? MeasurementsId { get; set; }
    }
}
