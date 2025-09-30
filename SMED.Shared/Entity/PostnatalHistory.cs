using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PostnatalHistory
    {
        [Key]
        public int PostNatalId { get; set; }

        [StringLength(100)]
        public string HistoryNumber { get; set; }

        public int ClinicalHistoryId { get; set; }

        public string? Description { get; set; }

        public bool? Bcg { get; set; }

        public bool? Rotavirus { get; set; }

        public bool? Pentavalente { get; set; }

        public bool? Influenza { get; set; }

        public bool? Varicela { get; set; }

        public bool? HepatitisB { get; set; }

        public bool? TripleViral { get; set; }

        public bool? PolioVirus { get; set; }

        public bool? Neumococo { get; set; }

        public string? Observations { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("PostnatalHistories")]
        public virtual ClinicalHistory ClinicalHistory { get; set; } = null!;
    }
}