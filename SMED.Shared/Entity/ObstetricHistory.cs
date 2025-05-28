using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class ObstetricHistory
    {
        [Key]
        public int ObstetricHistoryId { get; set; }

        [StringLength(50)]
        public string HistoryNumber { get; set; } = null!;

        public int ClinicalHistoryId { get; set; }
        public bool? CurrentPregnancy { get; set; }
        public bool? PreviousPregnancies { get; set; }
        public bool? Deliveries { get; set; }
        public bool? Abortions { get; set; }
        public bool? CSections { get; set; }

        public int? LiveBirths { get; set; }
        public int? Stillbirths { get; set; }
        public int? LivingChildren { get; set; }

        public bool? Breastfeeding { get; set; }

        public int? DiseaseId { get; set; }

        [ForeignKey("DiseaseId")]
        [InverseProperty("ObstetricHistories")]
        public virtual Disease? DiseaseNavigation { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("ObstetricHistories")]
        public virtual ClinicalHistory HistoryNavigation { get; set; } = null!;
    }
}
