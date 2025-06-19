using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PlaceOfAttention
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [InverseProperty("PlaceOfAttentionNavigation")]
        public virtual ICollection<MedicalCare> MedicalCares { get; set; } = new List<MedicalCare>();
    }
}
