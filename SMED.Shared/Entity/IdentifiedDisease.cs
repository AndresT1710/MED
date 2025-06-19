using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class IdentifiedDisease
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Description { get; set; } = null!;
        public int DiseaseId { get; set; }

        [ForeignKey("DiseaseId")]
        [InverseProperty("IdentifiedDiseases")]
        public virtual Disease DiseaseNavigation { get; set; } = null!;
    }
}
