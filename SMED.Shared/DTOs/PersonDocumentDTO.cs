using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PersonDocumentDTO
    {
        public int PersonId { get; set; }

        public int? DocumentTypeId { get; set; }

        public string? DocumentNumber { get; set; }

        public string? DocumentTypeName { get; set; }
    }
}
