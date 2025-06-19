using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class Evolution
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public float Percentage { get; set; }
        public int MedicalCareId { get; set; }

        [ForeignKey("MedicalCareId")]
        [InverseProperty("Evolutions")]
        public virtual MedicalCare MedicalCare { get; set; } = null!;
    }
}
