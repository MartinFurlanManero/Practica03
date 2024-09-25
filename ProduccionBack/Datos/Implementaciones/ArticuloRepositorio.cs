using ProduccionBack.Datos.Interfaces;
using ProduccionBack.Datos.Utilidades;
using ProduccionBack.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProduccionBack.Datos.Implementaciones
{
    public class ArticuloRepositorio : IArticuloRepositorio
    {
        public bool Delete(int id)
        {
            var parametros = new List<ParametroSQL>
            {
                new ParametroSQL("@cod_articulo", id)
            };

            int filasAfectadas = DataHelper.GetInstance().ExecuteSPDML("SP_REGISTRAR_BAJA_ARTICULO", parametros);
            return filasAfectadas > 0;
        }

        public List<Articulo> GetAll()
        {
            List<Articulo> lst = new List<Articulo>();
            var helper = DataHelper.GetInstance();
            var t = helper.ExecuteSPQuery("SP_RECUPERAR_ARTICULO", null);
            foreach (DataRow row in t.Rows)
            {
                int id = Convert.ToInt32(row["cod_articulo"]);
                string nombre = Convert.ToString(row["articulo"]);
                double precio = Convert.ToDouble(row["pre_unitario"]);

                Articulo articulo = new Articulo()
                {
                    Codigo = id,
                    Nombre = nombre,
                    Precio = precio
                };
                lst.Add(articulo);
            }

            return lst;
        }

        public Articulo GetById(int id)
        {
            var parameters = new List<ParametroSQL>();
            parameters.Add(new ParametroSQL("@cod_articulo", id));
            DataTable t = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_ARTICULO_POR_ID", null);
            if (t != null && t.Rows.Count == 1)
            {
                DataRow row = t.Rows[0];
                int cod = Convert.ToInt32(row["cod_articulo"]);
                string nombre = Convert.ToString(row["articulo"]);
                float precio = (float)row["pre_unitario"];

                Articulo articulo = new Articulo()
                {
                    Codigo = cod,
                    Nombre = nombre,
                    Precio = precio
                };
                return articulo;
            }
            return null;
        }

        public bool Update(Articulo oArticulo)
        {
            bool resutlado = true;
            SqlConnection? cnn = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                var cmd = new SqlCommand("SP_ACTUALIZAR_ARTICULO", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cod_articulo", oArticulo.Codigo);
                cmd.Parameters.AddWithValue("@articulo", oArticulo.Nombre);
                cmd.Parameters.AddWithValue("@pre_unitario", oArticulo.Precio);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                resutlado = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return resutlado;
        }

        public bool Save(Articulo oArticulo)
        {
            bool resutlado = true;
            SqlConnection? cnn = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                var cmd = new SqlCommand("SP_INSERTAR_ARTICULO", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cod_articulo", oArticulo.Codigo);
                cmd.Parameters.AddWithValue("@articulo", oArticulo.Nombre);
                cmd.Parameters.AddWithValue("@pre_unitario", oArticulo.Precio);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                resutlado = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return resutlado;
        }
    }
}
