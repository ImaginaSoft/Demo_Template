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
	public class PasajeroAccess
	{

		public string RegistrarPasajero(string pDesLog, string pApe, string pNumP, string pFecNac, string pNacionalidad, string pNroPedido)
		{
			try
			{

				using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
				{

					SqlCommand cmd = new SqlCommand("VTA_Pasajero_I", con);

					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.Add("@NroPedido", SqlDbType.Text).Value = pDesLog;
					cmd.Parameters.Add("@NroPasajero", SqlDbType.Text).Value = pDesLog;
					cmd.Parameters.Add("@NomPasajero", SqlDbType.Text).Value = pDesLog;
					cmd.Parameters.Add("@ApPasajero", SqlDbType.Text).Value = pApe;
					cmd.Parameters.Add("@Pasaporte", SqlDbType.Text).Value = pNumP;
					cmd.Parameters.Add("@Nacionalidad", SqlDbType.Text).Value = pNacionalidad;
					cmd.Parameters.Add("@FchNacimiento", SqlDbType.Text).Value = pFecNac;
					cmd.Parameters.Add("@TipoPasajero", SqlDbType.Text).Value = "test";
					cmd.Parameters.Add("@Observacion", SqlDbType.Text).Value = "test";
					cmd.Parameters.Add("@Acomodacion", SqlDbType.Text).Value = "test";
					cmd.Parameters.Add("@CodUsuario", SqlDbType.Text).Value = "test";
					cmd.Parameters.Add("@CodGenero", SqlDbType.Text).Value = "test";
					cmd.Parameters.Add("@DesIdioma", SqlDbType.Text).Value = "test";
					cmd.Parameters.Add("@DesHabitacion", SqlDbType.Text).Value = "test";

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



	}
}