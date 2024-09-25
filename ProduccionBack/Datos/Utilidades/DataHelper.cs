using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProduccionBack.Datos.Utilidades
{
    public class DataHelper
    {
        private static DataHelper instance;
        private SqlConnection cnn;

        public DataHelper()
        {
            cnn = new SqlConnection(Properties.Resources.CadenaConexion);
        }
        public static DataHelper GetInstance()
        {
            if (instance == null)
            {
                instance = new DataHelper();
            }
            return instance;
        }
        public DataTable ExecuteSPQuery(string sp, List<ParametroSQL>? parametros)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;

            try
            {
                cnn.Open();
                cmd = new SqlCommand(sp, cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parametros != null)
                {
                    foreach (var param in parametros)
                    {
                        cmd.Parameters.AddWithValue(param.Nombre, param.Valor);
                    }
                }

                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException)
            {
                dt = null;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            return dt;
        }
        public int ExecuteSPDML(string sp, List<ParametroSQL>? parametros)
        {
            int rows;
            try
            {
                cnn.Open();
                var cmd = new SqlCommand(sp, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var param in parametros)
                    {
                        cmd.Parameters.AddWithValue(param.Nombre, param.Valor);
                    }
                }

                rows = cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (SqlException)
            {
                rows = 0;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            return rows;
        }
        public int ExecuteSPDMLTransact(string sp, List<ParametroSQL>? parametros, SqlConnection cnn, SqlTransaction transaction, object parameterOut = null)
        {
            return 0;
        }
        public SqlConnection GetConnection()
        {
            return cnn;
        }
    }
}
