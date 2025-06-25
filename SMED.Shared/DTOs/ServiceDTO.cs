using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int TypeOfServiceId { get; set; }
        public string? TypeOfServiceName { get; set; }

        public float Cost { get; set; }
    }

}
