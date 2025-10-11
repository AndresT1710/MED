using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PrenatalHistoryDTO
    {
        public int PrenatalHistoryId { get; set; }
        public string? HistoryNumber { get; set; }
        public int ClinicalHistoryId { get; set; }
        public string? Description { get; set; }
        public bool? PlannedPregnancy { get; set; }
        public string? MedicationsOrVitamins { get; set; }
        public bool? RadiationExposure { get; set; }
        public int? NumberOfControls { get; set; }
        public int? NumberOfUltrasounds { get; set; }
        public bool? FetalSuffering { get; set; }
        public string? ComplicationsDuringPregnancy { get; set; }

        public int? NumberOfDeeds { get; set; }




    }
}