using System;
using System.Collections.Generic;

namespace SMED.Shared.DTOs
{
    public class MedicalCareDTO
    {
        public int CareId { get; set; }
        public int LocationId { get; set; }
        public int PlaceOfAttentionId { get; set; }
        public string? NamePlace { get; set; }
        public int PatientId { get; set; }
        public string? NamePatient { get; set; }
        public int HealthProfessionalId { get; set; }
        public string? NameHealthProfessional { get; set; }
        public string? Area { get; set; }
        public DateTime CareDate { get; set; }

        public List<CurrentIllnessDTO>? CurrentIllnesses { get; set; }
        public List<PhysiotherapyDiagnosticDTO>? PhysiotherapyDiagnostics { get; set; }
        public List<OsteoarticularEvaluationDTO>? OsteoarticularEvaluations { get; set; }
        public List<NeuromuscularEvaluationDTO>? NeuromuscularEvaluations { get; set; }
        public List<SensitivityEvaluationDTO>? SensitivityEvaluations { get; set; }
        public List<SkinEvaluationDTO>? SkinEvaluations { get; set; }
        public List<SpecialTestDTO>? SpecialTests { get; set; }
        public List<PainScaleDTO>? PainScales { get; set; }
        public List<SessionsDTO>? Sessions { get; set; }

        public VitalSignsDTO? VitalSigns { get; set; }
        public List<MedicalServiceDTO> MedicalServices { get; set; } = new();
        public List<MedicalProcedureDTO> MedicalProcedures { get; set; } = new();
        public ReasonForConsultationDTO? ReasonForConsultation { get; set; }
    }
}
