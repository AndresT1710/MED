using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class ObstetricHistoryDTO
    {
        public int ObstetricHistoryId { get; set; }
        public string HistoryNumber { get; set; } = null!;
        public int ClinicalHistoryId { get; set; }
        public bool? CurrentPregnancy { get; set; }
        public bool? PreviousPregnancies { get; set; }
        public bool? Deliveries { get; set; }
        public bool? Abortions { get; set; }
        public bool? CSections { get; set; }
        public int? LiveBirths { get; set; }
        public int? Stillbirths { get; set; }
        public int? LivingChildren { get; set; }
        public bool? Breastfeeding { get; set; }
        public int? DiseaseId { get; set; }
    }
}
