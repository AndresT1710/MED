using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class AllergyHistoryDTO
    {
        public int AllergyHistoryId { get; set; }
        public string HistoryNumber { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? RegistrationDate { get; set; }
        public int? AllergyId { get; set; }
        public int ClinicalHistoryId { get; set; }

        public string? AllergyName { get; set; }
        public string? ClinicalHistoryNumber { get; set; }
    }
}
