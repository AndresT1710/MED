using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class ToxicHabitBackground
    {
        [Key]
        public int ToxicHabitBackgroundId { get; set; }

        [StringLength(50)]
        public string HistoryNumber { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime? RecordDate { get; set; }

        public int? ToxicHabitId { get; set; }

        public int ClinicalHistoryId { get; set; }

        [ForeignKey("ToxicHabitId")]
        [InverseProperty("ToxicHabitBackgrounds")]
        public virtual ToxicHabit? ToxicHabit { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("ToxicHabitBackgrounds")]
        public virtual ClinicalHistory ClinicalHistory { get; set; } = null!;
    }

}
