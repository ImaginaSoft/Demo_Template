using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeruTourism.Models.PeruTourism
{
    public class PropuestaViewModel
    {
        public List<Programa> lstPrograma { get; set; }
        public List<Servicio> lstServicio { get; set; }
        public List<PropuestaPrecio> lstPropuestaPrecio { get; set; }
        public List<Balance> lstBalance { get; set; }

        //*****AAAAAAAAAA***//
        public Pasajero Pasajero { get; set; }
        public List<Pasajero> lstPasajero { get; set; }
        public List<ReservaAereo> lstReservaAerero { get; set; }
        public List<ReservaAereo> lstReservaTerrestre { get; set; }
        public List<ReservaHotel> lstReservaHotel { get; set; }
  
        //*****AAAAAAAAAA***//

        public List<Staff> lstStaff { get; set; }
        public List<Video> lstVideo { get; set; }
        public List<Clima> lstClima { get; set; }
        public string info { get; set; }
        public char idioma { get; set; }

    }
}