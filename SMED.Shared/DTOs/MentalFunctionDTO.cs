using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class MentalFunctionDTO
    {
        public int MentalFunctionId { get; set; }
        public string FunctionName { get; set; }
        public int TypeOfMentalFunctionId { get; set; }
        public string TypeOfMentalFunctionName { get; set; }
    }
}
