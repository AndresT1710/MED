using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class ForbiddenFoodDTO
    {
        public int ForbiddenFoodId { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string? Description { get; set; }
        public int FoodId { get; set; }
        public int CareId { get; set; }
    }
}
