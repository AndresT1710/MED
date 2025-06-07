using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class SportsActivitiesHistoryDTO
    {
        public int SportActivityHistoryId { get; set; }

        [StringLength(50)]
        public string HistoryNumber { get; set; } = null!;
        public int MinutesPerDay { get; set; }
        public int NumberOfDays { get; set; }
        public string Description { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }
        public int ClinicalHistoryId { get; set; }
        public string? SportActivityName { get; set; } = null!;
        public int? SportActivityId { get; set; }
    }
}
