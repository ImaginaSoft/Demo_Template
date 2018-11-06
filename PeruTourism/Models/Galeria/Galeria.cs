using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeruTourism.Models.Galeria
{

    [Serializable]
    public class Galeria
    {

        //public string CodCliente { get; set; }
        public byte Imagen1 { get; set; }
        public string flagImg01 { get; set; }
        public byte Imagen2 { get; set; }
        public string flagImg02 { get; set; }
        public byte Imagen3 { get; set; }
        public string flagImg03 { get; set; }
        public string NroServicio { get; set; }
        public string DireccionHTL { get; set; }
        public string Telefono { get; set; }

    }
}