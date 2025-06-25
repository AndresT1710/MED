using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class OrdersDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime? OrderDate { get; set; }
    }

}
