using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class SpecialTestDTO
    {
        public int SpecialTestId { get; set; }
        public string? Test { get; set; }
        public string? Observations { get; set; }
        public int ResultTypeId { get; set; }
        public int MedicalCareId { get; set; }
    }

}
