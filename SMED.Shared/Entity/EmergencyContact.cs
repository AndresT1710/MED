using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class EmergencyContact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PatientId { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(10)]
        public string? Phone { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [ForeignKey("PatientId")]
        [InverseProperty("EmergencyContacts")]
        public virtual Patient Patient { get; set; } = null!;
    }

}
