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

        public ActionResult Galeria(int pIdServicio)
        {

            string sIdServicio = string.Empty;

            var lstGaleria = objGaleriaAccess.CargarGaleria(pIdServicio);

            objGaleriaViewModel.lstGaleria = lstGaleria.ToList();

            sIdServicio = lstGaleria.FirstOrDefault().NroServicio.Trim();

            int sIdServicio_length = sIdServicio.Length;

         

            return View(objGaleriaViewModel);

         

        }



        //public ActionResult OpenModalDetailsRooms(string NroServicio)
        //{

        //    try
        //    {
        //        HotelAvailRS objHotelViewModelGg = ConvertStringToRoomViewModel(pHotelViewModelGG);

        //        HotelViewModel objHotelViewModel = ConvertStringToHotelViewModel(pHotelViewModel);

        //        List<HotelAvailRS> listaHoteles = objHotelViewModel.objSearchAvailableHotelRS.ListHotels.ToList();

        //        string[] strIds = HotelRoomId.Split('-');

        //        var objHotel = listaHoteles.FirstOrDefault(x => x.HotelID == strIds[0].Trim());

        //        ws_MotorHoteles.RoomAvailRS objSeletedRoom = new ws_MotorHoteles.RoomAvailRS();

        //        foreach (var itemRoomGroup in objHotelViewModelGg.RoomGroup)
        //        {
        //            var objRoomTriPoint = itemRoomGroup.Item.Where(y => y.RoomID == strIds[1].Trim()).ToList();
        //            if (objRoomTriPoint.Count != 0)
        //            {
        //                if (strIds[2].Trim() == "tourico")
        //                {
        //                    objSeletedRoom = objRoomTriPoint.FirstOrDefault(z => z.DataReserveRoomID == strIds[2].Trim() + "-" + strIds[3].Trim());

        //                }
        //                else
        //                {

        //                    objSeletedRoom = objRoomTriPoint.FirstOrDefault(z => z.DataReserveRoomID == strIds[2].Trim());
        //                }


        //            }
        //        }

        //        if (objHotel != null)
        //        {
        //            objHotelViewModel.HotelSelected = new HotelSelectedRQ()
        //            {
        //                HotelID = objHotel.HotelID,
        //                Address = objHotel.Address,
        //                DestinationID = objHotel.DestinationID,
        //                CountryName = objHotel.CountryName,
        //                RegionName = objHotel.RegionName,
        //                Category = objHotel.Category,
        //                Provider = objHotel.Provider,
        //                Key = objHotel.Key,
        //                RoomSelected = objSeletedRoom
        //            };
        //        }


        //        return PartialView("~/Views/PaqueteCostum/VuelosHoteles/Hoteles/_ModalDetalleRooms.cshtml", objHotelViewModel);
        //    }
        //    catch (Exception)
        //    {
        //        return Redirect("paquetes");
        //    }
        //}












        }
}