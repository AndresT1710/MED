using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? ProvinceId { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [InverseProperty("CityNavigation")]
        public virtual ICollection<PersonResidence> PersonResidences { get; set; } = new List<PersonResidence>();

        [ForeignKey("ProvinceId")]
        [InverseProperty("Cities")]
        public virtual Province? ProvinceNavigation { get; set; }
    }

}
