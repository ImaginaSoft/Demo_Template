using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_1.Models.PeruTourism
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


    }
}