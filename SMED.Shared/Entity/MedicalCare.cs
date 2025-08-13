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
        public string Area { get; set; }
        public DateTime CareDate { get; set; }

        [ForeignKey("LocationId")]
        [InverseProperty("MedicalCares")]
        public virtual PlaceOfAttention PlaceOfAttentionNavigation { get; set; } = null!;

        [InverseProperty("MedicalCare")]
        public virtual ICollection<MedicalDiagnosis> Diagnoses { get; set; } = new List<MedicalDiagnosis>();

        [InverseProperty("MedicalCare")]
        public virtual VitalSigns? VitalSigns { get; set; }

        [InverseProperty("MedicalCare")]
        public virtual ICollection<Evolution> Evolutions { get; set; } = new List<Evolution>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<ReasonForConsultation> ReasonsForConsultation { get; set; } = new List<ReasonForConsultation>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<ExamResults> ExamResults { get; set; } = new List<ExamResults>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<IdentifiedDisease> IdentifiedDiseases { get; set; } = new List<IdentifiedDisease>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<PhysicalExam> PhysicalExams { get; set; } = new List<PhysicalExam>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<ReviewSystemDevices> ReviewSystemDevices { get; set; } = new List<ReviewSystemDevices>();

        [ForeignKey("HealthProfessionalId")]
        [InverseProperty("MedicalCares")]
        public virtual HealthProfessional HealthProfessional { get; set; } = null!;

        [ForeignKey("PatientId")]
        [InverseProperty("MedicalCares")]
        public virtual Patient Patient { get; set; } = null!;

        [InverseProperty("MedicalCare")]
        public virtual MedicalReferral? MedicalReferral { get; set; }

        [InverseProperty("MedicalCare")]
        public virtual ICollection<MedicalService> MedicalServices { get; set; } = new List<MedicalService>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<MedicalProcedure> MedicalProcedures { get; set; } = new List<MedicalProcedure>();

        [InverseProperty("MedicalCare")]
        public virtual AdditionalData? AdditionalData { get; set; }
    }
}
