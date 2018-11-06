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

                        cmd = new SqlCommand("VTA_ListaIMG_S", con);

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

        public IEnumerable<Galeria> CargarGaleria(int sNroServicio)
        {
            try
            {
                var lstGaleria = new List<Galeria>();

                string sIdGaleria = string.Empty;

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd = new SqlCommand("VTA_ListaIMG_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@NroServicio", SqlDbType.Int).Value = sNroServicio;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        


                        Galeria galeria = new Galeria
                        {


                            //Imagen1 = Convert.FromBase64String("FlagImg01"),                          
                            flagImg01 = rdr["FlagImg01"].ToString(),
                            NroServicio = rdr["nroServicio"].ToString(),
                            DireccionHTL = rdr["DireccionHTL"].ToString(),
                            Telefono = rdr["Telefono"].ToString()
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