using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class CostOfServiceDTO
    {
        public int Id { get; set; }
        public float Value { get; set; }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
    }

}
