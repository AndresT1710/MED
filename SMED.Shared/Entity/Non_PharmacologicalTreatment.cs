using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class Non_PharmacologicalTreatment : Treatment
    {
        // ✅ No agregamos propiedades adicionales, solo hereda de Treatment
        // La descripción se maneja a través de la propiedad Description de Treatment
    }
}
