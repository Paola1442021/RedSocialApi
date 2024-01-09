using LogicaDeNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.InterfacesRepositorio
{
    public interface IRepositorioMensajes: IRepositorio<Mensaje>
    {
        public IEnumerable<int> ObtenerIdsDeMensajes(List<Mensaje> mensajes);
        public IEnumerable<Mensaje> ObtenerCharlaDeUsuarios(int idUsuario1, int idUsuario2);
        public void RemoveMensaje(int messageId, int userId);


    }
}
