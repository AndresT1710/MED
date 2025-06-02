using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public partial class FamilyHistoryDetail
    {
        [Key]
        public int FamilyHistoryDetailId { get; set; }

        [StringLength(50)]
        public string MedicalRecordNumber { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }

        public int? appearanceAge { get; set; }

        public int? DiseaseId { get; set; }

        public int ClinicalHistoryId { get; set; }

        public int RelationshipId { get; set; }

        [ForeignKey("DiseaseId")]
        [InverseProperty("FamilyHistoryDetails")]
        public virtual Disease? DiseaseNavigation { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("FamilyHistoryDetails")]
        public virtual ClinicalHistory MedicalRecordNavigation { get; set; } = null!;

        [ForeignKey("RelationshipId")]
        [InverseProperty("FamilyHistoryDetails")] // ← nombre debe coincidir con la propiedad de tipo ICollection en Relationship
        public virtual Relationship? RelationshipNavigation { get; set; }
    }
}
