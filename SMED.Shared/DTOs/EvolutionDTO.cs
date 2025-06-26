using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class EvolutionDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public float Percentage { get; set; }
        public int MedicalCareId { get; set; }
    }
}
