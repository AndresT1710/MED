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
    public class LifeStyleHistory
    {
        [Key]
        public int LifeStyleHistoryId { get; set; }

        [StringLength(50)]
        public string HistoryNumber { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }


        public int ClinicalHistoryId { get; set; }
        public int? LifeStyleId { get; set; }

        [ForeignKey("LifeStyleId")]
        [InverseProperty("LifeStyleHistories")]
        public virtual LifeStyle? LifeStyleNavigation { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("LifeStyleHistories")]
        public virtual ClinicalHistory HistoryNavigation { get; set; } = null!;
    }
}
