using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class SleepHabitHistory
    {
        [Key]
        public int HabitSleepHistoryId { get; set; }
        public string HistoryNumber { get; set; }

        public DateTime? RecordDate { get; set; }
        public int SleepHabitId { get; set; }

        public int ClinicalHistoryId { get; set; }


        [ForeignKey("SleepHabitId")]
        [InverseProperty("SleepHabitHistories")]
        public virtual Habits Habit { get; set; } = null!;

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("SleepHabitHistories")]
        public virtual ClinicalHistory HistoryNavigation { get; set; } = null!;



    }
}
