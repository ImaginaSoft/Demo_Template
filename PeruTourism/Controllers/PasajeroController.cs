using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeruTourism.Models.Pasajero;
using PeruTourism.Repository.PeruTourism;
using PeruTourism.Models.Visa;
using PeruTourism.Utility;
using PeruTourism.Models.PeruTourism;
using PeruTourism.Models.Paises;
using PeruTourism.Models.TipoPasajero;

namespace PeruTourism.Controllers
{
	public class PasajeroController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}


		public ActionResult Pasajero(string pIdPedido)
		{
            if (pIdPedido.Contains(" "))
                ViewBag.nroPedido = pIdPedido.Substring(0, pIdPedido.IndexOf(" "));
            else
                ViewBag.nroPedido = pIdPedido;

            ViewBag.Genero = ObtenerGeneros();
            ViewBag.Paises = ObtenerPaises();
            ViewBag.Tipos = ObtenerTipos();

            return View("Pasajero");
        }

        public static IList<SelectListItem> ObtenerGeneros()
        {
            var lresultado = new List<SelectListItem>
            {
                new SelectListItem { Value = string.Empty, Text = "(Seleccione)" },
                new SelectListItem { Value = "M", Text = "Masculino" },
                new SelectListItem { Value = "F", Text = "Femenino" }
            };

            return lresultado;
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
                lresultado = new List<SelectListItem>();
            }

            return lresultado;
        }

        [NonAction]
        public static IList<SelectListItem> ObtenerPaises()
        {
            List<Pais> lista;
            var lresultado = new List<SelectListItem>();

            try
            {
                lista = (new PasajeroAccess()).ListarPaises();

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
