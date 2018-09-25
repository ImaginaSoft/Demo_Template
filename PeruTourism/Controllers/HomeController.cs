using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeruTourism.Repository.PeruTourism;
using PeruTourism.Models.PeruTourism;

namespace PeruTourism.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            string idCliente = id;
            string codCLiente = string.Empty;

            LoginAccess objLogin = new LoginAccess();
            FichaPropuestaAccess objPropuesta = new FichaPropuestaAccess();
            PropuestaViewModel objPropuestaViewModel = new PropuestaViewModel();

            try
            {

                if (idCliente.Trim().Length > 0)
                {
                     codCLiente = idCliente.Substring(7, 5);

                    var lstCliente = objLogin.LeerCliente(idCliente, codCLiente);

                    Session["IdCliente"] = idCliente.Trim();
                    Session["CodCliente"] = lstCliente.FirstOrDefault().CodCliente;
                    Session["NomCliente"] = lstCliente.FirstOrDefault().NomCliente;
                    Session["EmailCliente"] = lstCliente.FirstOrDefault().EmailCliente;

                }

                if (Session["CodCliente"] != null)
                {

                    var lstPublicacion = objLogin.LeeUltimaPublicacion(Convert.ToInt32(codCLiente));
                    var lstProgramaGG = objPropuesta.ObtenerListadoPropuesta(lstPublicacion.FirstOrDefault().NroPedido);

                    objPropuestaViewModel.lstPrograma = lstProgramaGG.ToList();

                }

            }
            catch (Exception ex)
            {

                return View("~/Views/Shared/Error.cshtml");

            }

            return View(objPropuestaViewModel);

        }

        public ActionResult VerPropuesta() {

            PropuestaViewModel objPropuestaViewModel = new PropuestaViewModel();
            try
            {
                LoginAccess objLogin = new LoginAccess();
                FichaPropuestaAccess objPropuesta = new FichaPropuestaAccess();
               
                var codCliente = Session["CodCliente"];

                if (codCliente != null)
                {

                    var lstPublicacion = objLogin.LeeUltimaPublicacion(Convert.ToInt32(codCliente));
                    var lstProgramaGG = objPropuesta.ObtenerListadoPropuesta(lstPublicacion.FirstOrDefault().NroPedido);

                    objPropuestaViewModel.lstPrograma = lstProgramaGG.ToList();

                }
            }
            catch (Exception ex) {

                return View("~/Views/Shared/Error.cshtml");
            }


            return View(objPropuestaViewModel);
        }

        public ActionResult VerPropuestaDetalle(string KeyReg,string NroPrograma) {

            string nroPedido = KeyReg.Substring(0,6);
            string nroPropuesta = KeyReg.Substring(8,1);
            string nroVersion = KeyReg.Substring(10,1);
            
            var codCliente = Session["CodCliente"];

            LoginAccess objLogin = new LoginAccess();
            FichaPropuestaAccess objPropuesta = new FichaPropuestaAccess();
            PropuestaViewModel objPropuestaViewModel = new PropuestaViewModel();

            List<Servicio> ListServicios = new List<Servicio>();
            Servicio objServicio = new Servicio();

            //Servicio[] arrayServicio = new Servicio[] { };

            if (codCliente != null)
            {

                var lstPublicacion = objLogin.LeeUltimaPublicacion(Convert.ToInt32(codCliente));
                var lstProgramaGG = objPropuesta.ObtenerListadoPropuesta(lstPublicacion.FirstOrDefault().NroPedido);

                objPropuestaViewModel.lstPrograma = lstProgramaGG.Where(p => p.NroPrograma == NroPrograma).ToList();

            }

            var lstPropuestaDetalle = objPropuesta.ObtenerListadoServiciosPropuesta(Convert.ToInt32(nroPedido),Convert.ToInt32(nroPropuesta));

            var agrupacion = from p in lstPropuestaDetalle group p by p.NroDia into grupo select grupo;


            foreach (var item in agrupacion) {

                string nroDia = string.Empty;
                string servDetAgrupado = string.Empty;
                string desServicio = string.Empty;
                int i = 0;
                int cantidad = agrupacion.Count();

                Servicio[] arrayServicio = new Servicio[cantidad];

                foreach (var itemAgrupado in item) {
                   
                    servDetAgrupado = servDetAgrupado + itemAgrupado.DesServicioDet + Environment.NewLine;

                    if (itemAgrupado.DesServicio !="") {

                        desServicio = itemAgrupado.DesServicio;

                    }
                    objServicio.NroDia = itemAgrupado.NroDia;
                    objServicio.DesServicio = (desServicio=="")?"SIN TITULO": desServicio;
                    objServicio.DesServicioDet = servDetAgrupado;

                }


                var servicioDetAgrupado = new Servicio {

                    NroDia = item.FirstOrDefault().NroDia,
                    DesServicio = desServicio,
                    DesServicioDet = servDetAgrupado
                };
   

                ListServicios.Add(servicioDetAgrupado);

            }

           // objPropuestaViewModel.lstServicio = lstPropuestaDetalle.ToList();

            objPropuestaViewModel.lstServicio = ListServicios;

            return View(objPropuestaViewModel);
        }

        public JsonResult ListadoPedido() {

            PedidoAccess objPedido = new PedidoAccess();
            var vPedido = objPedido.ObtenerListadoPedido();
            return Json(vPedido, JsonRequestBehavior.AllowGet);

        }


    }
}