using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SMED.Shared.Entity
{
    public class HabitHistory
    {
        [Key]
        public int HabitHistoryId { get; set; }
        public string HistoryNumber {  get; set; }

        public DateTime? RecordDate { get; set; }
        public int HabitId { get; set; }

        public int ClinicalHistoryId { get; set; }


        [ForeignKey("HabitId")]
        [InverseProperty("HabitHistories")]
        public virtual Habits Habit { get; set; } = null!;

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("HabitHistories")]
        public virtual ClinicalHistory ClinicalHistory { get; set; } = null!;
    }
}
