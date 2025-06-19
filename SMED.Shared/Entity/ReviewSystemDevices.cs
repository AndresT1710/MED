using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class ReviewSystemDevices
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string State { get; set; } = null!;
        public string Observations { get; set; } = null!;

        public int SystemsDevicesId { get; set; }

        [ForeignKey("SystemsDevicesId")]
        [InverseProperty("Review")]
        public virtual SystemsDevices SystemsDevices { get; set; } = null!;
    }

}
