using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class DocumentType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [InverseProperty("DocumentTypeNavigation")]
        public virtual ICollection<PersonDocument> PersonDocuments { get; set; } = new List<PersonDocument>();

        [InverseProperty("DocumentTypeNavigation")]
        public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();
    }

}