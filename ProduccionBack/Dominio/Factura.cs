using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProduccionBack.Dominio
{
    public class Factura
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public FormaPago FormaPago { get; set; }
        public string Cliente { get; set; }

        // Usar una sola lista para los detalles
        public List<DetalleFactura> Detalles { get; set; }

        public Factura()
        {
            Detalles = new List<DetalleFactura>();
        }

        public void AddDetalle(DetalleFactura detalle)
        {
            if (detalle != null)
            {
                Detalles.Add(detalle);
            }
        }

        public void Remove(int index)
        {
            Detalles.RemoveAt(index);
        }

        public List<DetalleFactura> GetDetalle()
        {
            return Detalles;
        }

        public override string ToString()
        {
            return $"NroFactura: {NroFactura}, Fecha: {Fecha}, Froma Pago: {FormaPago}, Cleinte: {Cliente}";
        }
    }
}
