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
    public class FacturaRepositorio : IFacturaRepositorio
    {
        public List<Factura> GetAll()
        {
            List<Factura> lst = new List<Factura>();
            Factura? oFactura = null;
            DataTable table = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_FACTURAS", null);
            foreach (DataRow row in table.Rows)
            {
                if (oFactura == null || oFactura.NroFactura != Convert.ToInt32(row["cod_factura"].ToString()))
                {
                    oFactura = new Factura();
                    oFactura.NroFactura = Convert.ToInt32(row["cod_factura"]);
                    oFactura.Fecha = Convert.ToDateTime(row["fecha"]);
                    oFactura.FormaPago = new FormaPago
                    {
                        Id = Convert.ToInt32(row["cod_forma_pago"].ToString()),
                        Nombre = Convert.ToString(row["forma_pago"].ToString())
                    };
                    oFactura.Cliente = Convert.ToString(row["cliente"]);
                    lst.Add(oFactura);
                }
                else
                {
                    oFactura.AddDetalle(ReadDetalle(row));
                }
            }
            return lst;
        }

        private DetalleFactura ReadDetalle(DataRow row)
        {
            DetalleFactura detalle = new DetalleFactura();
            detalle.Articulo = new Articulo
            {
                Codigo = Convert.ToInt32(row["cod_articulo"]),
                Nombre = Convert.ToString(row["articulo"]),
                Precio = Convert.ToSingle(row["pre_unitario"])
            };
            detalle.Cantidad = Convert.ToInt32(row["cantidad"]);
            return detalle;
        }

        public Factura? GetById(int id)
        {
            Factura? oFactura = null;
            var helper = DataHelper.GetInstance();
            var parametro = new ParametroSQL("cod_factura", id);
            var parametros = new List<ParametroSQL>();
            parametros.Add(parametro);

            var t = helper.ExecuteSPQuery("SP_RECUPERAR_PRESUPUESTO_POR_ID", parametros);
            foreach (DataRow row in t.Rows)
            {
                if (oFactura == null)
                {
                    oFactura = new Factura();
                    oFactura.NroFactura = Convert.ToInt32(row["cod_factura"]);
                    oFactura.Fecha = Convert.ToDateTime(row["fecha"]);
                    oFactura.FormaPago = new FormaPago
                    {
                        Id = Convert.ToInt32(row["cod_forma_pago"]),
                        Nombre = Convert.ToString(row["forma_pago"])
                    };
                    oFactura.Cliente = Convert.ToString(row["cliente"]);
                    oFactura.AddDetalle(ReadDetalle(row));
                }
                else
                {
                    oFactura.AddDetalle(ReadDetalle(row));
                }
            }
            return oFactura;
        }

        public bool Save(Factura oFactura)
        {
            bool resultado = true;
            SqlTransaction? t = null;
            SqlConnection? cnn = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();

                // Verificar si el cod_forma_pago existe
                var cmdCheck = new SqlCommand("SELECT COUNT(*) FROM formas_pagos WHERE cod_forma_pago = @cod_forma_pago", cnn, t);
                cmdCheck.Parameters.AddWithValue("@cod_forma_pago", oFactura.FormaPago.Id);
                int count = (int)cmdCheck.ExecuteScalar();

                if (count == 0)
                {
                    throw new Exception("El código de forma de pago no existe.");
                }

                var cmd = new SqlCommand("SP_INSERTAR_MAESTRO", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cliente", oFactura.Cliente);
                cmd.Parameters.AddWithValue("@cod_forma_pago", oFactura.FormaPago.Id);
                cmd.Parameters.AddWithValue("@fecha", oFactura.Fecha);

                SqlParameter param = new SqlParameter("@cod_factura", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();

                int nroFactura = (int)param.Value;

                //int nroDetalle = 1;
                foreach (var detalle in oFactura.GetDetalle())
                {
                    var cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE", cnn, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@cod_factura", nroFactura);
                    //cmdDetalle.Parameters.AddWithValue("@cod_detalle_factura", nroDetalle);
                    cmdDetalle.Parameters.AddWithValue("@cod_articulo", detalle.Articulo.Codigo);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    cmdDetalle.ExecuteNonQuery();
                    //nroDetalle++;
                }

                t.Commit();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al guardar la factura: " + ex.Message);
                if (t != null)
                {
                    t.Rollback();
                }
                resultado = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return resultado;
        }

        public bool Update(Factura oFactura)
        {
            bool resultado = true;
            SqlTransaction? t = null;
            SqlConnection? cnn = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();

                var cmd = new SqlCommand("SP_ACTUALIZAR_FACTURA", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cod_factura", oFactura.NroFactura);
                cmd.Parameters.AddWithValue("@cliente", oFactura.Cliente);
                cmd.Parameters.AddWithValue("@cod_forma_pago", oFactura.FormaPago.Id);
                cmd.Parameters.AddWithValue("@fecha", oFactura.Fecha);
                cmd.ExecuteNonQuery();

                // Eliminar los detalles anteriores
                var cmdDeleteDetalles = new SqlCommand("SP_ELIMINAR_DETALLES_POR_FACTURA", cnn, t);
                cmdDeleteDetalles.CommandType = CommandType.StoredProcedure;
                cmdDeleteDetalles.Parameters.AddWithValue("@cod_factura", oFactura.NroFactura);
                cmdDeleteDetalles.ExecuteNonQuery();

                // Insertar los nuevos detalles
                foreach (var detalle in oFactura.GetDetalle())
                {
                    var cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE", cnn, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@cod_factura", oFactura.NroFactura);
                    cmdDetalle.Parameters.AddWithValue("@cod_articulo", detalle.Articulo.Codigo);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    cmdDetalle.ExecuteNonQuery();
                }

                t.Commit();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al actualizar la factura: " + ex.Message);
                if (t != null)
                {
                    t.Rollback();
                }
                resultado = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return resultado;
        }

        public bool Delete(int codFactura)
        {
            bool resultado = true;
            SqlTransaction? t = null;
            SqlConnection? cnn = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();

                // Eliminar los detalles de la factura
                var cmdDeleteDetalles = new SqlCommand("SP_ELIMINAR_DETALLES_POR_FACTURA", cnn, t);
                cmdDeleteDetalles.CommandType = CommandType.StoredProcedure;
                cmdDeleteDetalles.Parameters.AddWithValue("@cod_factura", codFactura);
                cmdDeleteDetalles.ExecuteNonQuery();

                // Eliminar la factura
                var cmdDeleteFactura = new SqlCommand("SP_ELIMINAR_FACTURA", cnn, t);
                cmdDeleteFactura.CommandType = CommandType.StoredProcedure;
                cmdDeleteFactura.Parameters.AddWithValue("@cod_factura", codFactura);
                cmdDeleteFactura.ExecuteNonQuery();

                t.Commit();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al eliminar la factura: " + ex.Message);
                if (t != null)
                {
                    t.Rollback();
                }
                resultado = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return resultado;
        }
    }
}
