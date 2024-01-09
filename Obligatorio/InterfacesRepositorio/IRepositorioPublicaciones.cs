using LogicaDeNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.InterfacesRepositorio
{
    public interface IRepositorioPublicaciones : IRepositorio<Publicacion>
    {
        Publicacion FindById(int? publicacionPadre);
        public IEnumerable<int> ObtenerIdsDePublicaciones(List<Publicacion> publis);
        public void RemovePublicacion(int publicacionId, int userId);


    }
}
