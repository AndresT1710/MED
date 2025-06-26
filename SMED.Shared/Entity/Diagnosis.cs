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
        public int MedicalCareId { get; set; }

        [ForeignKey("MedicalCareId")]
        [InverseProperty("Diagnoses")]
        public virtual MedicalCare MedicalCare { get; set; } = null!;

        public int? DiseaseId { get; set; }

        [ForeignKey("DiseaseId")]
        [InverseProperty("Diagnosis")]
        public virtual Disease? DiseaseNavigation { get; set; }

        [InverseProperty("Diagnosis")]
        public virtual ICollection<OrderDiagnosis> OrderDiagnosis { get; set; } = new List<OrderDiagnosis>();

        [InverseProperty("Diagnosis")]
        public virtual ICollection<DiagnosisTreatment> DiagnosisTreatments { get; set; } = new List<DiagnosisTreatment>();

        [InverseProperty("Diagnosis")]
        public virtual ICollection<Interconsultation> Interconsultations { get; set; } = new List<Interconsultation>();

    }

}
