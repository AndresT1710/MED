using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class DiagnosticType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [InverseProperty("DiagnosticTypeNavigation")]
        public virtual ICollection<MedicalDiagnosis> Diagnoses { get; set; } = new List<MedicalDiagnosis>();
    }

}
