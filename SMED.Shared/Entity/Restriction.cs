using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public  class Restriction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RestrictionId { get; set; }
        public string Description { get; set; }
        public int FoodId { get; set; }

        [ForeignKey("FoodId")]
        [InverseProperty("Restrictions")]
        public virtual Food Food { get; set; } = null!;
        public virtual FoodPlan FoodPlan { get; set; } = null!;

    }
}
