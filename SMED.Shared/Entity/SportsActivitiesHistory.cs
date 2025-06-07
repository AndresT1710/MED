using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class SportsActivitiesHistory
    {
        [Key]
        public int SportActivityHistoryId { get; set; }

        [StringLength(50)]
        public string HistoryNumber { get; set; } = null!;

        public string Description { get; set; } = null!;
        public int MinutesPerDay { get; set; }
        public int NumberOfDays { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }
        public int ClinicalHistoryId { get; set; }
        public int? SportActivityId { get; set; }

        [ForeignKey("SportActivityId")]
        [InverseProperty("SportsActivitiesHistories")]
        public virtual SportsActivities? SportActivityNavigation { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("SportsActivitiesHistories")]
        public virtual ClinicalHistory HistoryNavigation { get; set; } = null!;

    }
}
