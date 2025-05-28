using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class DiseaseType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiseaseTypeId { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [InverseProperty("DiseaseTypeNavigation")]
        public virtual ICollection<Disease> Diseases { get; set; } = new List<Disease>();
    }

}
