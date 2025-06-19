using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public virtual ICollection<Restriction> Restrictions { get; set; } = new List<Restriction>();

        [InverseProperty("Food")]
        public virtual ICollection<RecommendedFoods> RecommendedFoods { get; set; } = new List<RecommendedFoods>();

    }
}
