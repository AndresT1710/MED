using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class NeuropsychologicalHistoryDTO
    {
        public int NeuropsychologicalHistoryId { get; set; }
        public string? HistoryNumber { get; set; }
        public int? ClinicalHistoryId { get; set; }
        public string? Description { get; set; }
        public string? HomeConduct { get; set; }
        public string? SchoolConduct { get; set; }
        public string? Leverage { get; set; }
        public string? HearingObservation { get; set; }
        public string? SightObservation { get; set; }
        public string? SpeechObservation { get; set; }
        public string? DreamObservation { get; set; }
        public bool? Faintings { get; set; }
        public string? ObservationDifferentAbility { get; set; }
        public string? Observation { get; set; }
    }
}