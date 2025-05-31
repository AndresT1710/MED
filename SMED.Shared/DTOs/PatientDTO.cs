using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PatientDTO
    {
        public int PersonId { get; set; }
        public PersonDTO Person { get; set; }

    }
}
