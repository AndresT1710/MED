using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        public DateTime? OrderDate { get; set; }

        [InverseProperty("Order")]
        public virtual ICollection<OrderDiagnosis> OrderDiagnoses { get; set; } = new List<OrderDiagnosis>();
    }


}
