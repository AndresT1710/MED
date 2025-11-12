using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public class Activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityId { get; set; }

        public string? NameActivity { get; set; }
        public DateTime? DateActivity { get; set; }

        public int? SessionId { get; set; }
        public int? PsychologySessionId { get; set; }

        // 🔗 Relación con Sessions
        [ForeignKey("SessionId")]
        [InverseProperty("Activities")]
        public virtual Sessions? Session { get; set; }

        // 🔗 Relación con PsychologySessions
        [ForeignKey("PsychologySessionId")]
        [InverseProperty("Activities")]
        public virtual PsychologySessions? PsychologySession { get; set; }

        public int? TypeOfActivityId { get; set; }

        [ForeignKey("TypeOfActivityId")]
        [InverseProperty("Activities")]
        public virtual TypeOfActivity? TypeOfActivity { get; set; }
    }
}
