using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int TypeOfServiceId { get; set; }

        [ForeignKey("TypeOfServiceId")]
        public virtual TypeOfService TypeOfService { get; set; } = null!;

        // 🔗 Relación uno a uno con CostOfService
        public virtual CostOfService CostOfService { get; set; } = null!;

        [InverseProperty("Service")]
        public virtual ICollection<Interconsultation> Interconsultations { get; set; } = new List<Interconsultation>();
    }
}
