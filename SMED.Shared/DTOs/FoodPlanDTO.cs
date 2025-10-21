using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class FoodPlanDTO
    {
        public int FoodPlanId { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public int? RestrictionId { get; set; }
        public int RecommendedFoodId { get; set; }
        public int CareId { get; set; }

        // Propiedades de navegación si las necesitas
        public RestrictionDTO? Restriction { get; set; }
        public RecommendedFoodsDTO? RecommendedFood { get; set; }
        public MedicalCareDTO? MedicalCare { get; set; }
    }
}
