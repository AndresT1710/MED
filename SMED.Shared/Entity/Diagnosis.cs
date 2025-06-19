using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class Diagnosis
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Cie10 { get; set; } = null!;
        public string Denomination { get; set; } = null!;
        public string DiagnosticType { get; set; } = null!;
        public string Recurrence { get; set; } = null!;
        public string DiagnosisMotivation { get; set; } = null!;

        [InverseProperty("Diagnosis")]
        public virtual ICollection<OrderDiagnosis> OrderDiagnoses { get; set; } = new List<OrderDiagnosis>();

        public int MedicalCareId { get; set; }

        [ForeignKey("MedicalCareId")]
        [InverseProperty("Diagnoses")]
        public virtual MedicalCare MedicalCare { get; set; } = null!;

    }

}
