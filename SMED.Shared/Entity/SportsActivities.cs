using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class SportsActivities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SportActivityId { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [InverseProperty("SportActivityNavigation")]
        public virtual ICollection<SportsActivitiesHistory> SportsActivitiesHistories { get; set; } = new List<SportsActivitiesHistory>();




    }
}
