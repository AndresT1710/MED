using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class UserSessionInfo
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int? PersonId { get; set; }
        public int? HealthProfessionalTypeId { get; set; }
        public string? ProfessionalTypeName { get; set; }
        public string? RegistrationNumber { get; set; }
        public bool IsAdmin { get; set; }
        public List<string> AllowedModules { get; set; } = new();
    }
}
