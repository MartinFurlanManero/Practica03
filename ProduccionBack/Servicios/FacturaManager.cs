using ProduccionBack.Datos.Implementaciones;
using ProduccionBack.Datos.Interfaces;
using ProduccionBack.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProduccionBack.Servcios
{
    public class FacturaManager : IFacturaManager
    {
        private IFacturaRepositorio _repositorio;

        public FacturaManager()
        {
            _repositorio = new FacturaRepositorio();
        }

        public List<Factura> GetFacturas()
        {
            return _repositorio.GetAll();
        }

        public Factura? GetFacturaById(int id)
        {
            return _repositorio.GetById(id);
        }

        public bool SaveFactura(Factura factura)
        {
            return _repositorio.Save(factura);
        }

        public bool DeleteFactura(int nroFactura)
        {
            return _repositorio.Delete(nroFactura);
        }

        public bool Update(Factura factura)
        {
            return _repositorio.Update(factura);
        }
    }
}
