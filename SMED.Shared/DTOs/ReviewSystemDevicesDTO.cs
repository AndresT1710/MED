using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class ReviewSystemDevicesDTO
    {
        public int Id { get; set; }
        public string? State { get; set; }
        public string? Observations { get; set; }
        public int SystemsDevicesId { get; set; }
        public string? SystemName { get; set; } 
        public int? MedicalCareId { get; set; }
    }
}
