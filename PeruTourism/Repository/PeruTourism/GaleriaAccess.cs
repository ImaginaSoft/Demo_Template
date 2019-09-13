using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using PeruTourism.Models.PeruTourism;
using PeruTourism.Models.Galeria;
using PeruTourism.Utility;
using System.Web.Configuration;
using System.Collections.Specialized;


namespace PeruTourism.Repository.PeruTourism
{
    public class GaleriaAccess
    {
        public string ObtenerListadoGaleria(int pNroServicio) {

            try
            {
                //List<Programa> lstfprograma = new List<Programa>();

                string sIdServicio = string.Empty;

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    SqlCommand cmd = new SqlCommand();

                        cmd = new SqlCommand("peru4me_new.VTA_ListaIMG_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.Add("@NroServicio", SqlDbType.Int).Value = pNroServicio;
            
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        sIdServicio = rdr["NroServicio"].ToString().Trim();
                    }

                    con.Close();
                }

                return sIdServicio;

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public IEnumerable<Servicio> CargarGaleria(int sNroServicio)
        {
            try
            {
                var lstGaleria = new List<Servicio>();

                string sIdGaleria = string.Empty;

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd = new SqlCommand("peru4me_new.VTA_ListaIMG_S", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@NroServicio", SqlDbType.Int).Value = sNroServicio;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                       
                        Servicio galeria = new Servicio
                        {
                            //Imagen1 = Convert.FromBase64String("Imagen2"),
                            Imagen1 = (byte[]) rdr["Imagen1"],
                            Imagen2 = (byte[]) rdr["Imagen2"],
                            Imagen3 = (byte[]) rdr["Imagen3"],
                            Valoracion = Convert.ToInt32(rdr["Valoracion"]),
							NombreHTL = rdr["NombreHTL"].ToString(),
							// NroServicio = rdr["nroServicio"].ToString(),
							DireccionHTL = rdr["DireccionHTL"].ToString(),
                            Telefono = rdr["Telefono"].ToString(),
                            DescripcionHTL = rdr["DesHTL"].ToString(),
                            DescripcionHTLI = rdr["DesHTLI"].ToString()
                        };
                        //pedido.CodVendedor = Convert.ToChar(GetValue(reader, "CodVendedor"));

                        lstGaleria.Add(item: galeria);
                    }

                    con.Close();
                }

                return lstGaleria;

            }
            catch (Exception ex)
            {
                throw;
            }

        }


    }
}