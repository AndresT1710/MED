using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class MedicalProcedure
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProcedureId { get; set; }

        // Fecha de procedimiento
        public DateTime ProcedureDate { get; set; }

        // Procedimiento específico (relación con tabla de procedimientos)
        public int SpecificProcedureId { get; set; }

        // Atención médica asociada
        public int? CareId { get; set; }

        // Personal de salud que realizó el procedimiento
        public int HealthProfessionalId { get; set; }

        // Paciente
        public int PatientId { get; set; }

        // Médico tratante
        public int? TreatingPhysicianId { get; set; }

        // Departamento donde se realiza el procedimiento
        public int? LocationId { get; set; }

        // Observaciones adicionales
        public string? Observations { get; set; }

        public string Status { get; set; }


        [ForeignKey("SpecificProcedureId")]
        [InverseProperty("MedicalProcedures")]
        public virtual Procedures? Procedure { get; set; }

        [ForeignKey("CareId")]
        [InverseProperty("MedicalProcedures")]
        public virtual MedicalCare? MedicalCare { get; set; }

        [ForeignKey("HealthProfessionalId")]
        [InverseProperty("MedicalProceduresAsHealthProfessional")]
        public virtual HealthProfessional? HealthProfessional { get; set; }

        [ForeignKey("PatientId")]
        [InverseProperty("MedicalProcedures")]
        public virtual Patient? Patient { get; set; }

        [ForeignKey("TreatingPhysicianId")]
        [InverseProperty("MedicalProceduresAsTreatingPhysician")]
        public virtual HealthProfessional? TreatingPhysician { get; set; }

        [ForeignKey("LocationId")]
        [InverseProperty("MedicalProcedures")]
        public virtual Location? LocationNavigation { get; set; }
    }
}
