using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PatientRelationship
    {
        [Key]
        public int PatientId { get; set; }

        public int? RelationshipId { get; set; }

        [ForeignKey("PatientId")]
        [InverseProperty("PatientRelationship")]
        public virtual Patient Patient { get; set; } = null!;

        [ForeignKey("RelationshipId")]
        [InverseProperty("PatientRelationships")]
        public virtual Relationship? Relationship { get; set; }
    }

}
