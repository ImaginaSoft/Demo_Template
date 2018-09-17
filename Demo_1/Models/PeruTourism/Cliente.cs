using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeruTourism.Models.PeruTourism
{

    [Serializable]
    public class Cliente
    {

        public string CodCliente { get; set; }
        public string NomCliente { get; set; }

        public string EmailCliente { get; set; }
    }
}