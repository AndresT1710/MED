using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class GynecologicalHistoryDTO
    {
        public int GynecologicalHistoryId { get; set; }
        public string MedicalRecordNumber { get; set; } = null!;
        public string? GynecologicalDevelopment { get; set; }
        public DateOnly? Menarche { get; set; }
        public DateOnly? Pubarche { get; set; }
        public string? MenstrualCycles { get; set; }
        public DateOnly? LastMenstruation { get; set; }
        public string? ContraceptiveMethods { get; set; }
        public int? DiseaseId { get; set; }
        public int ClinicalHistoryId { get; set; }
    }
}
