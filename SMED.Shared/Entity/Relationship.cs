using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public class Relationship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [InverseProperty("RelationshipNavigation")] // ← nombre debe coincidir con la propiedad de navegación en FamilyHistoryDetail
        public virtual ICollection<FamilyHistoryDetail> FamilyHistoryDetails { get; set; } = new List<FamilyHistoryDetail>();

        [InverseProperty("Relationship")] // ← Esto es para la otra relación, como PatientRelationship
        public virtual ICollection<PatientRelationship> PatientRelationships { get; set; } = new List<PatientRelationship>();
    }
}
