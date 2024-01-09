using LogicaDeNegocio.Dominio;
using LogicaDeNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeDatos
{
    public class RepositorioNotificaciones : IRepositorioNotificaciones
    {

        public EmpresaContext Contexto { get; set; }

        public RepositorioNotificaciones(EmpresaContext ctx)
        {
            Contexto = ctx;
        }

        public void Add(Notificacion obj)
        {
            if (obj != null)
            {
                obj.Validar();
                Contexto.notificaciones.Add(obj);
                Contexto.SaveChanges();
            }
        }

        public IEnumerable<Notificacion> FindAll()
        {
            return Contexto.notificaciones
                .Include(n => n.Usuario)
                .ToList();
        }
        //encontrar todas las etiquetas de una publicacion
        public IEnumerable<Notificacion> EncontrarNotificacionesNoLeidas()
        {
            return Contexto.notificaciones
                .Where(n => n.leido == false)
                .Include(n => n.Usuario)
                .ToList();
        }
        //buscamos etiqueta por id
        public Notificacion FindById(int unId)
        {
            var notificacion = Contexto.notificaciones
                .Where(n => n.Id == unId)
                .Include(n => n.Usuario);

            return (Notificacion)notificacion;
        }

        public void Remove(Notificacion obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Notificacion obj)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> ObtenerIdsDeNotificaciones(List<Notificacion> notis)
        {
            List<int> idsEncontrados = new List<int>();

            foreach (Notificacion noti in notis)
            {
                int idEncontrado = Contexto.notificaciones
                    .Where(n => n.Id == noti.Id)
                    .Select(n => n.Id)
                    .FirstOrDefault();

                if (idEncontrado != 0)
                {
                    idsEncontrados.Add(idEncontrado);
                }
            }

            return idsEncontrados;
        }
        public void MarcarNotificacionComoLeida(int notificacionId, int userId)
        {
            // Obtener el usuario con sus notificaciones
            var usuario = Contexto.users
                .Include(u => u.Notificaciones)
                .FirstOrDefault(u => u.Id == userId);

            if (usuario != null)
            {
                // Buscar la notificación por su Id en la lista del usuario
                var notificacion = usuario.Notificaciones?.FirstOrDefault(n => n.Id == notificacionId);

                if (notificacion != null)
                {
                    // Marcar la notificación como leída
                    notificacion.leido = true;

                    // Guardar los cambios en la base de datos
                    Contexto.SaveChanges();
                }
                else
                {
                    // La notificación no fue encontrada en la lista del usuario
                    // Puedes lanzar una excepción o manejarlo de acuerdo a tus requerimientos
                    throw new Exception("Notificación no encontrada en la lista del usuario.");
                }
            }
            else
            {
                // El usuario no fue encontrado
                // Puedes lanzar una excepción o manejarlo de acuerdo a tus requerimientos
                throw new Exception("Usuario no encontrado.");
            }
        }

    }
}

