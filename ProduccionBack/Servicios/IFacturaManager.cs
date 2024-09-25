using ProduccionBack.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProduccionBack.Servcios
{
    public interface IFacturaManager
    {
        List<Factura> GetFacturas();

        Factura? GetFacturaById(int id);

        bool SaveFactura(Factura factura);

        bool DeleteFactura(int nroFactura);

        bool Update(Factura factura);
    }
}
