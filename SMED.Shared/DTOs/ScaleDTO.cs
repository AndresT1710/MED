using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class ScaleDTO
    {
        public int ScaleId { get; set; }
        public int Value { get; set; }
        public string? Description { get; set; }
    }
}
