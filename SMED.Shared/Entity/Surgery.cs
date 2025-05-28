using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMED.Shared.Entity;

namespace Infrastructure.Models
{
    public partial class Surgery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SurgeryId { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [InverseProperty("SurgeryNavigation")]
        public virtual ICollection<SurgeryHistory> SurgeryHistories { get; set; } = new List<SurgeryHistory>();
    }
}
