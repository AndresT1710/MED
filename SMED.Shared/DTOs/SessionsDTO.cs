using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class SessionsDTO
    {
        public int SessionsId { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public string? Treatment { get; set; }
        public bool? MedicalDischarge { get; set; }
        public string Observations { get; set; }
        public int? MedicalCareId { get; set; }
    }

}
