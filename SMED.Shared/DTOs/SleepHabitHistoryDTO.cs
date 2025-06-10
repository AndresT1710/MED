using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class SleepHabitHistoryDTO
    {
        public int HabitSleepHistoryId { get; set; }
        public string HistoryNumber { get; set; } = null!;
        public DateTime? RecordDate { get; set; }
        public int SleepHabitId { get; set; }
        public string Description { get; set; } = null!;
        public int ClinicalHistoryId { get; set; }

        public string? SleepHabitName { get; set; }
    }
}
