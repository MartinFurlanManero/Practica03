using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProduccionBack.Dominio
{
    public class DetalleFactura
    { 
        public Articulo Articulo { get; set; }
        public int Cantidad { get; set; }
    }
}
