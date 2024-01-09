using ExcepcionesPropias;
using LogicaDeNegocio.Dominio;
using LogicaDeNegocio.InterfacesRepositorio;
using LogicaDeNegocio.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeDatos
{
    public class RepositorioUsers : IRepositorioUsers
    {
        public EmpresaContext Contexto { get; set; }

        public RepositorioUsers(EmpresaContext ctx)
        {
            Contexto = ctx;
        }

        public void Add(User obj)
        {
            try
            {
                if (obj != null)
                {
                    // Verificar si ya existe un usuario con el mismo alias
                    var usuarioExistente = Contexto.users.FirstOrDefault(u => u.Id != obj.Id && (u.Email.Value == obj.Email.Value || u.Username.Value == obj.Username.Value));

                    if (usuarioExistente == null)
                    {
                        obj.Validar(); // Supongo que Validar() realiza las validaciones necesarias
                        Contexto.users.Add(obj);
                        Contexto.SaveChanges();
                    }
                    else
                    {
                        // Lanzar una excepción o realizar alguna acción para manejar el caso de usuario duplicado
                        throw new MensajeExeption("Ya existe ese usuario.");
                    }
                }
            }
            catch (UserException ex)
            {
                throw new Exception("No se pudo crear el usuario", ex);
            }
        }

        // Supongamos que tienes un método Remove en tu repositorio que elimina el usuario de la base de datos
        // Además, asumimos que tu lista de seguidores y seguidos está correctamente configurada para admitir esta eliminación.
        public void Remove(User usu)
        {
            var usuario = Contexto.users
                .Include(u => u.Publicaciones)
                .Include(u => u.Grupos)
                .Include(u => u.Email)
                .Include(u => u.MensajesRecibidos)
                .Include(u => u.MensajesEnviados)
                .Include(u => u.Username)
                .Include(u => u.Notificaciones)
                .Include(u => u.Seguidores)
                .Include(u => u.Seguidos)
                .Include(u => u.MeGustas)

        .FirstOrDefault(u => u.Id == usu.Id);

            if (usuario != null)
            {
                // Eliminar al usuario de las listas de seguidores
                usuario.Seguidores?.ForEach(s => s.Seguidos?.Remove(usuario));

                // Eliminar al usuario de las listas de seguidos
                usuario.Seguidos?.ForEach(s => s.Seguidos?.Remove(usuario));

                // Eliminar al usuario de los grupos
                usuario.Grupos?.ForEach(g => g.Miembros?.Remove(usuario));

                // Eliminar mensajes recibidos
                Contexto.mensajes.RemoveRange(usuario.MensajesRecibidos);

                // Eliminar mensajes enviados
                Contexto.mensajes.RemoveRange(usuario.MensajesEnviados);

                // Eliminar mensajes recibidos
                Contexto.meGustas.RemoveRange(usuario.MeGustas);

                // Eliminar mensajes enviados
                Contexto.publicaciones.RemoveRange(usuario.Publicaciones);// Eliminar mensajes recibidos

                // Antes de eliminar el usuario, elimina las notificaciones asociadas
                Contexto.notificaciones.RemoveRange(usuario.Notificaciones);

                // Eliminar al usuario de las listas de mensajes recibidos y enviados
                usuario.MensajesRecibidos?.Clear();
                usuario.MensajesEnviados?.Clear();
                usuario.Publicaciones?.Clear();
                usuario.MeGustas?.Clear();


                // Eliminar al usuario de la lista de notificaciones
                foreach (var notificacion in usuario.Notificaciones)
                {
                    notificacion.Usuario = null;
                }

                // Finalmente, eliminar el usuario de la base de datos
                Contexto.users.Remove(usuario);
                Contexto.SaveChanges();
            }
        }



        public void ModificarNombreDeUsuario(User obj, string nuevoUsername)
        {

            obj.Username = new NombreUsuario(nuevoUsername); // Si el nuevo username es un string, puedes crear un nuevo NombreUsuario
            Update(obj);
        }

        public void Update(User obj)
        {
            Contexto.users.Update(obj);
            Contexto.SaveChanges();
        }

        public void ModificarContrasenia(User obj, string contrasenia)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Cifrar la contraseña proporcionada
                byte[] passwordBytes = Encoding.UTF8.GetBytes(contrasenia);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                string encryptedPassword = BitConverter.ToString(hashBytes).Replace("-", "");

                // Asignar la contraseña cifrada al objeto User
                obj.Password_hash = encryptedPassword;
            }

            // Llamar al método Update fuera del bloque using
            Update(obj);
        }


        public IEnumerable<User> FindAll()
        {
            return Contexto.users
                .Include(u => u.Publicaciones)
                .Include(u => u.Grupos)
                .Include(u => u.Email)
                .Include(u => u.MensajesRecibidos)
                .Include(u => u.MensajesEnviados)
                .Include(u => u.Username)
                .Include(u => u.Notificaciones)
                .Include(u => u.Seguidores)
                .Include(u => u.Seguidos)
                .Include(u => u.MeGustas)
                .ToList();
        }
        public string EncontrarNombreUsuario(int id)
        {
            var usuario = Contexto.users
                .FirstOrDefault(u => u.Id == id);

            return usuario?.Username?.Value;
        }


        public User FindById(int id)
        {
            var user = Contexto.users
               .Include(u => u.Publicaciones)
                .Include(u => u.Grupos)
                .Include(u => u.Email)
                .Include(u => u.MensajesRecibidos)
                .Include(u => u.MensajesEnviados)
                .Include(u => u.Username)
                .Include(u => u.Notificaciones)
                .Include(u => u.Seguidores)
                .Include(u => u.Seguidos)
               .FirstOrDefault(u => u.Id == id);

            return user;
        }
        public User Login(string username, string password)
        {
            if (username == null || password == null)
            {
                return null;
            }
            else
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    // Cifrar la contraseña proporcionada
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                    byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                    string encryptedPassword = BitConverter.ToString(hashBytes).Replace("-", "");

                    // Buscar el usuario por nombre de usuario y contraseña cifrada
                    var usuarioEncontrado = Contexto.users
       .Include(u => u.Publicaciones)
                .Include(u => u.Grupos)
                .Include(u => u.Email)
                .Include(u => u.MensajesRecibidos)
                .Include(u => u.MensajesEnviados)
                .Include(u => u.Username)
                .Include(u => u.Notificaciones)
                .Include(u => u.Seguidores)
                .Include(u => u.Seguidos)
                .Include(u => u.MeGustas)
      .FirstOrDefault(u => u.Username.Value == username && u.Password_hash == encryptedPassword);

                    return usuarioEncontrado;
                }
            }
        }

        //funcion que voy a devolver cuando lo busque por nombre
        public User EncontrarUsuario(string username)
        {
            if (username == null)
            {
                return null;
            }
            else
            {
               
                    // Buscar el usuario por nombre de usuario 
                    var usuarioEncontrado = Contexto.users
      .Include(u => u.Publicaciones)
                .Include(u => u.Grupos)
                .Include(u => u.Email)
                .Include(u => u.MensajesRecibidos)
                .Include(u => u.MensajesEnviados)
                .Include(u => u.Username)
                .Include(u => u.Notificaciones)
                .Include(u => u.Seguidores)
                .Include(u => u.Seguidos)
                .Include(u => u.MeGustas)
      .FirstOrDefault(u => u.Username.Value == username );

                    return usuarioEncontrado;
                }
            }




        public void AsignarFotoPerfil(int userId, string nomArchivoFotoPerfil)
        {
            // Obtener el usuario de la base de datos
            var usuario = Contexto.users.FirstOrDefault(u => u.Id == userId);

            if (usuario != null)
            {
                // Asignar el nombre del archivo de foto de perfil
                usuario.NomArchivoFotoPerfil = nomArchivoFotoPerfil;

                // Guardar los cambios en la base de datos
                Contexto.SaveChanges();
            }
            else
            {
                // El usuario no fue encontrado
                // Puedes lanzar una excepción o manejarlo de acuerdo a tus requerimientos
                throw new Exception("Usuario no encontrado.");
            }
        }
        public void SeguirUsuario(int idUsuarioSeguidor, int idUsuarioSeguido)
        {
            // Obtener los usuarios de la base de datos
            var usuarioSeguidor = Contexto.users.Include(u => u.Seguidos).FirstOrDefault(u => u.Id == idUsuarioSeguidor);
            var usuarioSeguido = Contexto.users.Include(u => u.Seguidores).FirstOrDefault(u => u.Id == idUsuarioSeguido);

            if (usuarioSeguidor != null && usuarioSeguido != null)
            {
                // Verificar si ya están siguiéndose mutuamente (evitar duplicados)
                if (!usuarioSeguidor.Seguidos.Contains(usuarioSeguido) && !usuarioSeguido.Seguidores.Contains(usuarioSeguidor))
                {
                    // Establecer la relación de seguimiento
                    usuarioSeguidor.Seguidos.Add(usuarioSeguido);
                    usuarioSeguido.Seguidores.Add(usuarioSeguidor);

                    // Guardar los cambios en la base de datos
                    Contexto.SaveChanges();
                }
                else
                {
                    // Ya están siguiéndose mutuamente o ya existe la relación
                    // Puedes lanzar una excepción o manejarlo de acuerdo a tus requerimientos
                    throw new Exception("Ya están siguiéndose mutuamente.");
                }
            }
            else
            {
                // Al menos uno de los usuarios no fue encontrado
                // Puedes lanzar una excepción o manejarlo de acuerdo a tus requerimientos
                throw new Exception("Usuarios no encontrados.");
            }
        }


        public void DejarDeSeguirUsuario(int idUsuarioSeguidor, int idUsuarioSeguido)
        {
            // Obtener los usuarios de la base de datos
            var usuarioSeguidor = Contexto.users.Include(u => u.Seguidos).FirstOrDefault(u => u.Id == idUsuarioSeguidor);
            var usuarioSeguido = Contexto.users.Include(u => u.Seguidores).FirstOrDefault(u => u.Id == idUsuarioSeguido);

            if (usuarioSeguidor != null && usuarioSeguido != null)
            {
                // Verificar si existe la relación de seguimiento
                if (usuarioSeguidor.Seguidos.Contains(usuarioSeguido) && usuarioSeguido.Seguidores.Contains(usuarioSeguidor))
                {
                    // Eliminar la relación de seguimiento
                    usuarioSeguidor.Seguidos.Remove(usuarioSeguido);
                    usuarioSeguido.Seguidores.Remove(usuarioSeguidor);

                    // Guardar los cambios en la base de datos
                    Contexto.SaveChanges();
                }
                else
                {
                    // La relación de seguimiento no existe
                    // Puedes lanzar una excepción o manejarlo de acuerdo a tus requerimientos
                    throw new Exception("No existe la relación de seguimiento.");
                }
            }
            else
            {
                // Al menos uno de los usuarios no fue encontrado
                // Puedes lanzar una excepción o manejarlo de acuerdo a tus requerimientos
                throw new Exception("Usuarios no encontrados.");
            }
        }

        public IEnumerable<Publicacion> MostrarPublicacionesDeUsuario(int id)
        {
            var usuario = Contexto.users
                .Include(u => u.Publicaciones)
                .FirstOrDefault(u => u.Id == id);

            return usuario?.Publicaciones;
        }




        public IEnumerable<Grupo> MostrarGrupos(int id)
        {
            var usuario = Contexto.users
                .Include(u => u.Grupos)
                .FirstOrDefault(u => u.Id == id);

            return usuario?.Grupos;
        }


        /* public IEnumerable<Mensaje> MostrarMensajesRecibidos(int id)
         {
             var usuario = Contexto.users
                 .Include(u => u.MensajesRecibidos)
                 .FirstOrDefault(u => u.Id == id);

             return usuario?.MensajesRecibidos;
         }
         public IEnumerable<Mensaje> MostrarMensajesEnviados(int id)
         {
             var usuario = Contexto.users
                 .Include(u => u.MensajesEnviados)
                 .FirstOrDefault(u => u.Id == id);

             return usuario?.MensajesEnviados;
         }*/

      

        public IEnumerable<Notificacion> Mostrarnotificaciones(int id)
        {
            var usuario = Contexto.users
                .Include(u => u.Notificaciones)
                .FirstOrDefault(u => u.Id == id);

            return usuario?.Notificaciones;
        }
        public IEnumerable<User> MostrarSeguidores(int id)
        {
            var usuario = Contexto.users
                .Include(u => u.Seguidores)
                .FirstOrDefault(u => u.Id == id);

            return usuario?.Seguidores;
        }
        public IEnumerable<User> MostrarSeguidos(int id)
        {
            var usuario = Contexto.users
                .Include(u => u.Seguidos)
                .FirstOrDefault(u => u.Id == id);

            return usuario?.Seguidos;
        }

       //aplicacion para que aparezcan perfiles para seguir
        public IEnumerable<User> ObtenerSeguidosDeSeguidos(int userId)
        {
            // Obtener el usuario y sus seguidos
            var usuario = Contexto.users
                .Include(u => u.Seguidos)
                .FirstOrDefault(u => u.Id == userId);

            if (usuario != null)
            {
                // Obtener los seguidos de los seguidos
                var seguidosDeSeguidos = usuario.Seguidos
                    .SelectMany(u => u.Seguidos)
                    .Distinct()
                    .ToList();

                return seguidosDeSeguidos;
            }
            else
            {
                // El usuario no fue encontrado
                // Puedes lanzar una excepción o manejarlo de acuerdo a tus requerimientos
                throw new Exception("Usuario no encontrado.");
            }
        }
        public IEnumerable<int> ObtenerIdsDeUsers(List<User> users)
        {
            List<int> idsEncontrados = new List<int>();

            foreach (User us in users)
            {
                int idEncontrado = Contexto.users
                    .Where(u => u.Id == us.Id)
                    .Select(u => u.Id)
                    .FirstOrDefault();

                if (idEncontrado != 0)
                {
                    idsEncontrados.Add(idEncontrado);
                }
            }

            return idsEncontrados;
        }

    }
}
