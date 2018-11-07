using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeruTourism.Repository.PeruTourism;
using PeruTourism.Utility;
using PeruTourism.Models.Galeria;
using System.Data.SqlClient;
using System.Data;


namespace PeruTourism.Controllers
{
    public class GaleriaController : Controller
    {
        GaleriaAccess objGaleriaAccess = new GaleriaAccess();
        GaleriaViewModel objGaleriaViewModel = new GaleriaViewModel();

        // GET: Galeria
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Galeria(int pIdServicio)
        //{

        //    string sIdServicio = string.Empty;

        //    var lstGaleria = objGaleriaAccess.CargarGaleria(pIdServicio);

        //    objGaleriaViewModel.lstGaleria = lstGaleria.ToList();

        //    sIdServicio = lstGaleria.FirstOrDefault().NroServicio.Trim();

        //    int sIdServicio_length = sIdServicio.Length;

         

        //    return View(objGaleriaViewModel);

         

        //}



        //[HttpPost]
        //public ActionResult OpenModalDetailsHotel(string pNroServicio)
        //{
        //    try
        //    {


        //        string sIdServicio = string.Empty;

        //        var lstGaleria = objGaleriaAccess.CargarGaleria(Convert.ToInt32(pNroServicio));

        //        objGaleriaViewModel.lstGaleria = lstGaleria.ToList();

        //        sIdServicio = lstGaleria.FirstOrDefault().NroServicio.Trim();

        //        int sIdServicio_length = sIdServicio.Length;




        //        return PartialView("~/Views/Galeria/_ModalDetalleHotel.cshtml", objGaleriaViewModel);
        //        //return PartialView("~/Views/PaqueteCostum/VuelosHoteles/Hoteles/_ModalDetalleRooms.cshtml", objHotelViewModel);
        //    }
        //    catch (Exception)
        //    {

        //        return View("~/Views/Shared/Error.cshtml");
        //        //return Redirect("paquetes");
        //    }
        //}












    }
}