using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using PeruTourism.Models.PeruTourism;
using PeruTourism.Utility;
using PeruTourism.Models.Paises;
using PeruTourism.Models.TipoPasajero;

namespace PeruTourism.Repository.PeruTourism
{
	public class PasajeroAccess
	{
		public void RegistrarPasajero(Int16 pNumPasajero, string pDesLog, string pApe, string pPasajero, string pFecNac, string pNacionalidad, string pNroPedido, string pTipo, string pGenero)
		{
            string codigo = "";

            try
            {
				using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
				{
					SqlCommand cmd = new SqlCommand("VTA_Pasajero_Ins", con);

					cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NroPedido", pNroPedido);
					cmd.Parameters.AddWithValue("@NroPasajero", pNumPasajero);
					cmd.Parameters.AddWithValue("@NomPasajero", pDesLog);
					cmd.Parameters.AddWithValue("@ApPasajero", pApe);
					cmd.Parameters.AddWithValue("@Pasaporte", pPasajero);
					cmd.Parameters.AddWithValue("@Nacionalidad", pNacionalidad);
					cmd.Parameters.AddWithValue("@FchNacimiento", pFecNac);
					cmd.Parameters.AddWithValue("@CodGenero", pGenero);
                    cmd.Parameters.AddWithValue("@TipoPasajero", pTipo);

                    con.Open();
					codigo = (String)cmd.ExecuteScalar();
                    con.Close();
                }
			}
			catch (Exception ex)
			{
				throw;
			}
        }
        public void EliminarPasajero(Int16 pNumPasajero, string pNroPedido)
        {
            string codigo = "";

            try
            {
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    SqlCommand cmd = new SqlCommand("VTA_Pasajero_Del", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NroPedido", pNroPedido);
                    cmd.Parameters.AddWithValue("@NroPasajero", pNumPasajero);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Pasajero> ListarPasajeros(string pNroPedido)
        {
            var lista = new List<Pasajero>();
            var dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    SqlCommand cmd = new SqlCommand("VTA_Pasajero_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NroPedido", pNroPedido);

                    con.Open();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();

                    lista = dt.AsEnumerable().Select(x => new Pasajero
                    {
                        NomPasajero = x.Field<string>("NomPasajero"),
                        ApePasajero = x.Field<string>("ApePasajero"),
                        Pasaporte = x.Field<string>("Pasaporte"),
                        FchNacimiento = Convert.ToDateTime(x.Field<string>("FchNacimiento")),
                        CodNacionalidad = x.Field<string>("CodNacionalidad"),
                        Nacionalidad = x.Field<string>("Nacionalidad"),
                        Genero = x.Field<string>("NomGenero"),
                        NroPasajero = x.Field<Int16>("NroPasajero"),
                        TipoPasajero = x.Field<string>("CodTipoPasajero"),
                        CodGenero = x.Field<string>("CodGenero")
                    })
                    .ToList();
                }
            }
            catch (Exception ex)
            {
                lista = new List<Pasajero>();
            }

            return lista;
        }

        public List<Pais> ListarPaises()
        {
            var lista = new List<Pais>();
            var dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    SqlCommand cmd = new SqlCommand("TAB_Pais_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();

                    lista = dt.AsEnumerable().Select(x => new Pais
                    {
                        CodPais = x.Field<string>("CodPais"),
                        NomPais = x.Field<string>("NomPais")
                    })
                    .ToList();
                }
            }
            catch (Exception ex)
            {
                lista = new List<Pais>();
            }

            return lista;
        }

        public List<TipoPasajero> ListarTipoPasajero()
        {
            var lista = new List<TipoPasajero>();
            var dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    SqlCommand cmd = new SqlCommand("TAB_TipoPasajero", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();

                    lista = dt.AsEnumerable().Select(x => new TipoPasajero
                    {
                        CodTipoPasajero = x.Field<string>("CodTipoPasajero"),
                        NomTipoPasajero = x.Field<string>("TipoPasajero")
                    })
                    .ToList();
                }
            }
            catch (Exception ex)
            {
                lista = new List<TipoPasajero>();
            }

            return lista;
        }
    }
}