using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PeruTourism.Repository.PeruTourism;
using PeruTourism.Models.Visa;

namespace PeruTourism.Controllers
{
    public class PagoVisaController : Controller
    {

        PagoVisaAccess objPagoVisaAccess = new PagoVisaAccess();
        PedidoVisaViewModel objPedidoVisaViewModel = new PedidoVisaViewModel();
        // GET: PagoVisa
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //public ActionResult Pago(string pIdPedido, string pcodCliente, string pNroPrograma, string pFlagIdioma, bool pFlagVendido) {

        //    string sIdPedido = string.Empty;

        //    string urlPago = string.Empty;

        //    urlPago = objPagoVisaAccess.CargaUrlVisanetLink();

        //    objPedidoVisaViewModel.urlPago = urlPago;
        //    objPedidoVisaViewModel.codCliente = pcodCliente;
        //    objPedidoVisaViewModel.idPedido = pIdPedido;
        //    objPedidoVisaViewModel.nroPrograma = pNroPrograma;
        //    objPedidoVisaViewModel.idioma = pFlagIdioma;
        //    objPedidoVisaViewModel.flagVendido = pFlagVendido;

        //    int sIdPedido_length = sIdPedido.Length;

        //    return View(objPedidoVisaViewModel);


        //}
        //public ActionResult ConfirmarPago(PedidoVisa pedido) {

        //    var lstPedidoVisa = objPagoVisaAccess.CargarPedidoVisa(Convert.ToInt32(pedido.IDPedido.Substring(13, 6)));



        //    objPedidoVisaViewModel.lstPedidoVisa = lstPedidoVisa.ToList();

        //    return View(objPedidoVisaViewModel);

        //}

        //public ActionResult RealizarPago(PedidoVisa pedido) {


        //    var lstPedidoVisa = new List<PedidoVisa>();

        //    lstPedidoVisa.Add(item: pedido);

        //    if (!ModelState.IsValid) {

        //        objPedidoVisaViewModel.lstPedidoVisa = lstPedidoVisa;

        //        //string gg = objPagoVisaAccess.InsertarOrdenpagoVISA(pedido.Monto, pedido.IDPedido);
        //        return View("~/Views/PagoVisa/Pago.cshtml", objPedidoVisaViewModel);
        //    }

        //    string gg = objPagoVisaAccess.InsertarOrdenpagoVISA(pedido.Monto, pedido.IDPedido);
        //    return Content("Success");
        //}



	}
}