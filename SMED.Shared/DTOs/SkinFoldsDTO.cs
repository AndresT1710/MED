using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
        public class SkinFoldsDTO
        {
            public int SkinFoldsId { get; set; }
            public string? Subscapular { get; set; }
            public string? Triceps { get; set; }
            public string? Biceps { get; set; }
            public string? IliacCrest { get; set; }
            public string? Supraespinal { get; set; }
            public string? Abdominal { get; set; }
            public string? FrontalThigh { get; set; }
            public string? MedialCalf { get; set; }
            public string? MedialAxillary { get; set; }
            public string? Pectoral { get; set; }
            public int? MeasurementsId { get; set; }
        }
}
