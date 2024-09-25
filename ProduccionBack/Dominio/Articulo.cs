using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProduccionBack.Dominio
{
    public class Articulo
    {
        public int Codigo {  get; set; }    
        public string Nombre { get; set; }
        public double Precio { get; set; }

        public override string ToString()
        {
            return $"Id: {Codigo}, Nombre: {Nombre}, Precio: {Precio}";
        }
    }
}
