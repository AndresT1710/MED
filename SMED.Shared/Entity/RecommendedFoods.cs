using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class RecommendedFoods
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecommendedFoodId { get; set; }

        [StringLength(200)]
        public string Description { get; set; } = null!;

        [StringLength(100)]
        public string Frequency { get; set; } = null!;

        public int Quantity { get; set; }

        public int FoodId { get; set; }

        [ForeignKey("FoodId")]
        [InverseProperty("RecommendedFoods")]
        public virtual Food Food { get; set; } = null!;

        [InverseProperty("RecommendedFood")]
        public virtual ICollection<FoodPlan> FoodPlans { get; set; } = new List<FoodPlan>();
    }

}
