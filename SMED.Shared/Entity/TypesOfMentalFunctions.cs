using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public partial class TypesOfMentalFunctions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TypeOfMentalFunctionId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = null!;

        [InverseProperty("TypeOfMentalFunction")]
        public virtual ICollection<MentalFunction> MentalFunctions { get; set; } = new List<MentalFunction>();
    }
}
