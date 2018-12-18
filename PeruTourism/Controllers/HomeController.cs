using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeruTourism.Repository.PeruTourism;
using PeruTourism.Utility;
using PeruTourism.Models.PeruTourism;
using System.Data.SqlClient;
using System.Data;
using PeruTourism.Models.Galeria;



namespace PeruTourism.Controllers
{
    public class HomeController : Controller
    {

        GaleriaAccess objGaleriaAccess = new GaleriaAccess();
        GaleriaViewModel objGaleriaViewModel = new GaleriaViewModel();
        PropuestaViewModel objPropuestaViewModel = new PropuestaViewModel();
        FichaPropuestaAccess objPropuesta = new FichaPropuestaAccess();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string id)
        {

            string idCliente = id;
            string codCLiente = string.Empty;
            string lineagg = "0,";

            string _IdCliente = string.Empty;
            string _CodCliente = string.Empty;
            string _NomCliente = string.Empty;
            string _EmailCliente = string.Empty;

            LoginAccess objLogin = new LoginAccess();
            
            //PropuestaViewModel objPropuestaViewModel = new PropuestaViewModel();

            try
            {

                if (idCliente.Trim().Length > 0)
                {

                    int gg = idCliente.Length - (idCliente.Length - (idCliente.Length - (idCliente.Substring(0, 7).Length)));
                    codCLiente = idCliente.Substring(7, gg);

                    var lstCliente = objLogin.LeerCliente(idCliente, codCLiente);

                    _IdCliente = idCliente.Trim();
                    _CodCliente = lstCliente.FirstOrDefault().CodCliente;
                    _NomCliente = lstCliente.FirstOrDefault().NomCliente;
                    _EmailCliente = lstCliente.FirstOrDefault().EmailCliente;

                    Session["IdCliente"] = idCliente.Trim();
                    Session["CodCliente"] = lstCliente.FirstOrDefault().CodCliente;
                    Session["NomCliente"] = lstCliente.FirstOrDefault().NomCliente;
                    //Session["EmailCliente"] = lstCliente.FirstOrDefault().EmailCliente;

                }

                if (_CodCliente != null)
                {

                    var lstPublicacion = objLogin.LeeUltimaPublicacion(Convert.ToInt32(codCLiente));
                    var lstProgramaGG = objPropuesta.ObtenerListadoPropuesta(lstPublicacion.FirstOrDefault().NroPedido, lstPublicacion.FirstOrDefault().FlagIdioma);


                    Session["Idioma"] = lstPublicacion.FirstOrDefault().FlagIdioma;

                    ViewBag.Idioma = lstPublicacion.FirstOrDefault().FlagIdioma;
                    ViewBag.IdCliente = _IdCliente;
                    ViewBag.CodCliente = _CodCliente;
                    ViewBag.NomCliente = _NomCliente;
                    ViewBag.EmailCliente = _EmailCliente;

                    objPropuestaViewModel.lstPrograma = lstProgramaGG.ToList();

                }

            }
            catch (Exception ex)
            {

                ViewBag.error = ex.ToString() + lineagg;
                return View("~/Views/Shared/Error.cshtml");

            }

            return View("Index",objPropuestaViewModel);

        }

        public ActionResult VerPropuesta(string pCodCliente)
        {

            //PropuestaViewModel objPropuestaViewModel = new PropuestaViewModel();
            try
            {
                LoginAccess objLogin = new LoginAccess();
                FichaPropuestaAccess objPropuesta = new FichaPropuestaAccess();

                //var codCliente = Session["CodCliente"];

                var codCliente = pCodCliente;

                if (codCliente != null)
                {

                    var lstPublicacion = objLogin.LeeUltimaPublicacion(Convert.ToInt32(codCliente));
                    var lstProgramaGG = objPropuesta.ObtenerListadoPropuesta(lstPublicacion.FirstOrDefault().NroPedido, lstPublicacion.FirstOrDefault().FlagIdioma);

                    objPropuestaViewModel.lstPrograma = lstProgramaGG.ToList();

                    ViewBag.Idioma = lstPublicacion.FirstOrDefault().FlagIdioma;

                    ViewBag.CodCliente = codCliente;

                }
            }
            catch (Exception ex)
            {

                return View("~/Views/Shared/Error.cshtml");
            }


            return View(objPropuestaViewModel);
        }

        public ActionResult VerPropuestaDetalle(string KeyReg, string NroPrograma, char FlagIdioma,string pCodCliente)
        {

            string nroPedido = KeyReg.Substring(0, 6);
            string nroPropuesta = KeyReg.Substring(8, 1);
            string nroVersion = KeyReg.Substring(10, 1);

            //var codCliente = Session["CodCliente"];
            ViewBag.nroPedido = nroPedido;
            ViewBag.nroPropuesta = nroPropuesta;
            ViewBag.codCliente = pCodCliente;
            ViewBag.nroVersion = nroVersion;

            var codCliente = pCodCliente;

            LoginAccess objLogin = new LoginAccess();
            FichaPropuestaAccess objPropuesta = new FichaPropuestaAccess();
            //PropuestaViewModel objPropuestaViewModel = new PropuestaViewModel();

            List<Servicio> ListServicios = new List<Servicio>();
            Servicio objServicio = new Servicio();

            //Servicio[] arrayServicio = new Servicio[] { };

            if (codCliente != null)
            {

                var lstPublicacion = objLogin.LeeUltimaPublicacion(Convert.ToInt32(codCliente));
                var lstProgramaGG = objPropuesta.ObtenerListadoPropuesta(lstPublicacion.FirstOrDefault().NroPedido, lstPublicacion.FirstOrDefault().FlagIdioma);

                objPropuestaViewModel.lstPrograma = lstProgramaGG.Where(p => p.NroPrograma == NroPrograma).ToList();

            }

            var lstPropuestaDetalle = objPropuesta.ObtenerListadoServiciosPropuesta(Convert.ToInt32(nroPedido), Convert.ToInt32(NroPrograma), FlagIdioma);

            var agrupacion = from p in lstPropuestaDetalle group p by p.NroDia into grupo select grupo;


            foreach (var item in agrupacion)
            {

                string nroDia = string.Empty;
                string servDetAgrupado = string.Empty;
                string desServicio = string.Empty;
                string ciudad = string.Empty;
                string horaServicio = string.Empty;
                //DateTime fchInicio = string.Empty;
                int i = 0;
                int cantidad = agrupacion.Count();

                Servicio[] arrayServicio = new Servicio[cantidad];

                foreach (var itemAgrupado in item)
                {


                    //servDetAgrupado = servDetAgrupado + itemAgrupado.DesServicioDet + Environment.NewLine;

                    if (itemAgrupado.CodTipoServicio == 2) {

                        servDetAgrupado = servDetAgrupado + itemAgrupado.DesServicioDet + "↕" + itemAgrupado.NroServicio + "|";
                    }
                    else {
                        servDetAgrupado = servDetAgrupado + itemAgrupado.DesServicioDet+ "|";
                    }

                    

                    if (itemAgrupado.DesServicio != "")
                    {

                        desServicio = itemAgrupado.DesServicio.FirstOrDefault().ToString();

                    }
                    objServicio.NroDia = itemAgrupado.NroDia;
                    objServicio.DesServicio = desServicio;
                    objServicio.DesServicioDet = servDetAgrupado;
                    objServicio.Ciudad = itemAgrupado.Ciudad;
                    objServicio.HoraServicio = itemAgrupado.HoraServicio;
                    objServicio.CodTipoServicio = itemAgrupado.CodTipoServicio;
                    objServicio.NroServicio = itemAgrupado.NroServicio;
					objServicio.NombreEjecutiva = itemAgrupado.NombreEjecutiva;
                    //objServicio.FchInicio = itemAgrupado.FchInicio;

                }


                var servicioDetAgrupado = new Servicio
                {

                    NroDia = item.FirstOrDefault().NroDia,
                    DesServicio = item.FirstOrDefault().DesServicio,
                    DesServicioDet = servDetAgrupado,
                    Ciudad = item.FirstOrDefault().Ciudad,
                    HoraServicio = item.FirstOrDefault().HoraServicio,
                    FchInicio = item.FirstOrDefault().FchInicio,
                    NroServicio = item.FirstOrDefault().NroServicio,
                    CodTipoServicio = item.FirstOrDefault().CodTipoServicio,
				    NombreEjecutiva = item.FirstOrDefault().NombreEjecutiva
				};


                ListServicios.Add(servicioDetAgrupado);

            }

            // objPropuestaViewModel.lstServicio = lstPropuestaDetalle.ToList();

            var lstPropuestaPrecio = objPropuesta.CargaPropuestaPrecio(Convert.ToInt32(nroPedido), Convert.ToInt32(NroPrograma));

            objPropuestaViewModel.lstServicio = ListServicios;
            objPropuestaViewModel.lstPropuestaPrecio = lstPropuestaPrecio.ToList();

            return View(objPropuestaViewModel);
        }

        public JsonResult ListadoPedido()
        {

            PedidoAccess objPedido = new PedidoAccess();
            var vPedido = objPedido.ObtenerListadoPedido();
            return Json(vPedido, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult OpenModalDetailsHotel(string pNroServicio)
        {
            try
            {
                string sIdServicio = string.Empty;

                var lstGaleria = objGaleriaAccess.CargarGaleria(Convert.ToInt32(pNroServicio));

                //objGaleriaViewModel.lstGaleria = lstGaleria.ToList();
                objPropuestaViewModel.lstServicio = lstGaleria.ToList();

                //sIdServicio = lstGaleria.FirstOrDefault().NroServicio.Trim();

                //int sIdServicio_length = sIdServicio.Length;

                return PartialView("~/Views/Galeria/_ModalDetalleHotel.cshtml", objPropuestaViewModel);
                //return PartialView("~/Views/PaqueteCostum/VuelosHoteles/Hoteles/_ModalDetalleRooms.cshtml", objHotelViewModel);
            }
            catch (Exception ex)
            {

                return View("~/Views/Shared/Error.cshtml");
                //return Redirect("paquetes");
            }
        }

        [HttpPost]
        public JsonResult RegistrarHistorialCliente(string pDesLog, string pCodCliente, string pNroPedido, string pNroPropuesta,string pNroVersion)
        {

            FichaPropuestaAccess objPropuesta = new FichaPropuestaAccess();

            string gg = objPropuesta.InsertarHistorialCliente(pDesLog, pCodCliente, pNroPedido, pNroPropuesta, pNroVersion);

			//jlopez
			//PeruTourismEmail gg = new PeruTourismEmail();		
            PeruTourismMail Mensaje = new PeruTourismMail();
            Mensaje.EnviarCorreo("Mensaje", "jlopez.j87@gmail.com", pDesLog);
			
			return Json(gg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MostrarSaldo(string pCliente,string pNroPedido) {

            DataTable dt = new DataTable("BalanceTable");
            BalanceViewModel objBalance = new BalanceViewModel();

            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
            dt.Columns.Add(new DataColumn("Col2", typeof(string)));
            dt.Columns.Add(new DataColumn("Col3", typeof(string)));


            for (int i = 0; i < 3; i++)
            {
                DataRow row = dt.NewRow();
                row["Col1"] = "col 1, row " + i;
                row["Col2"] = "col 2, row " + i;
                row["Col3"] = "col 3, row " + i;
                dt.Rows.Add(row);
            }


            var lstBalance = objPropuesta.CargaDocumentos(Convert.ToInt32(pCliente), Convert.ToInt32(pNroPedido));

            objBalance.lstBalance = lstBalance.ToList();

            return View(objBalance);
        }
        [HttpPost]
        public ActionResult OpenModalBalance(string pCliente, string pNroPedido)
        {
            try
            {
                //DataTable dt = new DataTable("BalanceTable");
                BalanceViewModel objBalance = new BalanceViewModel();

                //dt.Columns.Add(new DataColumn("Col1", typeof(string)));
                //dt.Columns.Add(new DataColumn("Col2", typeof(string)));
                //dt.Columns.Add(new DataColumn("Col3", typeof(string)));


                //for (int i = 0; i < 3; i++)
                //{
                //    DataRow row = dt.NewRow();
                //    row["Col1"] = "col 1, row " + i;
                //    row["Col2"] = "col 2, row " + i;
                //    row["Col3"] = "col 3, row " + i;
                //    dt.Rows.Add(row);
                //}


                var lstBalance = objPropuesta.CargaDocumentos(Convert.ToInt32(pCliente), Convert.ToInt32(pNroPedido));

                objBalance.lstBalance = lstBalance.ToList();

                objPropuestaViewModel.lstBalance = lstBalance.ToList();

                return PartialView("~/Views/Home/_ModalMostrarSaldo.cshtml", objPropuestaViewModel);

                
            }
            catch (Exception ex)
            {

                return View("~/Views/Shared/Error.cshtml");
                //return Redirect("paquetes");
            }
        }



        public ActionResult MyBookedTrip(string pCodCliente, string pNroPedido, string pNroPropuesta, string pNroVersion) {


            Error objError = new Error();


            ViewBag.CodCliente = pCodCliente;

            ViewBag.NroPedido = pNroPedido;
            ViewBag.NroPropuesta = pNroPropuesta;
            ViewBag.NroVersion = pNroVersion;

            string mensajeError = string.Empty;
            BalanceViewModel objBalance = new BalanceViewModel();

            var lstBalance = objPropuesta.CargaDocumentos(Convert.ToInt32(pCodCliente), Convert.ToInt32(pNroPedido));

            var lstVersionFacturada = objPropuesta.VersionFacturada(Convert.ToInt32(pNroPedido));

            objBalance.lstBalance = lstBalance.ToList();

            objPropuestaViewModel.lstBalance = lstBalance.ToList();


            if (lstVersionFacturada.FirstOrDefault().NroVersion != 0) {

                return View(objPropuestaViewModel);

            }
            else
            {
                mensajeError = "These options will be available after you book a trip with us.";
                objError.MsjError = mensajeError;
                return View("~/Views/Shared/MyBookTripMessage.cshtml");

            }

        }

        [HttpPost]
        public ActionResult OpenBookingStatusModal(int pNroPedido, int pNroPropuesta, int pNroVersion)
        {
            try
            {
                
                var lstPasajero = objPropuesta.CargaPasajero(pNroPedido);
                var lstReservaTerrestre = objPropuesta.CargaTerrestre(pNroPedido, pNroPropuesta, pNroVersion);
                var lstReservaAereo = objPropuesta.CargaAereo(pNroPedido, pNroPropuesta, pNroVersion);
                var lstReservaHotel = objPropuesta.CargaHotel(pNroPedido, pNroPropuesta, pNroVersion);

                //objPropuestaViewModel.lstBalance = lstBalance.ToList();

                objPropuestaViewModel.lstPasajero = lstPasajero.ToList();
                objPropuestaViewModel.lstReservaTerrestre = lstReservaTerrestre.ToList();
                objPropuestaViewModel.lstReservaAerero = lstReservaAereo.ToList();
                objPropuestaViewModel.lstReservaHotel = lstReservaHotel.ToList();
                

                return PartialView("~/Views/Home/_ModalBookingStatus.cshtml", objPropuestaViewModel);


            }
            catch (Exception ex)
            {

                return View("~/Views/Shared/Error.cshtml");
                //return Redirect("paquetes");
            }
        }



        [HttpPost]
        public ActionResult OpenInfoBeforeTripModal(int pNroPedido, int pNroPropuesta, int pNroVersion)
        {
            try
            {


                var lstHotelInfo = objPropuesta.CargaHotelInfoAntes(pNroPedido, pNroPropuesta, pNroVersion);
                var lstStaffInfo = objPropuesta.CargaStaffInfoAntes(pNroPedido,'I');
                var lstVideoInfo = objPropuesta.CargaVideoInfoAntes(pNroPedido, 'I');
                var lstClimaInfo = objPropuesta.CargaClimaInfoAntes(pNroPedido, pNroPropuesta, pNroVersion);
                string nomInfo = objPropuesta.CargaDocReqInfoAntes(pNroPedido, 'I');



                //var lstPasajero = objPropuesta.CargaPasajero(pNroPedido);
                //var lstReservaTerrestre = objPropuesta.CargaTerrestre(pNroPedido, pNroPropuesta, pNroVersion);
                //var lstReservaAereo = objPropuesta.CargaAereo(pNroPedido, pNroPropuesta, pNroVersion);
                //var lstReservaHotel = objPropuesta.CargaHotel(pNroPedido, pNroPropuesta, pNroVersion);

                //objPropuestaViewModel.lstBalance = lstBalance.ToList();

                objPropuestaViewModel.lstReservaHotel = lstHotelInfo.ToList();
                objPropuestaViewModel.lstStaff = lstStaffInfo.ToList();

                objPropuestaViewModel.lstVideo = lstVideoInfo.ToList();
                objPropuestaViewModel.lstClima = lstClimaInfo.ToList();

                objPropuestaViewModel.info = nomInfo;

                //objPropuestaViewModel.lstReservaTerrestre = lstReservaTerrestre.ToList();
                //objPropuestaViewModel.lstReservaAerero = lstReservaAereo.ToList();
                //objPropuestaViewModel.lstReservaHotel = lstReservaHotel.ToList();


                return PartialView("~/Views/Home/_ModalInfoBeforeTrip.cshtml", objPropuestaViewModel);


            }
            catch (Exception ex)
            {

                return View("~/Views/Shared/Error.cshtml");
                //return Redirect("paquetes");
            }
        }



        public JsonResult MostraBalanceData(string tableHtml)
        {
            Session["TableStr"] = tableHtml;
            return Json(new { Data = "true" });
        }


		[HttpPost]
        public JsonResult GG(string tableHtml)
        {
            Session["TableStr"] = tableHtml;
            return Json(new { Data = "true" });
        }

        [HttpPost]
        public JsonResult AjaxMethod(string name)
        {
            //PersonModel person = new PersonModel
            //{
            //    Name = name,
            //    DateTime = DateTime.Now.ToString()
            //};

            return Json(name, JsonRequestBehavior.AllowGet);
        }
    }
}