using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class AgentDTO
    {
        public int AgentId { get; set; }

        public int IdentificationNumber { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }

        public string? PhoneNumber { get; set; }
        public string? CellphoneNumber { get; set; }

        public int? DocumentType { get; set; }
        public string? DocumentTypeName { get; set; }

        public int? GenderId { get; set; }
        public string? GenderName { get; set; }   // opcional para mostrar nombre

        public int? MaritalStatusId { get; set; }
        public string? MaritalStatusName { get; set; } // opcional para mostrar nombre

        // mostrar también los pacientes asociados (solo datos básicos)
        public List<PatientDTO>? Patients { get; set; }
    }
}