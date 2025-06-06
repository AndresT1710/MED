using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class SleepHabit
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SleepHabitId { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [InverseProperty("SleepHabitNavigation")]
        public virtual ICollection<SleepHabitHistory> SleepHabitHistories { get; set; } = new List<SleepHabitHistory>();
    }
}
