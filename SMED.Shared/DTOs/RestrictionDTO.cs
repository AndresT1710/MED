using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class RestrictionDTO
    {
        public int RestrictionId { get; set; }
        public string Description { get; set; } = null!;
        public int FoodId { get; set; }

        public FoodDTO? Food { get; set; }
        public FoodPlanDTO? FoodPlan { get; set; }
    }
}
