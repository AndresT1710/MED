using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class MentalFunctionsPsychologyDTO
    {
        public int MentalFunctionsPsychologyId { get; set; }
        public int? MedicalCareId { get; set; }
        public int? MentalFunctionId { get; set; }
        public string? Description { get; set; }
    }
}
