using System;

namespace SMED.Shared.DTOs
{
    public class PersonalHistoryDTO
    {
        public int PersonalHistoryId { get; set; }
        public string MedicalRecordNumber { get; set; } = null!;

        public string Description { get; set; } = null!;
        public DateTime? RegistrationDate { get; set; }

        public int? DiseaseId { get; set; }
        public int ClinicalHistoryId { get; set; }

        // Nuevas propiedades para mostrar en la tabla
        public string? DiseaseName { get; set; }
        public string? DiseaseTypeName { get; set; }
    }
}
