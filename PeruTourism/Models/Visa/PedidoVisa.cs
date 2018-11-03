using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeruTourism.Models.Visa
{
    public class PedidoVisa
    {

        public string NomCliente { get; set; }
        public string DesPedido { get; set; }

        public string Idioma { get; set; }

        public string IDPedido { get; set; }

        public int Monto { get; set; }
    }
}