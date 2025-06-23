using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class Indications
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public int TreatmentId { get; set; }

        [ForeignKey("TreatmentId")]
        [InverseProperty("Indications")]
        public virtual Treatment Treatment { get; set; } = null!;

    }
}
