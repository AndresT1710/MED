using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PsychopsychiatricHistoryDTO
    {
        public int PsychopsychiatricHistoryId { get; set; }
        public string? HistoryNumber { get; set; }
        public int ClinicalHistoryId { get; set; }
        public string? Type { get; set; }
        public string? Actor { get; set; }
        public DateTime? HistoryDate { get; set; }
        public string? HistoryState { get; set; }
    }
}