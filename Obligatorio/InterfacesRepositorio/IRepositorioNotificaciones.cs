using LogicaDeNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.InterfacesRepositorio
{
    public interface IRepositorioNotificaciones : IRepositorio<Notificacion>
    {
        IEnumerable<Notificacion> EncontrarNotificacionesNoLeidas();
        public IEnumerable<int> ObtenerIdsDeNotificaciones(List<Notificacion> notis);
        public void MarcarNotificacionComoLeida(int notificacionId, int userId);


    }
}
