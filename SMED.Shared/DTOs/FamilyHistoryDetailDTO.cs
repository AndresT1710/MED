using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class FamilyHistoryDetailDTO
    {
        public int FamilyHistoryDetailId { get; set; }

        public string MedicalRecordNumber { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime? RegistrationDate { get; set; }

        public int? appearanceAge { get; set; }

        public int? DiseaseId { get; set; }

        public int ClinicalHistoryId { get; set; }

        public int RelationshipId { get; set; }
    }
}