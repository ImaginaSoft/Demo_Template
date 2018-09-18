using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeruTourism.Models.PeruTourism
{
    public class Servicio
    {

        public string Dia { get; set; }

        public DateTime FchInicio { get; set; }
        
        public string Ciudad { get; set; }

        public string HoraServicio { get; set; } 

        public string Serv { get; set; }

        public int NroDia { get; set; }

        public int NroOrden { get; set; }

        public string KeyReg { get; set; }


    }
}