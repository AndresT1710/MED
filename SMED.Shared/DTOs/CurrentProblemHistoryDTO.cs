using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class CurrentProblemHistoryDTO
    {
        public int CurrentProblemHistoryId { get; set; }
        public string HistoryNumber { get; set; }
        public int ClinicalHistoryId { get; set; }
        public string? AppearanceEvolution { get; set; }
        public string? TriggeringFactors { get; set; }
        public string? FrequencyIntensitySymptoms { get; set; }
        public string? Impact { get; set; }
    }
}