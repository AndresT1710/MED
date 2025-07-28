using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class OrderDiagnosis
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }
        public int DiagnosisId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Orders Order { get; set; } = null!;

        [ForeignKey("DiagnosisId")]
        public virtual MedicalDiagnosis Diagnosis { get; set; } = null!;
    }

}
