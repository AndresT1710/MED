using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class BioImpedanceDTO
    {
        public int BioImpedanceId { get; set; }
        public string? BodyFatPercentage { get; set; }
        public string? UpperSectionFatPercentage { get; set; }
        public string? LowerSectionFatPercentage { get; set; }
        public string? VisceralFat { get; set; }
        public string? MuscleMass { get; set; }
        public string? BoneWeight { get; set; }
        public string? BodyWater { get; set; }
        public int? MeasurementsId { get; set; }
    }
}
