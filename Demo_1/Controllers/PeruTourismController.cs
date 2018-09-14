using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

            //string sParamID = Request.QueryString["id"];

            string idCliente = userId;
            string codCLiente = userId.Substring(7, 10);
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



                }





            }
            catch (Exception ex) {



            }


            return View();

        }





        public JsonResult ListadoPropuesta(string q)
        {

            FichaPropuestaAccess objPropuesta = new FichaPropuestaAccess();

            var vPropuesta = objPropuesta.ObtenerListadoPropuesta();
            return Json(vPropuesta,JsonRequestBehavior.AllowGet);
            
            // objPedido = new Propuesta();
            //var vPedido = objPedido.ObtenerListadoPedido();
            //return Json(vPedido, JsonRequestBehavior.AllowGet);

        }

    }
}