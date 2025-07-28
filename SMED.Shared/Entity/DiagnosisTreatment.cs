using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class DiagnosisTreatment
    {
        [Key]
        public int Id { get; set; }

        // FK a Diagnosis
        public int DiagnosisId { get; set; }

        [ForeignKey("DiagnosisId")]
        [InverseProperty("DiagnosisTreatments")]
        public virtual MedicalDiagnosis Diagnosis { get; set; } = null!;

        // FK a Treatment
        public int TreatmentId { get; set; }

        [ForeignKey("TreatmentId")]
        [InverseProperty("DiagnosisTreatments")]
        public virtual Treatment Treatment { get; set; } = null!;
    }
}
