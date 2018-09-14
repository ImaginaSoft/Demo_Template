using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Demo_1.Models.PeruTourism;
using Demo_1.Repository.Data;
using System.Data.SqlClient;
using System.Data;

namespace Demo_1.Repository.PeruTourism
{
    public class PedidoAccess
    {


        public IEnumerable<Pedido> ObtenerListadoPedido()
        {

            var lstPedido = new List<Pedido>();

            using (SqlConnection objConnection = new SqlConnection(Data.Data.StrCnx_WebsSql)) {

                using (SqlCommand objCommand = new SqlCommand())
                {
                    
                    objCommand.CommandText = "SELECT * FROM CPEDIDO WHERE CodVendedor='MAYRA' AND FchPedido > = '2018-06-15';";
                    objCommand.CommandType = CommandType.Text;
                    objCommand.Connection = objConnection;
                    objConnection.Open();

                    using (var reader = objCommand.ExecuteReader()) {

                        while(reader.Read()){

                            var pedido = new Pedido
                            {
                                NroPedido = int.Parse(GetValue(reader, "NroPedido")),
                                DesPedido = GetValue(reader, "DesPedido"),
                                FchPedido = DateTime.Parse(GetValue(reader, "FchPedido"))
                            };
                            //pedido.CodVendedor = Convert.ToChar(GetValue(reader, "CodVendedor"));

                            lstPedido.Add(pedido);

                        }

                    }

                }

                return lstPedido;

            }

        }

        private string GetValue(IDataReader registo,
                           string columna)
        {
            var lvalue = registo[columna];

            return (((lvalue is DBNull) || (lvalue == null)) ? null : registo[columna].ToString());
        }

            











    }
}