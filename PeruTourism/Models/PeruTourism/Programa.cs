using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeruTourism.Models.PeruTourism
{
    [Serializable]
    public class Programa
    {
        public DateTime FchSys { get; set; }
        public string NroPrograma { get; set; }

        public string StsPrograma { get; set; }

        public string DesPrograma { get; set; }

        public int CantDias { get; set; }

        public string KeyReg { get; set; }

        public string Resumen { get; set; }

        public string ResumenComida { get; set; }


        //Propuesta Precio

        public string DesOrden { get; set; }
        public decimal PrecioxPersona { get; set; }
        public int CantPersonas { get; set; }
        public decimal PrecioTotal { get; set; }
        

    }
}