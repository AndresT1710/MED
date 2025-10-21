using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class RecommendedFoodsDTO
    {
        public int RecommendedFoodId { get; set; }

        public string Description { get; set; } = null!;

        public string Frequency { get; set; } = null!;

        public int Quantity { get; set; }
        public int FoodId { get; set; }

        public FoodDTO? Food { get; set; }
        public List<FoodPlanDTO>? FoodPlans { get; set; }
    }
}
