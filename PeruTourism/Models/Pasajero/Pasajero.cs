using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeruTourism.Models.PeruTourism
{
    public class Pasajero
    {
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string NomPasajero { get; set; }

        [StringLength(50)]
        [Display(Name = "Apellidos")]
        public string ApePasajero { get; set; }

        [StringLength(30)]
        [Display(Name = "Número de Pasaporte")]
        public string Pasaporte { get; set; }

        [StringLength(10)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime? FchNacimiento { get; set; }

        public string FormatFchNacimiento
        {
            get { return (FchNacimiento.HasValue ? FchNacimiento.Value.ToString("dd/MM/yyyy") : string.Empty); }
        }

        [StringLength(3)]
        [Display(Name = "Nacionalidad")]
        public string Nacionalidad { get; set; }

        [StringLength(1)]
        [Display(Name = "Género")]
        public string Genero { get; set; }

        [StringLength(30)]
        [Display(Name = "Tipo de pasajero")]
        public string TipoPasajero { get; set; }

        public Int16 NroPasajero { get; set; }
        public string CodNacionalidad { get; set; }
        public string CodGenero { get; set; }

        [Display(Name = "Observación")]
        public string Observacion { get; set; }
    }
}