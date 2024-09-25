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
    public class ArticuloManager : IArticuloManager
    {
        private IArticuloRepositorio _repositorio;

        public ArticuloManager()
        {
            _repositorio = new ArticuloRepositorio();
        }

        public List<Articulo> GetArticulos()
        {
            return _repositorio.GetAll();
        }

        public Articulo GetArticuloById(int id)
        {
            return _repositorio.GetById(id);
        }

        public bool DeleteArticulo(int id)
        {
            return _repositorio.Delete(id);
        }

        public bool SaveArticulo(Articulo articulo)
        {
            return _repositorio.Save(articulo);
        }

        public bool UpdateArticulo(Articulo oArticulo)
        {
            return _repositorio.Update(oArticulo);
        }
    }
}
