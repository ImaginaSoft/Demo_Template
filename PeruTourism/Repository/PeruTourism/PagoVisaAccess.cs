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


namespace PeruTourism.Repository.PeruTourism
{
    public class PagoVisaAccess
    {
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

        public string InsertarOrdenpagoVISA(int monto)
        {

            try
            {
                string iCODTIENDA = ((NameValueCollection)WebConfigurationManager.GetSection(ConstantesWeb.strSecureAppSettings))[("CodComercioVisaNet")];

                string lblMsg = string.Empty;

                int iNUMORDEN = 0;
                string sEticket = string.Empty;

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
                    cmd.Parameters.Add("@MonOrdenPago", SqlDbType.Money).Value = iCODTIENDA;
                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = iCODTIENDA;
                    cmd.Parameters.Add("@CodUsuario", SqlDbType.VarChar, 15).Value = iCODTIENDA;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    lblMsg = cmd.Parameters["@MsgTrans"].Value.ToString();
                    iNUMORDEN = Convert.ToInt32( cmd.Parameters["@NroOrdenPagoOut"].Value);

                    //SqlDataReader rdr = cmd.ExecuteReader();



                    if (lblMsg.Trim() == "OK") {
                        //INICIO - ETAPA 1 Registro del Pedido y Generacion del ETicket


                        sEticket = GeneraTicket(iNUMORDEN, monto);

                        if ( sEticket.Trim().Length >=5) {

                            if (sEticket.Substring(0,5) == "Error") {

                                lblMsg = "Error 3 : " + sEticket;

                            }
                            else {
                                //GrabaETicket(iNUMORDEN, sEticket);
                            }




                        }




                    }




                    //while (rdr.Read())
                    //{
                    //    PedidoVisa pedidoVisa = new PedidoVisa
                    //    {
                    //        NomCliente = rdr["NomCliente"].ToString(),
                    //        DesPedido = rdr["DesPedido"].ToString(),
                    //        Idioma = rdr["Idioma"].ToString(),
                    //        IDPedido = rdr["IDPedido"].ToString()
                    //    };
                    //    //pedido.CodVendedor = Convert.ToChar(GetValue(reader, "CodVendedor"));

                    //    lstPedidoVisa.Add(item: pedidoVisa);
                    //}

                    con.Close();
                }

            }
            catch (Exception ex)
            {

                throw;
            }



            return "";
        }



        public string GeneraTicket(int pNumOrden, int pMonto) {

            string Eticket = string.Empty;

            string numPedido = pNumOrden.ToString();

            string codTienda = ((NameValueCollection)WebConfigurationManager.GetSection(ConstantesWeb.strSecureAppSettings))[("CodComercioVisaNet")];

            string mount = pMonto.ToString();

            return "";



        }


    }
}