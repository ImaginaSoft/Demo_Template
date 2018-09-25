using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeruTourism.Models.PeruTourism;
using System.Data.SqlClient;
using System.Data;


namespace PeruTourism.Repository.PeruTourism
{
    public class LoginAccess
    {

        public IEnumerable<Cliente> LeerCliente(string idCliente, string codCliente) {

            try
            {

                List<Cliente> lstCliente = new List<Cliente>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("VTA_ClienteEmail_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CodCliente", SqlDbType.Int).Value = codCliente;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                     
                        if (rdr["IDCliente"].ToString().Trim() == idCliente.Trim()) {

                            Cliente fcliente = new Cliente
                            {

                                CodCliente = codCliente,
                                NomCliente = rdr["NomCliente"].ToString(),
                                EmailCliente = rdr["Email"].ToString()

                            };

                            lstCliente.Add(item: fcliente);

                        }

                    }

                    con.Close();
                }

                return lstCliente;

            }
            catch (Exception ex) {

                throw;

            } 

        }

        public IEnumerable<UltimaPublicacion> LeeUltimaPublicacion(int pCodCliente)
        {

            try
            {

                List<UltimaPublicacion> lstPublicacion = new List<UltimaPublicacion>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                 
                    SqlCommand cmd = new SqlCommand("P4I_PublicaUltimo_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CodZonaVta", SqlDbType.Char,3).Value = "PER";
                    cmd.Parameters.Add("@CodCliente", SqlDbType.Int).Value = pCodCliente;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        UltimaPublicacion fpublicacion = new UltimaPublicacion
                        {

                                NroPedido = Convert.ToInt32(rdr["NroPedido"]),
                                NroPropuesta = Convert.ToInt32(rdr["NroPropuesta"]),
                                NroVersion = Convert.ToInt32(rdr["NroVersion"]),
                                FlagIdioma = Convert.ToChar(rdr["FlagIdioma"].ToString()),
                                CantPropuestas = Convert.ToInt32(rdr["CantPropuestas"])

                        };

                        lstPublicacion.Add(item: fpublicacion);

                    }

                    con.Close();
                }

                return lstPublicacion;

            }
            catch (Exception ex)
            {

                throw;

            }


        }
    }
}