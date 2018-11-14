using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeruTourism.Repository.PeruTourism;
using PeruTourism.Models.Visa;
using PeruTourism.Utility;
using PeruTourism.Models.PeruTourism;


namespace PeruTourism.Controllers
{
    public class PagoVisaController : Controller
    {

        PagoVisaAccess objPagoVisaAccess = new PagoVisaAccess();
        PedidoVisaViewModel objPedidoVisaViewModel = new PedidoVisaViewModel();
        // GET: PagoVisa
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Pago(string pIdPedido) {

            string sIdPedido = string.Empty;

            //sIdPedido = objPagoVisaAccess.ObtenerListadoPedidoVISA(Convert.ToInt32(pIdPedido.Substring(0,6)));

            var lstPedidoVisa = objPagoVisaAccess.CargarPedidoVisa(Convert.ToInt32(pIdPedido.Substring(0,6)));

            objPedidoVisaViewModel.lstPedidoVisa = lstPedidoVisa.ToList();

            sIdPedido = lstPedidoVisa.FirstOrDefault().IDPedido.Trim();

            int sIdPedido_length = sIdPedido.Length;

            if (sIdPedido.Substring(13, 6) == pIdPedido.Substring(0, 6)) {

                return View(objPedidoVisaViewModel);

            }
            else {

                return View("PagoVisa/Info");

            }

        }
        public ActionResult ConfirmarPago(PedidoVisa pedido) {

            var lstPedidoVisa = objPagoVisaAccess.CargarPedidoVisa(Convert.ToInt32(pedido.IDPedido.Substring(13, 6)));



            objPedidoVisaViewModel.lstPedidoVisa = lstPedidoVisa.ToList();

            return View(objPedidoVisaViewModel);

        }

        public ActionResult RealizarPago(PedidoVisa pedido) {


            var lstPedidoVisa = new List<PedidoVisa>();

            lstPedidoVisa.Add(item: pedido);

            if (!ModelState.IsValid) {

                objPedidoVisaViewModel.lstPedidoVisa = lstPedidoVisa;

                //string gg = objPagoVisaAccess.InsertarOrdenpagoVISA(pedido.Monto, pedido.IDPedido);
                return View("~/Views/PagoVisa/Pago.cshtml", objPedidoVisaViewModel);
            }

            return Content("Success");
        }



    }
}