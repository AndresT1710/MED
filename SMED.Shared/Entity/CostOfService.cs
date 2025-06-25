using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class CostOfService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CostOfServiceId { get; set; }

        [Column(TypeName = "float")]
        public float Value { get; set; }

        public int ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        [InverseProperty("CostOfServices")]
        public virtual Service Service { get; set; } = null!;

    }

}
