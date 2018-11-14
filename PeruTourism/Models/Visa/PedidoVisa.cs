using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PeruTourism.Models.Visa
{
    public class PedidoVisa
    {

        public string NomCliente { get; set; }
        public string DesPedido { get; set; }

        public string Idioma { get; set; }

        public string IDPedido { get; set; }

        public int Monto { get; set; }

        [Display(Name = "Terms and Conditions")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Falta aceptar su conformidad con las politicas de devolución.")]
        public bool TermsAndConditions { get; set; }
    }
}