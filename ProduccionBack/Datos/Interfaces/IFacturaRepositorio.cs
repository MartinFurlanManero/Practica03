using ProduccionBack.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProduccionBack.Datos.Interfaces
{
    public interface IFacturaRepositorio
    {
        bool Save(Factura factura);
        bool Update(Factura factura);
        bool Delete(int nroFactura);
        List<Factura> GetAll();
        Factura? GetById(int id);
    }
}
