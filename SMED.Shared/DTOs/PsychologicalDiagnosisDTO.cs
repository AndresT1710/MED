namespace SMED.Shared.DTOs
{
    public class PsychologicalDiagnosisDTO
    {
        public int PsychologicalDiagnosisId { get; set; }
        public int MedicalCareId { get; set; }
        public string CIE10 { get; set; } = null!;
        public int DiagnosticTypeId { get; set; }
        public string Denomination { get; set; } = null!;
        
        // Datos relacionados para mostrar en frontend
        public string? DiagnosticTypeName { get; set; }
        public string? PatientName { get; set; }
        public DateTime? CareDate { get; set; }
        public string? HealthProfessionalName { get; set; }
    }
}