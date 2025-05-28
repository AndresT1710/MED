using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class PersonalHistory
    {
        [Key]
        public int PersonalHistoryId { get; set; }

        [StringLength(50)]
        public string MedicalRecordNumber { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }

        public int? DiseaseId { get; set; }

        [ForeignKey("DiseaseId")]
        [InverseProperty("PersonalHistories")]
        public virtual Disease? DiseaseNavigation { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("PersonalHistories")]
        public virtual ClinicalHistory MedicalRecordNavigation { get; set; } = null!;
    }
}
