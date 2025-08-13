using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class Treatment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Recommendations { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public int MedicalDiagnosisId { get; set; }

        [ForeignKey("MedicalDiagnosisId")]
        [InverseProperty("Treatments")]
        public virtual MedicalDiagnosis MedicalDiagnosis { get; set; } = null!;

        [InverseProperty("Treatment")]
        public virtual ICollection<Indications> Indications { get; set; } = new List<Indications>();
    }
}
