using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class DevelopmentRecordDTO
    {
        public int DevelopmentRecordId { get; set; }
        public string HistoryNumber { get; set; }
        public int ClinicalHistoryId { get; set; }
        public string? DevelopmentMilestone { get; set; }
        public string? AgeRange { get; set; }
        public string? Observations { get; set; }
    }
}