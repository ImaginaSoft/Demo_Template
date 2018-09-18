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
            string codCLiente = id.Substring(7, 5);
            string acceso = "A";

            LoginAccess objLogin = new LoginAccess();
            FichaPropuestaAccess objPropuesta = new FichaPropuestaAccess();
            PropuestaViewModel objPropuestaViewModel = new PropuestaViewModel();

            try
            {

                if (idCliente.Trim().Length > 0)
                {

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



            }

            
            return View(objPropuestaViewModel);

        }

        public ActionResult VerPropuesta() {


            LoginAccess objLogin = new LoginAccess();
            FichaPropuestaAccess objPropuesta = new FichaPropuestaAccess();
            PropuestaViewModel objPropuestaViewModel = new PropuestaViewModel();
            var codCliente = Session["CodCliente"];

            if (codCliente != null)
            {

                var lstPublicacion = objLogin.LeeUltimaPublicacion(Convert.ToInt32(codCliente));
                var lstProgramaGG = objPropuesta.ObtenerListadoPropuesta(lstPublicacion.FirstOrDefault().NroPedido);

                objPropuestaViewModel.lstPrograma = lstProgramaGG.ToList();

            }

            return View(objPropuestaViewModel);
        }

        public ActionResult VerPropuestaDetalle(string KeyReg) {

            string nroPedido = KeyReg.Substring(0,6);
            string nroPropuesta = KeyReg.Substring(8,1);
            string nroVersion = KeyReg.Substring(10,1);

            FichaPropuestaAccess objPropuesta = new FichaPropuestaAccess();

            PropuestaViewModel objPropuestaViewModel = new PropuestaViewModel();

            var lstPropuestaDetalle = objPropuesta.ObtenerListadoServiciosPropuesta(Convert.ToInt32(nroPedido),Convert.ToInt32(nroPropuesta));

            objPropuestaViewModel.lstServicio = lstPropuestaDetalle.ToList();

            return View(objPropuestaViewModel);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public JsonResult ListadoPedido() {

            PedidoAccess objPedido = new PedidoAccess();
            var vPedido = objPedido.ObtenerListadoPedido();
            return Json(vPedido, JsonRequestBehavior.AllowGet);

        }


    }
}