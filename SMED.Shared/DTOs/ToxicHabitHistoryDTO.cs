using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class ToxicHabitHistoryDTO
    {
        public int ToxicHabitHistoryId { get; set; }
        public string HistoryNumber { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? RecordDate { get; set; }
        public int? ToxicHabitId { get; set; }
        public int ClinicalHistoryId { get; set; }
    }
}
