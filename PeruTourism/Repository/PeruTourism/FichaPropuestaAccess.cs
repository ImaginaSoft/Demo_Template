using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using PeruTourism.Models.PeruTourism;
using PeruTourism.Utility;

namespace PeruTourism.Repository.PeruTourism
{
    public class FichaPropuestaAccess
    {

        public IEnumerable<Programa> ObtenerListadoPropuesta(int pNroPedido, char pFlagIdioma)
        {
            try
            {
                List<Programa> lstfprograma = new List<Programa>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    SqlCommand cmd = new SqlCommand();

                    if (pFlagIdioma.Equals(ConstantesWeb.CHR_IDIOMA_INGLES))
                    {
                        cmd = new SqlCommand("P4I_Publica_S", con);

                    }
                    else
                    {
                        cmd = new SqlCommand("P4E_Publica_S", con);
                    }

                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaServicio_S_GG", con);
                    //SqlCommand cmd = new SqlCommand("P4E_Publica_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@CodZonaVta", SqlDbType.Char, 3).Value = "PER";
                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    //cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = 6;

                    //cmd.Parameters.AddWithValue("@NroPedido", 162436);
                    //cmd.Parameters.AddWithValue("@NroPropuesta", 8);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Programa fprograma = new Programa
                        {

                            FchSys = Convert.ToDateTime(rdr["FchSys"].ToString()),
                            NroPrograma = rdr["NroPrograma"].ToString().Trim(),
                            StsPrograma = rdr["StsPrograma"].ToString().Trim(),
                            DesPrograma = rdr["DesPrograma"].ToString().Trim(),
                            CantDias = Convert.ToInt32(rdr["CantDias"]),
                            KeyReg = rdr["KeyReg"].ToString()

                        };

                        lstfprograma.Add(item: fprograma);
                    }

                    con.Close();
                }

                return lstfprograma;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Servicio> ObtenerListadoServiciosPropuesta(int pNroPedido, int pNroPropuesta, char pFlagIdioma)
			
        {
            try
            {
                List<Servicio> lstfservicio = new List<Servicio>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaServicio_S_GG", con);
                    SqlCommand cmd = new SqlCommand("P4I_PropuestaServ_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;
                    cmd.Parameters.Add("@FlagIdioma", SqlDbType.Char).Value = pFlagIdioma;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Servicio fservicio = new Servicio
                        {

                            NroServicio = Convert.ToInt32(rdr["NroServicio"]),
                            DesServicio = rdr["DesServicio"].ToString(),
                            CodTipoServicio = Convert.ToInt32(rdr["CodTipoServicio"]),
                            NroDia = rdr["NroDia"].ToString(),
                            DesServicioDet = rdr["DesServicioDet"].ToString(),
                            Ciudad = rdr["Ciudad"].ToString(),
                            HoraServicio = rdr["HoraServicio"].ToString(),
                            FchInicio = Convert.ToDateTime(rdr["FchInicio"].ToString()),
							NombreEjecutiva = rdr["CodUsuario"].ToString()
							//FchInicio = (rdr["FchInicio"] =null)? string.Empty : Convert.ToDateTime(rdr["FchInicio"].ToString())

						};

                        lstfservicio.Add(item: fservicio);
                    }

                    con.Close();
                }

                return lstfservicio;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IEnumerable<Programa> ObtenerVersion(int pNroPedido, int pNroPropuesta, int pNroVersion)
        {
            try
            {
                List<Programa> lstfprograma = new List<Programa>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaServicio_S_GG", con);
                    SqlCommand cmd = new SqlCommand("P4I_PropuestaNroPropuesta_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Programa fprograma = new Programa
                        {

                            FchSys = Convert.ToDateTime(rdr["FchSys"].ToString()),
                            DesPrograma = rdr["DesPropuesta"].ToString(),
                            CantDias = Convert.ToInt32(rdr["CantDias"]),
                            Resumen = rdr["Resumen"].ToString(),
                            ResumenComida = rdr["ResuComida"].ToString()

                        };

                        lstfprograma.Add(item: fprograma);
                    }

                    con.Close();
                }

                return lstfprograma;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IEnumerable<Propuesta> ObtenerListadoPropuesta_GG()
        {
            try
            {
                List<Propuesta> lstfpropuesta = new List<Propuesta>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaServicio_S_GG", con);
                    SqlCommand cmd = new SqlCommand("VTA_PropuestaNroPedido_S_GG", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = 147140;
                    //cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = 6;

                    //cmd.Parameters.AddWithValue("@NroPedido", 162436);
                    //cmd.Parameters.AddWithValue("@NroPropuesta", 8);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Propuesta fpropuesta = new Propuesta
                        {
                            NroPedido = Convert.ToInt32(rdr["NroPedido"]),
                            NroPropuesta = Convert.ToInt32(rdr["NroPropuesta"]),
                            DesPropuesta = rdr["DesPropuesta"].ToString()
                            //NroDia = Convert.ToInt32(rdr["NroDia"]),
                            //7NroOrden = Convert.ToInt32(rdr["NroOrden"]),
                            //NroServicio = Convert.ToInt32(rdr["NroServicio"]),
                            //DesServicio = rdr["DesServicio"].ToString(),
                            //DesServicioDet = rdr["DesServicioDet"].ToString()
                        };

                        lstfpropuesta.Add(item: fpropuesta);
                    }

                    con.Close();
                }

                return lstfpropuesta;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IEnumerable<Propuesta> ObtenerDetallePropuesta(int nroPedido, int nroPropuesta)
        {
            try
            {
                List<Propuesta> lstpropuestadet = new List<Propuesta>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("VTA_PropuestaServicio_S_GG", con);
                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaNroPedido_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = nroPedido;
                    cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = nroPropuesta;

                    //cmd.Parameters.AddWithValue("@NroPedido", 162436);
                    //cmd.Parameters.AddWithValue("@NroPropuesta", 8);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        Propuesta fpropuesta = new Propuesta
                        {
                            NroPedido = Convert.ToInt32(rdr["NroPedido"]),
                            //NroPropuesta = Convert.ToInt32(rdr["NroPropuesta"]),
                            //DesPropuesta = rdr["DesPropuesta"].ToString()
                            NroDia = Convert.ToInt32(rdr["NroDia"]),
                            NroOrden = Convert.ToInt32(rdr["NroOrden"]),
                            NroServicio = Convert.ToInt32(rdr["NroServicio"]),
                            DesServicio = rdr["DesServicio"].ToString(),
                            DesServicioDet = rdr["DesServicioDet"].ToString()

                        };

                        lstpropuestadet.Add(item: fpropuesta);
                    }

                    con.Close();
                }

                return lstpropuestadet;

            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public string InsertarHistorialCliente(string pDesLog,string pCodCliente,string pNroPedido, string nroPropuesta, string pNroVersion)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("HST_cliente_I", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@DesLog", SqlDbType.Text).Value = pDesLog;
                    cmd.Parameters.Add("@CodCliente", SqlDbType.Int).Value = Convert.ToInt32(pCodCliente);
                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = Convert.ToInt32(pNroPedido);
                    cmd.Parameters.Add("@NroPropuesta", SqlDbType.TinyInt).Value =Convert.ToInt32( nroPropuesta);
                    cmd.Parameters.Add("@NroVersion", SqlDbType.Int).Value = Convert.ToInt32(pNroVersion);

    
                    con.Open();
                    cmd.ExecuteNonQuery();  

                    con.Close();
                }

                return "ok";

            }
            catch (Exception ex)
            {
                throw;
            }
        }




        public IEnumerable<Balance> CargaDocumentos(int pCodCliente, int pNroPedido) {


            try
            {
                List<Balance> lstbalancedet = new List<Balance>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("P4I_MovtosxCliente2_S", con);
                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaNroPedido_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@CodCliente", SqlDbType.Int).Value = pCodCliente;
                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;

                    //cmd.Parameters.AddWithValue("@NroPedido", 162436);
                    //cmd.Parameters.AddWithValue("@NroPropuesta", 8);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        Balance fbalance = new Balance
                        {

                            FchEmision = Convert.ToDateTime(rdr["FchEmision"].ToString()),
                            Referencia = rdr["Referencia"].ToString(),
                            Cargo = Convert.ToDecimal(rdr["Cargo"]),
                            TipoOperacion = rdr["TipoOperacion"].ToString(),
                            Abono = Convert.ToDecimal(rdr["Abono"])

                            //NroPedido = Convert.ToInt32(rdr["NroPedido"]),
                       
                            //NroDia = Convert.ToInt32(rdr["NroDia"]),
                            //NroOrden = Convert.ToInt32(rdr["NroOrden"]),
                            //NroServicio = Convert.ToInt32(rdr["NroServicio"]),
                            //DesServicio = rdr["DesServicio"].ToString(),
                            //DesServicioDet = rdr["DesServicioDet"].ToString()

                        };

                        lstbalancedet.Add(item: fbalance);
                    }

                    con.Close();
                }

                return lstbalancedet;

            }
            catch (Exception ex)
            {
                throw;
            }


        }




    }
}