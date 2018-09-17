using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Demo_1.Models.PeruTourism;
using Demo_1.Repository.PeruTourism;

namespace Demo_1.Controllers
{
    public class PeruTourismController : Controller
    {
        // GET: PeruTourism
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ValidarAcceso(string userId) {          

            string idCliente = userId;
            string codCLiente = userId.Substring(7, 5);
            string acceso = "A";
            
            LoginAccess objLogin = new LoginAccess();
           
            try
            {

                if (userId.Trim().Length > 0) {

                   var lstCliente= objLogin.LeerCliente(idCliente, codCLiente);

                    Session["CodCliente"] = lstCliente.FirstOrDefault().CodCliente;
                    Session["NomCliente"] =lstCliente.FirstOrDefault().NomCliente;
                    Session["EmailCliente"] = lstCliente.FirstOrDefault().EmailCliente;

                }


                if (Session["CodCliente"] != null) {

                    var lstPublicacion = objLogin.LeeUltimaPublicacion(Convert.ToInt32(codCLiente));


                }





            }
            catch (Exception ex) {



            }
            

            return View("Shared/_Layout");

        }


      


        public JsonResult ListadoPropuesta(string q)
        {

            FichaPropuestaAccess objPropuesta = new FichaPropuestaAccess();

            var vPropuesta = objPropuesta.ObtenerListadoPropuesta_GG();
            return Json(vPropuesta,JsonRequestBehavior.AllowGet);
            
            // objPedido = new Propuesta();
            //var vPedido = objPedido.ObtenerListadoPedido();
            //return Json(vPedido, JsonRequestBehavior.AllowGet);

        }

    }
}