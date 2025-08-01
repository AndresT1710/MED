using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PharmacologicalTreatment : Treatment
    {
        public int Dose { get; set; }
        public string Frequency { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public string ViaAdmission { get; set; } = null!;
        public int MedicineId { get; set; }

        [ForeignKey("MedicineId")]
        [InverseProperty("PharmacologicalTreatments")]
        public virtual Medicine Medicine { get; set; } = null!;
    }
}
