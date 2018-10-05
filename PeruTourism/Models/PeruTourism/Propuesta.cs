using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeruTourism.Models.PeruTourism
{
    [Serializable]
    public class Propuesta
    {

        public int NroPedido { get; set; }

        public int NroPropuesta { get; set; }

        public string DesPropuesta { get; set; }

        public int NroDia { get; set; }

        public int NroOrden { get; set; }

        public int NroServicio { get; set; }

        public string DesServicio { get; set; }

        public string DesServicioDet { get; set; }
    }
}