using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class WorkHistoryDTO
    {
        public int WorkHistoryId { get; set; }
        public string? HistoryNumber { get; set; }
        public int ClinicalHistoryId { get; set; }
        public string? Experience { get; set; }
        public string? Stability { get; set; }
        public string? SatisfactionLevel { get; set; }
    }
}