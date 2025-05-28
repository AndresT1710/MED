using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class PersonDocument
    {
        [Key]
        public int PersonId { get; set; }

        public int? DocumentTypeId { get; set; }

        [StringLength(10)]
        public string? DocumentNumber { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("PersonDocument")]
        public virtual Person PersonNavigation { get; set; } = null!;

        [ForeignKey("DocumentTypeId")]
        [InverseProperty("PersonDocuments")]
        public virtual DocumentType? DocumentTypeNavigation { get; set; }
    }

}
