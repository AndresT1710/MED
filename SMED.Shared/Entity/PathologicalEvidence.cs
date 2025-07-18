using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PathologicalEvidence
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [InverseProperty("PathologicalEvidenceNavigation")]
        public virtual ICollection<PhysicalExam> PhysicalExams { get; set; } = new List<PhysicalExam>();
    }
}
