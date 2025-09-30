using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class SensitivityEvaluation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SensitivityEvaluationId { get; set; }

        public string Demandmas { get; set; }

        public int? SensitivityLevelId { get; set; }
        public int? BodyZoneId { get; set; }

        // CLAVE FORÁNEA - nombre correcto
        public int MedicalCareId { get; set; }

        // RELACIONES CORREGIDAS:
        [ForeignKey("SensitivityLevelId")]
        public virtual SensitivityLevel? SensitivityLevel { get; set; }

        [ForeignKey("BodyZoneId")]
        public virtual BodyZone? BodyZone { get; set; }

        [ForeignKey("MedicalCareId")] // ✅ CORREGIDO: coincide con la propiedad
        [InverseProperty("SensitivityEvaluations")]
        public virtual MedicalCare MedicalCare { get; set; } = null!;
    }
}