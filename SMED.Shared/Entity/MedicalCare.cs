using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class MedicalCare
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CareId { get; set; }
        public int LocationId { get; set; }
        public int PatientId { get; set; }
        public int HealthProfessionalId { get; set; }


        [ForeignKey("LocationId")]
        [InverseProperty("MedicalCares")]
        public virtual PlaceOfAttention PlaceOfAttentionNavigation { get; set; } = null!;

        [InverseProperty("MedicalCare")]
        public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();

        [InverseProperty("MedicalCare")]
        public virtual VitalSigns? VitalSigns { get; set; }

        [InverseProperty("MedicalCare")]
        public virtual ICollection<Evolution> Evolutions { get; set; } = new List<Evolution>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<ReasonForConsultation> ReasonsForConsultation { get; set; } = new List<ReasonForConsultation>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<Procedures> Procedures { get; set; } = new List<Procedures>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<ExamResults> ExamResults { get; set; } = new List<ExamResults>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<IdentifiedDisease> IdentifiedDiseases { get; set; } = new List<IdentifiedDisease>();

        [InverseProperty("MedicalCare")]
        public virtual PhysicalExam? PhysicalExam { get; set; }

        [InverseProperty("MedicalCare")]
        public virtual ReviewSystemDevices? ReviewSystemDevices { get; set; }


        [ForeignKey("HealthProfessionalId")]
        [InverseProperty("MedicalCares")]
        public virtual HealthProfessional HealthProfessional { get; set; } = null!;


        [ForeignKey("PatientId")]
        [InverseProperty("MedicalCares")]
        public virtual Patient Patient { get; set; } = null!;

        [InverseProperty("MedicalCare")]
        public virtual MedicalReferral? MedicalReferral { get; set; }

    }
}
