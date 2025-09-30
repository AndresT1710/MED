using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PerinatalHistoryDTO
    {
        public int PerinatalHistoryId { get; set; }
        public string? HistoryNumber { get; set; }
        public int ClinicalHistoryId { get; set; }
        public string? Description { get; set; }
        public string? TypeOfBirth { get; set; }
        public string? Apgar { get; set; }
        public string? AuditoryScreen { get; set; }
        public string? ResuscitationManeuvers { get; set; }
        public string? PlaceOfCare { get; set; }
        public decimal? NumberOfWeeks { get; set; }
        public bool? BirthCry { get; set; }
        public string? MetabolicScreen { get; set; }
        public string? ComplicationsDuringChildbirth { get; set; }


    }
}