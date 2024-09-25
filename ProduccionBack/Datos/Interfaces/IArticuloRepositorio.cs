using ProduccionBack.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProduccionBack.Datos.Interfaces
{
    public interface IArticuloRepositorio
    {
        List<Articulo> GetAll();
        Articulo GetById(int id);
        bool Save(Articulo oArticulo);
        bool Delete(int id);
        bool Update(Articulo oArticulo);
    }
}
