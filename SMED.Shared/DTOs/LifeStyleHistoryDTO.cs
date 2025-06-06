using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class LifeStyleHistoryDTO
    {
        public int LifeStyleHistoryId { get; set; }
        public string HistoryNumber { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? RegistrationDate { get; set; }
        public int ClinicalHistoryId { get; set; }
        public int? LifeStyleId { get; set; }

        public string? LifeStyleName { get; set; }
    }
}
