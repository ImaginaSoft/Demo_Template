using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PeruTourism.Repository.PeruTourism;
using PeruTourism.Utility;
using PeruTourism.Models.Paises;
using PeruTourism.Models.TipoPasajero;
using CustomLog;

namespace PeruTourism.Controllers
{
	public class PasajeroController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}


		public ActionResult Pasajero(string pIdPedido, char pIdioma)
		{
            if (pIdPedido.Contains(" "))
                ViewBag.nroPedido = pIdPedido.Substring(0, pIdPedido.IndexOf(" "));
            else
                ViewBag.nroPedido = pIdPedido;

            ViewBag.Genero = ObtenerGeneros(pIdioma);
            ViewBag.Paises = ObtenerPaises(pIdioma);
            ViewBag.Tipos = ObtenerTipos();
            ViewBag.Idioma = pIdioma;
            return View("Pasajero");
        }

        public static IList<SelectListItem> ObtenerGeneros(char pIdioma)
        {


            if (pIdioma.Equals(ConstantesWeb.CHR_IDIOMA_INGLES)) {
                var lresultado = new List<SelectListItem>
            {
                new SelectListItem { Value = string.Empty, Text = "(Select)" },
                new SelectListItem { Value = "M", Text = "Male"},
                new SelectListItem { Value = "F", Text = "Female" }
            };

                return lresultado;

            }
            else {
                var lresultado = new List<SelectListItem>
            {
                new SelectListItem { Value = string.Empty, Text = "(Seleccione)" },
                new SelectListItem { Value = "M", Text = "Masculino"},
                new SelectListItem { Value = "F", Text = "Femenino" }
            };

                return lresultado;
            }

         
        }

        public static IList<SelectListItem> ObtenerTipos()
        {
            List<TipoPasajero> lista;
            var lresultado = new List<SelectListItem>();

            try
            {
                lista = (new PasajeroAccess()).ListarTipoPasajero();

                lresultado.Add(new SelectListItem { Value = string.Empty, Text = "(Seleccione)" });

                foreach (var item in lista)
                {
                    lresultado.Add(new SelectListItem { Value = item.CodTipoPasajero, Text = item.NomTipoPasajero });
                }
            }
            catch (Exception ex)
            {
                Bitacora.Current.Error<PasajeroController>(ex, new { lresultado });
                lresultado = new List<SelectListItem>();
            }

            return lresultado;
        }

        [NonAction]
        public static IList<SelectListItem> ObtenerPaises(char pIdioma)
        {
            List<Pais> lista;
            var lresultado = new List<SelectListItem>();

            try
            {
                lista = (new PasajeroAccess()).ListarPaises(pIdioma);

                foreach (var item in lista)
                {
                    lresultado.Add(new SelectListItem { Value = item.CodPais, Text = item.NomPais });
                }
            }
            catch (Exception ex)
            {
                lresultado = new List<SelectListItem>();
            }

            return lresultado;
        }

        //[HttpPost]
        //public JsonResult RegistrarPasajero(string NombrePasajero)
        //{

        //	PasajeroAccess objPasajero = new PasajeroAccess();

        //	string gg = objPasajero.InsertarHistorialCliente(pDesLog, pCodCliente, pNroPedido, pNroPropuesta, pNroVersion);

        //	return Json(gg, JsonRequestBehavior.AllowGet);
        //}




    }
}
