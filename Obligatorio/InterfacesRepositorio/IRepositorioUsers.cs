using LogicaDeNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.InterfacesRepositorio
{
    public interface IRepositorioUsers : IRepositorio<User>
    {
        public User Login(string username, string password);
        public User EncontrarUsuario(string username);
        public void ModificarContrasenia(User obj, string contrasenia);
        public void ModificarNombreDeUsuario(User obj, string nuevoUsername);

        public string EncontrarNombreUsuario(int id);

        public void AsignarFotoPerfil(int userId, string nomArchivoFotoPerfil);
        public void SeguirUsuario(int idUsuarioSeguidor, int idUsuarioSeguido);
        public void DejarDeSeguirUsuario(int idUsuarioSeguidor, int idUsuarioSeguido);
        public IEnumerable<Publicacion> MostrarPublicacionesDeUsuario(int id);
        public IEnumerable<Grupo> MostrarGrupos(int id);
        /*
        public IEnumerable<Mensaje> MostrarMensajesRecibidos(int id);
        public IEnumerable<Mensaje> MostrarMensajesEnviados(int id);
        */
        public IEnumerable<Notificacion> Mostrarnotificaciones(int id);
        public IEnumerable<User> MostrarSeguidores(int id);
        public IEnumerable<User> MostrarSeguidos(int id);
        public IEnumerable<User> ObtenerSeguidosDeSeguidos(int userId);
        public IEnumerable<int> ObtenerIdsDeUsers(List<User> users);



    }
}
