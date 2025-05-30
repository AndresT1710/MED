using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class Habits
    {
        [Key]
        public int HabitId { get; set; }
        public string Name { get; set; }

        [InverseProperty("Habit")]
        public virtual ICollection<HabitHistory> HabitHistories { get; set; } = new List<HabitHistory>();
    }
}
