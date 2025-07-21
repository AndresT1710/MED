using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class AdditionalData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdditionalDataId { get; set; }

        [Required]
        public string Observacion { get; set; } = null!;

        public int MedicalCareId { get; set; }

        [ForeignKey("MedicalCareId")]
        [InverseProperty("AdditionalData")]
        public virtual MedicalCare MedicalCare { get; set; } = null!;
    }
}
