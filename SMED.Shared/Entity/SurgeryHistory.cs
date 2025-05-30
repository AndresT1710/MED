using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models;

namespace SMED.Shared.Entity
{
    public  class SurgeryHistory
    {
        [Key]
        public int SurgeryHistoryId { get; set; }

        [StringLength(50)]
        public string HistoryNumber { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }

        public DateTime? SurgeryDate { get; set; }

        public int ClinicalHistoryId { get; set; }
        public int? SurgeryId { get; set; }

        [ForeignKey("SurgeryId")]
        [InverseProperty("SurgeryHistories")]
        public virtual Surgery? SurgeryNavigation { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("SurgeryHistories")]
        public virtual ClinicalHistory HistoryNavigation { get; set; } = null!;
    }
}
