using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMED.Shared.Entity;

namespace Infrastructure.Models
{
    public partial class AllergyHistory
    {
        [Key]
        public int AllergyHistoryId { get; set; }

        [StringLength(50)]
        public string HistoryNumber { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }

        public int? AllergyId { get; set; }

        public int ClinicalHistoryId { get; set; }

        [ForeignKey("AllergyId")]
        [InverseProperty("AllergyHistories")]
        public virtual Allergy? AllergyNavigation { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("AllergyHistories")]
        public virtual ClinicalHistory HistoryNavigation { get; set; } = null!;
    }
}
