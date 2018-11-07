using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PeruTourism.Models.PeruTourism
{
    public class Servicio
    {

        public string Dia { get; set; }

        public int NroServicio { get; set; }
        public DateTime FchInicio { get; set; }

        public string Ciudad { get; set; }

        public string HoraServicio { get; set; }

        public string DesServicio { get; set; }

        public string NroDia { get; set; }

        public int NroOrden { get; set; }

        public string KeyReg { get; set; }

        public string DesServicioDet { get; set; }

        public int CodTipoServicio { get; set; }
        //Para servicios tipo Hotel


        public List<Galeria> ListaGaleria { get; set; }

        public byte[] Imagen1 { get; set; }
        public string flagImg01 { get; set; }
        public byte[] Imagen2 { get; set; }
        public string flagImg02 { get; set; }
        public byte[] Imagen3 { get; set; }
        public string flagImg03 { get; set; }
        public string DireccionHTL { get; set; }
        public string NombreHTL { get; set; }
        public string Telefono { get; set; }


    }
}