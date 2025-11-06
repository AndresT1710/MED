using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public partial class Advance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdvanceId { get; set; }

        [Required]
        public int SessionId { get; set; }

        [Required]
        [StringLength(200)]
        public string Task { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        // Relación con Sessions (1:Many - una sesión tiene muchos avances)
        [ForeignKey("SessionId")]
        [InverseProperty("Advances")]
        public virtual Sessions Session { get; set; } = null!;
    }
}