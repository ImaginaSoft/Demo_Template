using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeruTourism.Models.PeruTourism
{
    public class PropuestaPrecio
    {

        //Propuesta Precio

        public string DesOrden { get; set; }
        public decimal PrecioxPersona { get; set; }
        public int CantPersonas { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}