using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class SkinEvaluation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SkinEvaluationId { get; set; }

        public int? MedicalCareId { get; set; }
        public int? ColorId { get; set; }
        public int? EdemaId { get; set; }
        public int? StatusId { get; set; }
        public int? SwellingId { get; set; }


        [ForeignKey("MedicalCareId")]
        [InverseProperty("SkinEvaluations")]
        public virtual MedicalCare? MedicalCare { get; set; }

        [ForeignKey("ColorId")]
        public virtual Color Color { get; set; }

        [ForeignKey("EdemaId")]
        public virtual Edema Edema { get; set; }

        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }

        [ForeignKey("SwellingId")]
        public virtual Swelling Swelling { get; set; }
    }
}
