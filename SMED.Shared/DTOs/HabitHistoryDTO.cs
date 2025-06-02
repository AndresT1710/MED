using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class HabitHistoryDTO
    {
        public int HabitHistoryId { get; set; }
        public string HistoryNumber { get; set; }

        public DateTime? RecordDate { get; set; }
        public int HabitId { get; set; }

        public int ClinicalHistoryId { get; set; }

        public string? HabitName { get; set; }
    }
}
