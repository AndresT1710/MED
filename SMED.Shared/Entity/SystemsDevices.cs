﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class SystemsDevices
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        // Cambiamos el nombre de la propiedad para que coincida con el InverseProperty
        [InverseProperty("SystemsDevices")]
        public virtual ICollection<ReviewSystemDevices> Reviews { get; set; } = new List<ReviewSystemDevices>();
    }
}