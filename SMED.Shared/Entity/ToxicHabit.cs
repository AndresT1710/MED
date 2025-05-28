using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class ToxicHabit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ToxicHabitId { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [InverseProperty("ToxicHabit")]
        public virtual ICollection<ToxicHabitBackground> ToxicHabitBackgrounds { get; set; } = new List<ToxicHabitBackground>();
    }

}
