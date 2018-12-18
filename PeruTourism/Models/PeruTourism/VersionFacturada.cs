using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeruTourism.Models.PeruTourism
{
    public class VersionFacturada
    {

        public int NroPropuesta { get; set; }
        public int NroVersion { get; set; }

        public char FlagIdioma { get; set; }

        public int CantDias { get; set; }

    }
}