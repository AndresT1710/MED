using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class MedicalReferralDTO
    {
        public int Id { get; set; }
        public DateTime? DateOfReferral { get; set; }
        public string? Description { get; set; }
        public int MedicalCareId { get; set; }
    }
}
