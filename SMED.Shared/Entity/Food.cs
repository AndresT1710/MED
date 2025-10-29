using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public partial class Food
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodId { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [InverseProperty("FoodNavigation")]
        public virtual ICollection<FoodIntoleranceHistory> FoodIntoleranceHistories { get; set; } = new List<FoodIntoleranceHistory>();

        [InverseProperty("FoodNavigation")]
        public virtual ICollection<FoodConsumptionHistory> FoodConsumptionHistories { get; set; } = new List<FoodConsumptionHistory>();

        [InverseProperty("Food")]
        public virtual ICollection<FoodPlan> FoodPlans { get; set; } = new List<FoodPlan>();

        [InverseProperty("Food")]
        public virtual ICollection<ForbiddenFood> ForbiddenFoods { get; set; } = new List<ForbiddenFood>();
    }
}
