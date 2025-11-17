using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public partial class Advance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdvanceId { get; set; }

        public int? PsychologySessionId { get; set; }

        [Required]
        [StringLength(200)]
        public string Indications { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        // 🔗 Relación con PsychologySessions
        [ForeignKey("PsychologySessionId")]
        [InverseProperty("Advances")]
        public virtual PsychologySessions? PsychologySession { get; set; }
    }
}
