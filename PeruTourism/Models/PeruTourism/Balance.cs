using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeruTourism.Models.PeruTourism
{
    public class Balance
    {

       public  DateTime FchEmision { get; set; }
       public string Referencia { get; set; }

       public  decimal Cargo { get; set; }

       public  string TipoOperacion { get; set; }

       public decimal Abono { get; set; }



    }
}