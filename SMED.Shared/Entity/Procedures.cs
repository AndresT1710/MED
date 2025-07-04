﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class Procedures
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public int TypeOfProcedureId { get; set; }

        [ForeignKey("TypeOfProcedureId")]
        [InverseProperty("Procedures")]
        public virtual TypeOfProcedures TypeOfProcedure { get; set; } = null!;

        [InverseProperty("Procedure")]
        public virtual ICollection<MedicalProcedure> MedicalProcedures { get; set; } = new List<MedicalProcedure>();
    }
}
