using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class MeasurementsDTO
    {
        public int MeasurementsId { get; set; }
        public int? MedicalCareId { get; set; }
        public SkinFoldsDTO? SkinFolds { get; set; }
        public BioImpedanceDTO? BioImpedance { get; set; }
        public PerimetersDTO? Perimeters { get; set; }
        public DiametersDTO? Diameters { get; set; }
    }
}
