using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class HealthProfessionalDTO
    {
        public int HealthProfessionalId { get; set; }

        public int? HealthProfessionalTypeId { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? NameTypeProfessional { get; set; }
    }
}
