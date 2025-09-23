using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class Medicine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float Weight { get; set; }

        [InverseProperty("Medicine")]
        public virtual ICollection<PharmacologicalTreatment> PharmacologicalTreatments { get; set; } = new List<PharmacologicalTreatment>();


        [InverseProperty("Medicine")]
        public virtual ICollection<MedicationHistory> MedicationHistories { get; set; } = new List<MedicationHistory>();
    }
}