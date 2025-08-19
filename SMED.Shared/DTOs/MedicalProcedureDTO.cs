using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class MedicalProcedureDTO
    {
        public int ProcedureId { get; set; }
        public DateTime ProcedureDate { get; set; }
        public int SpecificProcedureId { get; set; }
        public int? CareId { get; set; }
        public int HealthProfessionalId { get; set; }
        public int PatientId { get; set; }
        public int? TreatingPhysicianId { get; set; }

        public int? LocationId { get; set; }

        public string? Observations { get; set; }
        public string Status { get; set; }

        // Propiedades de navegación para mostrar nombres
        public string? SpecificProcedureName { get; set; }
        public string? HealthProfessionalName { get; set; }
        public string? TreatingPhysicianName { get; set; }
        public string? PatientName { get; set; }
        public int? TypeOfProcedureId { get; set; }
        public string? TypeOfProcedureName { get; set; }

        public string? LocationName { get; set; }
    }
}
