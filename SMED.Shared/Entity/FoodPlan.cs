using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class FoodPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodPlanId { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public int? RestrictionId { get; set; }

        [ForeignKey("RestrictionId")]
        public virtual Restriction? Restriction { get; set; }

        public int RecommendedFoodId { get; set; }

        [ForeignKey("RecommendedFoodId")]
        [InverseProperty("FoodPlans")]
        public virtual RecommendedFoods RecommendedFood { get; set; } = null!;
    }


}
