using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeruTourism.Models.PeruTourism
{

    public enum TipoRespuesta
    {
        Exito = 0,
        Alerta = 1,
        Error = 2
    }
    public class RespuestaEmail
    {
        public TipoRespuesta Tipo { set; get; }
        public string Valor { set; get; }
        public string OtroValor { set; get; }

    }
}