using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using PeruTourism.Models.PeruTourism;
using PeruTourism.Models.Visa;
using PeruTourism.Utility;
using System.Web.Configuration;
using System.Collections.Specialized;
using System.Xml;
using System.IO;
using PeruTourism.ws_GenereEticket_Visa;
using System.Text;

namespace PeruTourism.Repository.PeruTourism
{
    public class PagoVisaAccess
    {

       

        public readonly WSEticket _wsGeneraEticketVisa = new WSEticket();
        string sFileLogVisa = ((NameValueCollection)WebConfigurationManager.GetSection(ConstantesWeb.strSecureAppSettings))[("RutaLogVisa")];
        string sFlagGrabaLogVisa = ((NameValueCollection)WebConfigurationManager.GetSection(ConstantesWeb.strSecureAppSettings))[("FlagGrabaLogVisa")];
        public string ObtenerListadoPedidoVISA(int pNroPedido) {

            try
            {
                //List<Programa> lstfprograma = new List<Programa>();

                string sIdPedido = string.Empty;

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    SqlCommand cmd = new SqlCommand();

                        cmd = new SqlCommand("VISA_Pedido_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
            
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        sIdPedido = rdr["IdPedido"].ToString().Trim();
                    }

                    con.Close();
                }

                return sIdPedido;

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public IEnumerable<PedidoVisa> CargarPedidoVisa(int pNroPedido) {

            try
            {
                var lstPedidoVisa = new List<PedidoVisa>();

                string sIdPedido = string.Empty;

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd = new SqlCommand("VISA_Pedido_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        PedidoVisa pedidoVisa = new PedidoVisa
                        {
                            NomCliente = rdr["NomCliente"].ToString(),
                            DesPedido = rdr["DesPedido"].ToString(),
                            Idioma = rdr["Idioma"].ToString(),
                            IDPedido = rdr["IDPedido"].ToString()
                        };
                        //pedido.CodVendedor = Convert.ToChar(GetValue(reader, "CodVendedor"));

                        lstPedidoVisa.Add(item :pedidoVisa);
                    }

                    con.Close();
                }

                return lstPedidoVisa;

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public string InsertarOrdenpagoVISA(int pMonto, string pIdPedido)
        {
            string lblMsg = string.Empty;
            try
            {
                string iCODTIENDA = ((NameValueCollection)WebConfigurationManager.GetSection(ConstantesWeb.strSecureAppSettings))[("CodComercioVisaNet")];
                int iNUMORDEN = 0;
                string sEticket = string.Empty;
                string sURL_FORMULARIO_VISA = string.Empty;
                string modulo = pIdPedido.Substring(10,3);                
                string nroPedido =pIdPedido.Substring(13,6);

                //0438012470P4M158316
                //ViewState("Modulo") = Mid(ViewState("ID"), 11, 3)
                //ViewState("NroPedido") = Mid(ViewState("ID"), 14, 10)

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd = new SqlCommand("VISA_OrdenPago_I", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pa = new SqlParameter();

                    pa = cmd.Parameters.Add("@MsgTrans", SqlDbType.VarChar, 150);
                    pa.Direction = ParameterDirection.Output;
                    pa.Value = string.Empty;

                    pa = cmd.Parameters.Add("@NroOrdenPagoOut", SqlDbType.Int);

                    pa.Direction = ParameterDirection.Output;
                    pa.Value = 0;


                    cmd.Parameters.Add("@CodComercio", SqlDbType.Int).Value = iCODTIENDA;
                    cmd.Parameters.Add("@MonOrdenPago", SqlDbType.Money).Value = pMonto;
                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = nroPedido;
                    cmd.Parameters.Add("@CodUsuario", SqlDbType.VarChar, 15).Value = modulo;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    lblMsg = cmd.Parameters["@MsgTrans"].Value.ToString();
                    iNUMORDEN = Convert.ToInt32( cmd.Parameters["@NroOrdenPagoOut"].Value);

                    //SqlDataReader rdr = cmd.ExecuteReader();


                    con.Close();

                    if (lblMsg.Trim() == "OK") {
                        //INICIO - ETAPA 1 Registro del Pedido y Generacion del ETicket


                        sEticket = GeneraTicket(iNUMORDEN, pMonto, pIdPedido);

                        if ( sEticket.Trim().Length >=5) {

                            if (sEticket.Substring(0,5) == "Error") {

                                lblMsg = "Error 3 : " + sEticket;

                            }
                            else {
                                GrabaETicket(iNUMORDEN, sEticket);
                            }

                        }

                    }

                    if (lblMsg.Trim() == "OK") {

                        lblMsg = string.Empty;

                        sURL_FORMULARIO_VISA = ((NameValueCollection)WebConfigurationManager.GetSection(ConstantesWeb.strSecureAppSettings))[("URL_FORMULARIO_VISA")];

                        GrabaLog(sURL_FORMULARIO_VISA, sFlagGrabaLogVisa);

                    }

                }

            }
            catch (Exception ex)
            {

                throw;
            }

            return lblMsg;
        }



        public string GeneraTicket(int pNumOrden, int pMonto,string pIdPedido) {


            WSEticket _wsGeneraEticketVisaGG = new WSEticket();

            string Eticket = string.Empty;
            string numPedido = pNumOrden.ToString();
            string codTienda = ((NameValueCollection)WebConfigurationManager.GetSection(ConstantesWeb.strSecureAppSettings))[("CodComercioVisaNet")];
            string mount = pMonto.ToString();
            string modulo = pIdPedido.Substring(10, 3);
            string nroPedido = pIdPedido.Substring(13, 6);

            StringBuilder cData = new StringBuilder();
            string xmlReq = string.Empty;

            cData.Append("<nuevo_eticket>");
            cData.Append("<parametros>");
            cData.AppendFormat("<parametro id=\"CANAL\">{0}</parametro>", 3);
            cData.AppendFormat("<parametro id=\"PRODUCTO\">{0}</parametro>", 1);
            cData.AppendFormat("<parametro id=\"CODTIENDA\">{0}</parametro>", codTienda);
            cData.AppendFormat("<parametro id=\"NUMORDEN\">{0}</parametro>", numPedido);
            cData.AppendFormat("<parametro id=\"MOUNT\">{0}</parametro>", mount);
            cData.AppendFormat("<parametro id=\"DATO_COMERCIO\">{0}</parametro>", modulo);
            cData.Append("</parametros>");
            cData.Append("</nuevo_eticket>");

            //cData.Append("<nuevo_eticket>");
            //cData.Append("<nuevo_eticket>");
            //xmlReq = (xmlReq + "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            xmlReq = (xmlReq + "<nuevo_eticket>");
            xmlReq = (xmlReq + " <parametros>");
            xmlReq = (xmlReq + "     <parametro id=\"CANAL\">3</parametro>");
            xmlReq = (xmlReq + "     <parametro id=\"PRODUCTO\">1</parametro>");
            xmlReq = (xmlReq + "     ");
            xmlReq = (xmlReq + ("		<parametro id=\"CODTIENDA\">"+ (codTienda + "</parametro>")));
            xmlReq = (xmlReq + ("		<parametro id=\"NUMORDEN\">"+ (numPedido + "</parametro>")));
            xmlReq = (xmlReq + ("		<parametro id=\"MOUNT\">"+ (mount + "</parametro>")));
            xmlReq = (xmlReq + ("		<parametro id=\"DATO_COMERCIO\">"+ (modulo + "</parametro>")));
            xmlReq = (xmlReq + " </parametros>");
            xmlReq = (xmlReq + "</nuevo_eticket>");

            //xmlReq = xmlReq + "<?xml version=""1.0"" encoding=""UTF-8"" ?>";
            //xmlReq = xmlReq + "<nuevo_eticket>";
            //    xmlReq = xmlReq + "	<parametros>";
            //xmlReq = xmlReq + "		<parametro id=""CANAL"">3</parametro>";
            //xmlReq = xmlReq + "		<parametro id=""PRODUCTO"">1</parametro>";
            //xmlReq = xmlReq + "		"
            //xmlReq = xmlReq + "		<parametro id=""CODTIENDA"">" + codTienda + "</parametro>"
            //xmlReq = xmlReq + "		<parametro id=""NUMORDEN"">" + numPedido + "</parametro>"
            //xmlReq = xmlReq + "		<parametro id=""MOUNT"">" + mount + "</parametro>"
            //xmlReq = xmlReq + "		<parametro id=""DATO_COMERCIO"">" + modulo + "</parametro>"
            //xmlReq = xmlReq + "	</parametros>"
            //xmlReq = xmlReq + "</nuevo_eticket>";



            try
            {

                string servicioUrl = ((NameValueCollection)WebConfigurationManager.GetSection(ConstantesWeb.strSecureAppSettings))[("URL_WSGENERAETICKET_VISA")];
                _wsGeneraEticketVisa.Url = servicioUrl;
                

                string xmlRes = _wsGeneraEticketVisaGG.GeneraEticket(cData.ToString());

                XmlDocument xmlDocument = new XmlDocument();

                xmlDocument.LoadXml(xmlRes);

                int iCantMensajes=CantidadMensajes (xmlDocument);

                // Grabar el log 

                GrabaLog("iCantMensajes = " + iCantMensajes.ToString(), sFlagGrabaLogVisa);


                int iNumMensaje = 0;

                for (iNumMensaje = 0; (iNumMensaje<= (iCantMensajes - 1)); iNumMensaje++)
                {
                    GrabaLog(("Mensaje #"
                                    + ((iNumMensaje + 1)
                                    + ": ")), sFlagGrabaLogVisa);
                    GrabaLog(RecuperaMensaje(xmlDocument, (iNumMensaje + 1)), sFlagGrabaLogVisa);
                }


                if (iCantMensajes == 0) {


                    Eticket = RecuperaEticket(xmlDocument);
                    GrabaLog("Eticket =" + Eticket, sFlagGrabaLogVisa);

                }

            }
            catch (Exception ex) {
                Eticket = "Error GeneraTicket :" + ex.ToString();

                GrabaLog(Eticket, "S");

            }   

            return Eticket;

        }


        private void GrabaETicket(int pNroOrdenPago,string pETicket ) {

            string lblMsg = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd = new SqlCommand("VISA_OrdenPagoETicket_U", con);
                    cmd.CommandType = CommandType.StoredProcedure;



                    SqlParameter pa = new SqlParameter();

                    pa = cmd.Parameters.Add("@MsgTrans", SqlDbType.VarChar, 150);
                    pa.Direction = ParameterDirection.Output;
                    pa.Value = string.Empty;

                    cmd.Parameters.Add("@NroOrdenPago", SqlDbType.Int).Value = pNroOrdenPago;
                    cmd.Parameters.Add("@ETicket", SqlDbType.VarChar, 30).Value = pETicket;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    lblMsg = cmd.Parameters["@MsgTrans"].Value.ToString();

                    con.Close();
                }
            }
            catch (Exception ex) {

                lblMsg = "Error 4:" + ex.Message;
                GrabaLog(lblMsg, "S");
            }
        }



        public int CantidadMensajes(XmlDocument xmlDoc) {

            int cantMensajes = 0;
            XmlNodeList nodeList;
            XmlNode XmlNode;

            nodeList = xmlDoc.SelectNodes("//mensajes");    
            XmlNode = nodeList.Item(0);

            if (XmlNode != null) {

                cantMensajes = 0;

            }
            else{
                cantMensajes = XmlNode.ChildNodes.Count;


            }
           
            return cantMensajes;
        }

        private string RecuperaMensaje(XmlDocument xmlDoc, int iNumMensaje) {

            string strReturn = string.Empty;
            XmlNodeList nodeList;
            XmlNode xmlNode;

            nodeList = xmlDoc.SelectNodes(("//mensajes/mensaje[@id=\""
                + (iNumMensaje + "\"]")));

            xmlNode = nodeList.Item(0).FirstChild;

            if (xmlNode != null) {
                strReturn = string.Empty;
            }
            else {
                strReturn = xmlNode.Value;
            }

            return strReturn;

        }

        private string RecuperaEticket(XmlDocument xmlDoc) {


            string strReturn = string.Empty;
            XmlNodeList nodeList;
            XmlNode xmlNode;

            nodeList = xmlDoc.SelectNodes("/registro/campo[@id='ETICKET']");

            xmlNode = nodeList.Item(0).FirstChild;

            if (xmlNode != null) {
                strReturn = string.Empty;

            }
            else {
                strReturn = xmlNode.Value;
            }

            return strReturn;
        }


        public void GrabaLog(string pMensaje, string pGrabaLogVisa) {

            if (pGrabaLogVisa == "S") {

                FileStream fs = null;
                FileStream s = null;

                if (!File.Exists(sFileLogVisa)) {

                    fs = File.Create(sFileLogVisa);

                }

                using (StreamWriter sw = new StreamWriter(sFileLogVisa,true))
                {
                    sw.WriteLine(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "   " +pMensaje);
                }

            }


        }

    }
}