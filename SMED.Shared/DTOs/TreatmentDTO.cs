using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class TreatmentDTO
    {
        public int Id { get; set; }
        public string? Recommendations { get; set; }
        public string? Description { get; set; } = null!;
        public int MedicalCareId { get; set; }

    }
}
