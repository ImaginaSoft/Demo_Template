using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeruTourism.Models.PeruTourism
{
    public class ReservaAereo
    {

        public DateTime FchVuelo { get; set; }
        public string AereoLinea { get; set; }
        public string RutaVuelo { get; set; }

        public string NroVuelo { get; set; }

        public string HoraSalida { get; set; }

        public string HoraLlegada { get; set; }

        public string CodReserva { get; set; }

        public string DesCantidad { get; set; }

        public string CodStsReserva { get; set; }    

        public int NroDia { get; set; }

        public int NroOrden { get; set; }

    }
}