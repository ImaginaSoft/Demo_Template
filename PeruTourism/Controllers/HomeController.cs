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
using PeruTourism.Models.Pasajero;
using System.Transactions;
using System.Web.Script.Serialization;

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
                    ViewBag.FlagIdioma = lstPublicacion.FirstOrDefault().FlagIdioma;
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
                    ViewBag.FlagIdioma = lstPublicacion.FirstOrDefault().FlagIdioma;

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
           // var lstPropuestaDetalle=null;

            List<Servicio> lstPropuestaDetalle = new List<Servicio>();

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


            if (nroVersion == "0") {

                 lstPropuestaDetalle = objPropuesta.ObtenerListadoServiciosPropuesta(Convert.ToInt32(nroPedido), Convert.ToInt32(NroPrograma), FlagIdioma).ToList();
            }
            else {
                 lstPropuestaDetalle = objPropuesta.ObtenerListadoServiciosPropuestaVersion(Convert.ToInt32(nroPedido), Convert.ToInt32(NroPrograma),Convert.ToInt32(nroVersion), FlagIdioma).ToList();

            }


            //var lstPropuestaDetalle = objPropuesta.ObtenerListadoServiciosPropuesta(Convert.ToInt32(nroPedido), Convert.ToInt32(NroPrograma), FlagIdioma);

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

                        servDetAgrupado = servDetAgrupado + itemAgrupado.DesServicioDet.Trim() + "↕" + itemAgrupado.NroServicio + "|";
                    }
                    else {

                        if (itemAgrupado.HoraServicio.Trim().Equals(string.Empty) || itemAgrupado.HoraServicio==null) {

                            //servDetAgrupado = servDetAgrupado + itemAgrupado.DesServicioDet + "<div class=\"prop-info\"><div class=\"info\"><i class=\"icon icon-time\"></i>" + "N/A" + "</div></div>" + "|";

                            servDetAgrupado = servDetAgrupado + itemAgrupado.DesServicioDet  + "|";

                        }
                        else {
                            //servDetAgrupado = servDetAgrupado + itemAgrupado.DesServicioDet + "<div class=\"prop-info\"><div class=\"info\"><i class=\"icon icon-time\"></i>" + itemAgrupado.HoraServicio + "</div></div>" + "|";
                            servDetAgrupado = servDetAgrupado + "<div class=\"prop-info\"><div class=\"info\"><i class=\"icon icon-time\"></i>" + itemAgrupado.HoraServicio + "</div></div>" +itemAgrupado.DesServicioDet + "|";
                        }

                        
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
            string gg = string.Empty;

            if (pDesLog.Equals(string.Empty))
            {
                gg = "gg";
            }
            else {
                gg = objPropuesta.InsertarHistorialCliente(pDesLog, pCodCliente, pNroPedido, pNroPropuesta, pNroVersion);
            }

            //string gg = objPropuesta.InsertarHistorialCliente(pDesLog, pCodCliente, pNroPedido, pNroPropuesta, pNroVersion);

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
            char flagIdioma = '\0';
            BalanceViewModel objBalance = new BalanceViewModel();

            var lstBalance = objPropuesta.CargaDocumentos(Convert.ToInt32(pCodCliente), Convert.ToInt32(pNroPedido));

            objBalance.lstBalance = lstBalance.ToList();

            objPropuestaViewModel.lstBalance = lstBalance.ToList();

            if (pNroVersion != "0")
            {

                var lstVersionFacturada = objPropuesta.VersionFacturada(Convert.ToInt32(pNroPedido));
                ViewBag.FlagIdioma = lstVersionFacturada.FirstOrDefault().FlagIdioma;


                if (lstVersionFacturada.FirstOrDefault().NroVersion != 0)
                {

                    return View(objPropuestaViewModel);

                }
                else
                {
                    flagIdioma = lstVersionFacturada.FirstOrDefault().FlagIdioma;
                    if (flagIdioma.Equals(ConstantesWeb.CHR_IDIOMA_INGLES))
                    {
                        mensajeError = "These options will be available after you book a trip with us.";
                    }
                    else {
                        mensajeError = "Estas opciones estarán disponibles después de reservar un viaje con nosotros.";

                    }

                   
                    objError.MsjError = mensajeError;
                    return View("~/Views/Shared/MyBookTripMessage.cshtml");

                }
            }
            else {
                return View("~/Views/Shared/MyBookTripMessage.cshtml");

            }








        }

        [HttpPost]
        public ActionResult OpenBookingStatusModal(int pNroPedido, int pNroPropuesta, int pNroVersion,char pFlagIdioma)
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


                ViewBag.FlagIdioma = pFlagIdioma;

                return PartialView("~/Views/Home/_ModalBookingStatus.cshtml", objPropuestaViewModel);


            }
            catch (Exception ex)
            {

                return View("~/Views/Shared/Error.cshtml");
                //return Redirect("paquetes");
            }
        }



        [HttpPost]
        public ActionResult OpenInfoBeforeTripModal(int pNroPedido, int pNroPropuesta, int pNroVersion,char pFlagIdioma)
        {
            try
            {
                var lstHotelInfo = objPropuesta.CargaHotelInfoAntes(pNroPedido, pNroPropuesta, pNroVersion);
                var lstStaffInfo = objPropuesta.CargaStaffInfoAntes(pNroPedido, pFlagIdioma);
                var lstVideoInfo = objPropuesta.CargaVideoInfoAntes(pNroPedido, pFlagIdioma);
                var lstClimaInfo = objPropuesta.CargaClimaInfoAntes(pNroPedido, pNroPropuesta, pNroVersion);
                string nomInfo = objPropuesta.CargaDocReqInfoAntes(pNroPedido, pFlagIdioma);

                objPropuestaViewModel.lstReservaHotel = lstHotelInfo.ToList();
                objPropuestaViewModel.lstStaff = lstStaffInfo.ToList();

                objPropuestaViewModel.lstVideo = lstVideoInfo.ToList();
                objPropuestaViewModel.lstClima = lstClimaInfo.ToList();
                objPropuestaViewModel.info = nomInfo;

                ViewBag.FlagIdioma = pFlagIdioma;
    
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

		[HttpPost]
		public JsonResult RegistrarPasajero(string pAccion = "", Int16 pNumPasajero = 0, string pDesLog = "", string pApe = "", string pPasaporte = "", string pFecNac = "",
                                            string pNacionalidad = "", string pTipo = "", string pGenero = "", string pNroPedido = "", string pObservacion = "")
        {
            String mensaje = "";
            String resultado = "";
            String valid = "";
            String fecha = "";

            if (pAccion != "E")
            {
                valid = ValidarCampos(pDesLog, pApe, pPasaporte, pFecNac, pNacionalidad, pGenero);

                if (pFecNac.Length > 0)
                    fecha = Convert.ToDateTime(pFecNac).ToString("yyyyMMdd");
            }

            if (valid.Length == 0)
            {
                using (TransactionScope transaccion = new TransactionScope())
                {
                    try
                    {
                        PasajeroAccess objPasajero = new PasajeroAccess();

                        switch(pAccion)
                        {
                            case "N":
                                objPasajero.RegistrarPasajero(pNumPasajero, pDesLog, pApe, pPasaporte, fecha, pNacionalidad, pNroPedido, pTipo, pGenero, pObservacion);
                                mensaje = "Se grabo correctamente la información.";
                                break;

                            case "M":
                                objPasajero.RegistrarPasajero(pNumPasajero, pDesLog, pApe, pPasaporte, fecha, pNacionalidad, pNroPedido, pTipo, pGenero, pObservacion);
                                mensaje = "Se actualizo correctamente la información.";
                                break;

                            case "E":
                                objPasajero.EliminarPasajero(pNumPasajero, pNroPedido);
                                mensaje = "Se elimino correctamente la información.";
                                break;
                        }

                        transaccion.Complete();

                        resultado = "OK";
                    }
                    catch (Exception ex)
                    {
                        transaccion.Dispose();

                        mensaje = ex.Message;
                        resultado = "ER";
                    }
                }
            }
            else
            {
                mensaje = valid;
                resultado = "ER";
            }

            return Json(new { contenido = mensaje, indicador = resultado });
        }

        [HttpPost]
        public JsonResult ListarPasajeros(string pNroPedido = "", Int16 pNumPasajero = 0)
        {
            JavaScriptSerializer _serializer = new JavaScriptSerializer();
            String mensaje = "";
            String resultado = "";
            List<Pasajero> lista;

            try
            {
                PasajeroAccess objPasajero = new PasajeroAccess();

                lista = objPasajero.ListarPasajeros(pNroPedido);

                if (pNumPasajero == 0)
                {
                    string tabla = "<table role='table' id='tbPasajeros' class='table table-striped table-bordered dt-responsive nowrap table-pt' width='100%' cellspacing='0' cellpadding='0'>";

                    tabla += @"<thead role='rowgroup'>
                            <tr role='row'>
                                <th role='columnheader'>NOMBRE</th>
                                <th role='columnheader'>APELLIDO</th>
                                <th role='columnheader'>NUMERO DE PASAPORTE</th>
                                <th role='columnheader'>FECHA DE NACIMIENTO</th>
                                <th role='columnheader'>NACIONALIDAD</th>
                                <th role='columnheader'>GENERO</th>
                                <th role='columnheader'>OBSERVACION</th>
                                <th role='columnheader' style='width: 110px'></th>
                            </tr>
                        </thead>
                        <tbody role='rowgroup'>";

                    foreach (Pasajero item in lista)
                    {
                        tabla += String.Format(@"<tr role='row'>
                                                <td role='cell' style='vertical-align: middle'>{0}</td>
                                                <td role='cell' style='vertical-align: middle'>{1}</td>
                                                <td role='cell' style='vertical-align: middle'>{2}</td>
                                                <td role='cell' style='vertical-align: middle'>{3}</td>
                                                <td role='cell' style='vertical-align: middle'>{4}</td>
                                                <td role='cell' style='vertical-align: middle'>{5}</td>
                                                <td role='cell' style='vertical-align: middle'>{6}</td>
                                                <td role='cell' style='text-align: center; vertical-align: middle'>
                                                    <button type='button' class='btn btn-primary btnAccion' onclick='editarPasajero({7})'><span class='fa fa-pencil'></span></button>
                                                    <button type='button' class='btn btn-danger btnAccion' onclick='eliminarPasajero({8})'><span class='fa fa-trash'></span></button>
                                                </td>
                                             </tr>",
                                               item.NomPasajero, item.ApePasajero, item.Pasaporte, item.FormatFchNacimiento, item.Nacionalidad, item.Genero, item.Observacion.ToString(), item.NroPasajero.ToString(), item.NroPasajero.ToString());
                    }

                    tabla += "</tbody></table>";

                    //tabla += "</tbody><tfoot>";

                    //tabla += String.Format("<tr><td colspan='7'><b>Nro. de pasajeros: {0}</b></td></tr>", lista.Count.ToString());

                    //tabla += "</tfoot></table>";

                    mensaje = tabla;
                }
                else
                {
                    mensaje = _serializer.Serialize(lista.Where(col => col.NroPasajero == pNumPasajero).ToList());
                }

                resultado = "OK";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                resultado = "ER";
            }

            return Json(new { contenido = mensaje, indicador = resultado });
        }

        public String ValidarCampos(string pDesLog, string pApe, string pPasaporte, string pFecNac, string pNacionalidad, string pGenero)
        {
            String resultado = "";

            if (pDesLog.Trim().Length == 0)
                resultado += "No ha ingreso el nombre del pasajero\n";

            if (pApe.Trim().Length == 0)
                resultado += "No ha ingreso el apellido del pasajero\n";

            if (pPasaporte.Trim().Length == 0)
                resultado += "No ha ingreso el número de pasaporte\n";

            if (pFecNac.Trim().Length == 0)
                resultado += "No ha ingreso la fecha de nacimiento\n";

            if (pNacionalidad.Trim().Length == 0)
                resultado += "No ha ingreso la nacionalidad\n";

            if (pGenero.Trim().Length == 0)
                resultado += "No ha seleccionado el género\n";

            return resultado;
        }


        public ActionResult ObtenerPasajero(string pCodCliente)
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
					var lstProgramaGG = objPropuesta.ObtenerListadoPasajero(lstPublicacion.FirstOrDefault().NroPedido);

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


	}
}