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
                            KeyReg = rdr["KeyReg"].ToString(),
                            EmailVendedor = rdr["EmailVendedor"].ToString()

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
							NombreEjecutiva = rdr["CodUsuario"].ToString(),
                            Resumen = rdr["Resumen"].ToString(),
                            ResuCaraEspe = rdr["ResuCaraEspe"].ToString(),
                            ResuComida = rdr["ResuComida"].ToString()
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



        public IEnumerable<Servicio> ObtenerListadoServiciosPropuestaVersion(int pNroPedido, int pNroPropuesta,int pNroVersion, char pFlagIdioma)

        {
            try
            {
                List<Servicio> lstfservicio = new List<Servicio>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaServicio_S_GG", con);
                    SqlCommand cmd = new SqlCommand("P4I_PropuestaServVersion_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;
                    cmd.Parameters.Add("@NroVersion", SqlDbType.Int).Value = pNroVersion;
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
                            NombreEjecutiva = rdr["CodUsuario"].ToString(),
                            Resumen = rdr["Resumen"].ToString(),
                            ResuCaraEspe = rdr["ResuCaraEspe"].ToString(),
                            ResuComida = rdr["ResuComida"].ToString()
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


        public IEnumerable<PropuestaPrecio> CargaPropuestaPrecio(int pNroPedido,int pNroPropuesta)
        {


            try
            {
                List<PropuestaPrecio> lstpropuestaprecio = new List<PropuestaPrecio>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("P4I_PropuestaPrecio_S", con);
                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaNroPedido_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;

                    //cmd.Parameters.AddWithValue("@NroPedido", 162436);
                    //cmd.Parameters.AddWithValue("@NroPropuesta", 8);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        PropuestaPrecio fprograma = new PropuestaPrecio
                        {

                            DesOrden = rdr["DesOrden"].ToString(),
                            PrecioxPersona = Convert.ToDecimal(rdr["PrecioxPersona"].ToString()),
                            CantPersonas = Convert.ToInt32(rdr["CantPersonas"].ToString()),
                            PrecioTotal = Convert.ToDecimal(rdr["PrecioTotal"].ToString())
                       

                            //NroPedido = Convert.ToInt32(rdr["NroPedido"]),

                            //NroDia = Convert.ToInt32(rdr["NroDia"]),
                            //NroOrden = Convert.ToInt32(rdr["NroOrden"]),
                            //NroServicio = Convert.ToInt32(rdr["NroServicio"]),
                            //DesServicio = rdr["DesServicio"].ToString(),
                            //DesServicioDet = rdr["DesServicioDet"].ToString()

                        };

                        lstpropuestaprecio.Add(item: fprograma);
                    }

                    con.Close();
                }

                return lstpropuestaprecio;

            }
            catch (Exception ex)
            {
                throw;
            }


        }


        //*****OPCION ESTADO DE TU RESERVA******//

        public IEnumerable<Pasajero> CargaPasajero(int pNroPedido)
        {

            try
            {
                List<Pasajero> lstPasajero = new List<Pasajero>();
                var dt = new DataTable();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("P4I_Pasajeros_S", con);
              
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;

                    con.Open();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();

                    lstPasajero = dt.AsEnumerable().Select(x => new Pasajero
                    {
                        NomPasajero = x.Field<string>("NomPasajero"),
                        ApePasajero = x.Field<string>("ApePasajero"),
                        FchNacimiento = Convert.ToDateTime(x.Field<string>("FchNacimientoStr")),
                        Pasaporte = x.Field<string>("Pasaporte")
                    })
                    .ToList();
                }

                return lstPasajero;

            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public IEnumerable<ReservaAereo> CargaTerrestre(int pNroPedido, int pNroPropuesta, int pNroVersion)
        {

            try
            {
                List<ReservaAereo> lstReservaTerrestre = new List<ReservaAereo>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("P4I_ReservaAereo_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;
                    cmd.Parameters.Add("@NroVersion", SqlDbType.Int).Value = pNroVersion;
                    cmd.Parameters.Add("@CodTipoServicio", SqlDbType.Int).Value = 3;


                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        ReservaAereo fterrestre = new ReservaAereo
                        {

                            FchVuelo = Convert.ToDateTime(rdr["FchVuelo"].ToString()),
                            AereoLinea = rdr["Aerolinea"].ToString().Trim(),
                            RutaVuelo = rdr["RutaVuelo"].ToString().Trim(),
                            NroVuelo = rdr["NroVuelo"].ToString().Trim(),
                            HoraSalida = rdr["HoraSalida"].ToString().Trim(),
                            HoraLlegada = rdr["HoraLlegada"].ToString().Trim(),
                            CodReserva = rdr["CodReserva"].ToString().Trim(),
                            DesCantidad = rdr["DesCantidad"].ToString().Trim(),
                            CodStsReserva = rdr["CodStsReserva"].ToString().Trim(),
                            NroDia = Convert.ToInt32(rdr["NroDia"].ToString()),
                            NroOrden = Convert.ToInt32(rdr["NroOrden"].ToString())



                        };

                        lstReservaTerrestre.Add(item: fterrestre);
                    }

                    con.Close();
                }

                return lstReservaTerrestre;

            }
            catch (Exception ex)
            {
                throw;
            }


        }


        public IEnumerable<ReservaAereo> CargaAereo(int pNroPedido, int pNroPropuesta, int pNroVersion)
        {


            try
            {
                List<ReservaAereo> lstReservaAereo = new List<ReservaAereo>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("P4I_ReservaAereo_S", con);
                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaNroPedido_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;
                    cmd.Parameters.Add("@NroVersion", SqlDbType.Int).Value = pNroVersion;
                    cmd.Parameters.Add("@CodTipoServicio", SqlDbType.Int).Value = 1;

                    //cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;

                    //cmd.Parameters.AddWithValue("@NroPedido", 162436);
                    //cmd.Parameters.AddWithValue("@NroPropuesta", 8);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        ReservaAereo faereo = new ReservaAereo
                        {

                            FchVuelo = Convert.ToDateTime(rdr["FchVuelo"].ToString()),
                            AereoLinea = rdr["Aerolinea"].ToString(),
                            RutaVuelo = rdr["RutaVuelo"].ToString(),
                            NroVuelo = rdr["NroVuelo"].ToString(),
                            HoraSalida = rdr["HoraSalida"].ToString(),
                            HoraLlegada = rdr["HoraLlegada"].ToString(),
                            CodReserva = rdr["CodReserva"].ToString(),
                            DesCantidad = rdr["DesCantidad"].ToString(),
                            CodStsReserva = rdr["CodStsReserva"].ToString(),
                            NroDia = Convert.ToInt32(rdr["NroDia"].ToString()),
                            NroOrden = Convert.ToInt32(rdr["NroOrden"].ToString())



                        };

                        lstReservaAereo.Add(item: faereo);
                    }

                    con.Close();
                }

                return lstReservaAereo;

            }
            catch (Exception ex)
            {
                throw;
            }


        }



        public IEnumerable<ReservaHotel> CargaHotel(int pNroPedido, int pNroPropuesta, int pNroVersion)
        {


            try
            {
                List<ReservaHotel> lstReservaHotel = new List<ReservaHotel>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("P4I_ReservaHotelFact_S", con);
                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaNroPedido_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;
                    cmd.Parameters.Add("@NroVersion", SqlDbType.Int).Value = pNroVersion;
                   

                    //cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;

                    //cmd.Parameters.AddWithValue("@NroPedido", 162436);
                    //cmd.Parameters.AddWithValue("@NroPropuesta", 8);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        ReservaHotel fhotel = new ReservaHotel
                        {

                            NomCiudad = rdr["NomCiudad"].ToString(),
                            NombreHotel = rdr["Hotel"].ToString(),
                            Telefono = rdr["Telefono1"].ToString(),
                            StsReserva = rdr["StsReserva"].ToString(),
                            HotelAlternativo = rdr["HotelAlternativo"].ToString(),
                            



                        };

                        lstReservaHotel.Add(item: fhotel);
                    }

                    con.Close();
                }

                return lstReservaHotel;

            }
            catch (Exception ex)
            {
                throw;
            }


        }





        public IEnumerable<VersionFacturada> VersionFacturada(int pNroPedido)
        {
            string lblMsg = string.Empty;
            try
            {
                List<VersionFacturada> lstVersionFacturada = new List<VersionFacturada>();
                int iNUMORDEN = 0;
                string sEticket = string.Empty;
                string sURL_FORMULARIO_VISA = string.Empty;

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd = new SqlCommand("P4I_VersionFact_S", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pa = new SqlParameter();

                    pa = cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int);
                    pa.Direction = ParameterDirection.Output;
                    pa.Value = string.Empty;

                    pa = cmd.Parameters.Add("@NroVersion", SqlDbType.Int);

                    pa.Direction = ParameterDirection.Output;
                    pa.Value = 0;

                     
                    pa = cmd.Parameters.Add("@FlagIdioma", SqlDbType.Char, 1);

                    pa.Direction = ParameterDirection.Output;
                    pa.Value = 0;


                    pa = cmd.Parameters.Add("@CantDias", SqlDbType.Int);

                    pa.Direction = ParameterDirection.Output;
                    pa.Value = 0;


                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    //cmd.Parameters.Add("@MonOrdenPago", SqlDbType.Money).Value = pMonto;
                    //cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = nroPedido;
                    //cmd.Parameters.Add("@CodUsuario", SqlDbType.VarChar, 15).Value = modulo;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    //lblMsg = cmd.Parameters["@MsgTrans"].Value.ToString();
                    //iNUMORDEN = Convert.ToInt32(cmd.Parameters["@NroOrdenPagoOut"].Value);

                    //SqlDataReader rdr = cmd.ExecuteReader()

                    VersionFacturada fversionfacturada = new VersionFacturada
                    {

                        NroPropuesta= Convert.ToInt32(cmd.Parameters["@NroPropuesta"].Value),
                        NroVersion = Convert.ToInt32(cmd.Parameters["@NroVersion"].Value),
                        FlagIdioma =Convert.ToChar( cmd.Parameters["@FlagIdioma"].Value),
                        CantDias = Convert.ToInt32(cmd.Parameters["@CantDias"].Value)

                    };

                    lstVersionFacturada.Add(item: fversionfacturada);


                    con.Close();

              

                }
                return lstVersionFacturada;
            }
            catch (Exception ex)
            {

                throw;
            }

         
        }

        //*****OPCION ANTES DE TU VIAJE******//

        public IEnumerable<ReservaHotel> CargaHotelInfoAntes(int pNroPedido, int pNroPropuesta, int pNroVersion)
        {

            try
            {
                List<ReservaHotel> lstReservaHotel = new List<ReservaHotel>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("P4I_VersionHotel_S", con);
                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaNroPedido_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;
                    cmd.Parameters.Add("@NroVersion", SqlDbType.Int).Value = pNroVersion;


                    //cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;

                    //cmd.Parameters.AddWithValue("@NroPedido", 162436);
                    //cmd.Parameters.AddWithValue("@NroPropuesta", 8);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        ReservaHotel fhotel = new ReservaHotel
                        {

                            NomCiudad = rdr["NomCiudad"].ToString(),
                            NombreHotel = rdr["Titulo"].ToString(),
                            Telefono = rdr["Telefono1"].ToString(),
                            NomPagina = rdr["NomPagina"].ToString()




                        };

                        lstReservaHotel.Add(item: fhotel);
                    }

                    con.Close();
                }

                return lstReservaHotel;

            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public IEnumerable<Staff> CargaStaffInfoAntes(int pNroPedido, char pFlagIdioma)
        {


            try
            {
                List<Staff> lstStaff = new List<Staff>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("P4I_InformacionStaff_S", con);
                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaNroPedido_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    cmd.Parameters.Add("@FlagIdioma", SqlDbType.Char).Value = pFlagIdioma;
                    //cmd.Parameters.Add("@NroVersion", SqlDbType.Int).Value = pNroVersion;


                    //cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;

                    //cmd.Parameters.AddWithValue("@NroPedido", 162436);
                    //cmd.Parameters.AddWithValue("@NroPropuesta", 8);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        Staff fstaff = new Staff
                        {


                            NomVendedor = rdr["NomVendedor"].ToString(),
                            Cargo = rdr["Cargo"].ToString(),
                            TelefonoEmergencia = rdr["TfEmergencia"].ToString()


                        };

                        lstStaff.Add(item: fstaff);
                    }

                    con.Close();
                }

                return lstStaff;

            }
            catch (Exception ex)
            {
                throw;
            }


        }


        public IEnumerable<Video> CargaVideoInfoAntes(int pNroPedido, char pFlagIdioma)
        {


            try
            {
                List<Video> lstVideo = new List<Video>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("P4I_InformacionVideo_S", con);
                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaNroPedido_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    cmd.Parameters.Add("@FlagIdioma", SqlDbType.Char).Value = pFlagIdioma;
                    //cmd.Parameters.Add("@NroVersion", SqlDbType.Int).Value = pNroVersion;


                    //cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;

                    //cmd.Parameters.AddWithValue("@NroPedido", 162436);
                    //cmd.Parameters.AddWithValue("@NroPropuesta", 8);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        Video fvideo = new Video
                        {


                            VideoUrl = rdr["VideoURL"].ToString(),
                            VideoTitulo = rdr["VideoTitulo"].ToString()


                        };

                        lstVideo.Add(item: fvideo);
                    }

                    con.Close();
                }

                return lstVideo;

            }
            catch (Exception ex)
            {
                throw;
            }


        }


        public IEnumerable<Clima> CargaClimaInfoAntes(int pNroPedido, int pNroPropuesta,int pNroVersion)
        {


            try
            {
                List<Clima> lstClima = new List<Clima>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("P4I_VersionInformacionClima_S", con);
                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaNroPedido_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;
                    cmd.Parameters.Add("@NroVersion", SqlDbType.Int).Value = pNroVersion;


                    //cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;

                    //cmd.Parameters.AddWithValue("@NroPedido", 162436);
                    //cmd.Parameters.AddWithValue("@NroPropuesta", 8);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        Clima fclima = new Clima
                        {

                            NomCiudad = rdr["NomCiudad"].ToString(),
                            MesAño = rdr["MesAno"].ToString(),
                            TempMinima = rdr["TempMinima"].ToString(),
                            TempMaxima = rdr["TempMaxima"].ToString()
                            


                        };

                        lstClima.Add(item: fclima);
                    }

                    con.Close();
                }

                return lstClima;

            }
            catch (Exception ex)
            {
                throw;
            }


        }


        public string CargaDocReqInfoAntes(int pNroPedido, char pFlagIdioma)
        {


            try
            {
                List<Clima> lstClima = new List<Clima>();

                string nomInfo = string.Empty;

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("P4I_InformacionDocReq_S", con);
                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaNroPedido_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    cmd.Parameters.Add("@FlagIdioma", SqlDbType.Char,1).Value = pFlagIdioma;
                    //cmd.Parameters.Add("@NroVersion", SqlDbType.Int).Value = pNroVersion;


                    //cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;

                    //cmd.Parameters.AddWithValue("@NroPedido", 162436);
                    //cmd.Parameters.AddWithValue("@NroPropuesta", 8);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        nomInfo = rdr["NomInf"].ToString();
                    }
                    //while (rdr.Read())
                    //{

                    //    Clima fclima = new Clima
                    //    {

                    //        NomCiudad = rdr["NomCiudad"].ToString(),
                    //        MesAño = rdr["MesAno"].ToString(),
                    //        TempMinima = rdr["TempMinima"].ToString(),
                    //        TempMaxima = rdr["TempMaxima"].ToString()



                    //    };

                    //    lstClima.Add(item: fclima);
                    //}

                    con.Close();
                }

                return nomInfo;

            }
            catch (Exception ex)
            {
                throw;
            }


        }


		public IEnumerable<Programa> ObtenerListadoPasajero(int pNroPedido)
		{
			try
			{
				List<Programa> lstfprograma = new List<Programa>();

				using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
				{
					SqlCommand cmd = new SqlCommand();

					cmd = new SqlCommand("VTA_PasajeroNroPedido_S", con);
					
					cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
				
					con.Open();
					cmd.ExecuteNonQuery();
					SqlDataReader rdr = cmd.ExecuteReader();

					//while (rdr.Read())
					//{
					//	Programa fprograma = new Programa
					//	{

					//		FchSys = Convert.ToDateTime(rdr["FchSys"].ToString()),
					//		NroPrograma = rdr["NroPrograma"].ToString().Trim(),
					//		StsPrograma = rdr["StsPrograma"].ToString().Trim(),
					//		DesPrograma = rdr["DesPrograma"].ToString().Trim(),
					//		CantDias = Convert.ToInt32(rdr["CantDias"]),
					//		KeyReg = rdr["KeyReg"].ToString()

					//	};

					//	lstfprograma.Add(item: fprograma);
					//}

					con.Close();
				}

				return lstfprograma;

			}
			catch (Exception ex)
			{
				throw;
			}
		}













	}
}