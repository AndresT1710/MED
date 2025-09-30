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
        public int PlaceOfAttentionId { get; set; }
        public int PatientId { get; set; }
        public int HealthProfessionalId { get; set; }
        public DateTime CareDate { get; set; }

        [ForeignKey("LocationId")]
        [InverseProperty("MedicalCares")]
        public virtual Location LocationNavigation { get; set; } = null!;

        [ForeignKey("PlaceOfAttentionId")]
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
        public virtual ICollection<MedicalReferral> MedicalReferrals { get; set; } = new List<MedicalReferral>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<MedicalService> MedicalServices { get; set; } = new List<MedicalService>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<MedicalProcedure> MedicalProcedures { get; set; } = new List<MedicalProcedure>();

        [InverseProperty("MedicalCare")]
        public virtual AdditionalData? AdditionalData { get; set; }

        [InverseProperty("MedicalCare")]
        public virtual ICollection<MedicalEvaluation> MedicalEvaluations { get; set; } = new List<MedicalEvaluation>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<OsteoarticularEvaluation> OsteoarticularEvaluations { get; set; } = new List<OsteoarticularEvaluation>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<CurrentIllness> CurrentIllnesses { get; set; } = new List<CurrentIllness>();
        [InverseProperty("MedicalCare")]
        public virtual ICollection<NeuromuscularEvaluation> NeuromuscularEvaluations { get; set; } = new List<NeuromuscularEvaluation>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<Sessions> Sessions { get; set; } = new List<Sessions>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<SensitivityEvaluation> SensitivityEvaluations { get; set; } = new List<SensitivityEvaluation>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<SkinEvaluation> SkinEvaluations { get; set; } = new List<SkinEvaluation>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<SpecialTest> SpecialTests { get; set; } = new List<SpecialTest>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<PhysiotherapyDiagnostic> PhysiotherapyDiagnostics { get; set; } = new List<PhysiotherapyDiagnostic>();

        [InverseProperty("MedicalCare")]
        public virtual ICollection<PainScale> PainScales { get; set; } = new List<PainScale>();

    }
}