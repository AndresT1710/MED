using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PersonAddressDTO
    {
        public string? MainStreet { get; set; }
        public string? SecondaryStreet1 { get; set; }
        public string? SecondaryStreet2 { get; set; }
        public string? HouseNumber { get; set; }
        public string? Reference { get; set; }
    }
}
