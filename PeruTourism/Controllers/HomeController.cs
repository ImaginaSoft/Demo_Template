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


namespace PeruTourism.Controllers
{
    public class HomeController : Controller
    {
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
            FichaPropuestaAccess objPropuesta = new FichaPropuestaAccess();
            PropuestaViewModel objPropuestaViewModel = new PropuestaViewModel();

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
                    //Session["NomCliente"] = lstCliente.FirstOrDefault().NomCliente;
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

            PropuestaViewModel objPropuestaViewModel = new PropuestaViewModel();
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

            var codCliente = pCodCliente;


            LoginAccess objLogin = new LoginAccess();
            FichaPropuestaAccess objPropuesta = new FichaPropuestaAccess();
            PropuestaViewModel objPropuestaViewModel = new PropuestaViewModel();

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
                    servDetAgrupado = servDetAgrupado + itemAgrupado.DesServicioDet + "|";

                    if (itemAgrupado.DesServicio != "")
                    {

                        desServicio = itemAgrupado.DesServicio.FirstOrDefault().ToString();

                    }
                    objServicio.NroDia = itemAgrupado.NroDia;
                    objServicio.DesServicio = desServicio;
                    objServicio.DesServicioDet = servDetAgrupado;
                    objServicio.Ciudad = itemAgrupado.Ciudad;
                    objServicio.HoraServicio = itemAgrupado.HoraServicio;
                    //objServicio.FchInicio = itemAgrupado.FchInicio;

                }


                var servicioDetAgrupado = new Servicio
                {

                    NroDia = item.FirstOrDefault().NroDia,
                    DesServicio = item.FirstOrDefault().DesServicio,
                    DesServicioDet = servDetAgrupado,
                    Ciudad = item.FirstOrDefault().Ciudad,
                    HoraServicio = item.FirstOrDefault().HoraServicio,
                    FchInicio = item.FirstOrDefault().FchInicio
                };


                ListServicios.Add(servicioDetAgrupado);

            }

            // objPropuestaViewModel.lstServicio = lstPropuestaDetalle.ToList();

            objPropuestaViewModel.lstServicio = ListServicios;

            return View(objPropuestaViewModel);
        }

        public JsonResult ListadoPedido()
        {

            PedidoAccess objPedido = new PedidoAccess();
            var vPedido = objPedido.ObtenerListadoPedido();
            return Json(vPedido, JsonRequestBehavior.AllowGet);

        }


        public ActionResult OpenModalHotelDetails(string pIdServicio) {


            return View();


        }
    }
}