using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class PersonAddress
    {
        [Key]
        public int PersonId { get; set; }

        [StringLength(100)]
        public string? MainStreet { get; set; }

        [StringLength(100)]
        public string? SecondaryStreet1 { get; set; }

        [StringLength(100)]
        public string? SecondaryStreet2 { get; set; }

        [StringLength(50)]
        public string? HouseNumber { get; set; }

        [StringLength(100)]
        public string? Reference { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("PersonAddress")]
        public virtual Person PersonNavigation { get; set; } = null!;
    }

}
