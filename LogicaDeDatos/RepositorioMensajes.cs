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
    public class RepositorioMensajes : IRepositorioMensajes
    {

        public EmpresaContext Contexto { get; set; }

        public RepositorioMensajes(EmpresaContext ctx)
        {
            Contexto = ctx;
        }

        public void Add(Mensaje obj)
        {
            if (obj != null)
            {
                obj.Validar();
                Contexto.mensajes.Add(obj);
                Contexto.SaveChanges();
            }
        }

         public IEnumerable<Mensaje> FindAll()
         {
             return Contexto.mensajes
                 .Include(m => m.Emisor)
                 .Include(m => m.Receptor)
                 .ToList();
         }
        //encontrar todas las etiquetas de una publicacion
        public IEnumerable<Mensaje> EncontrarMensajesEnviadosDeUsuario(User usu)
        {
            return Contexto.mensajes
                .Where(msg => msg.Emisor.Id == usu.Id)
                .Include(e => e.Emisor) 
                .Include(e => e.Receptor) 
                .ToList();
        }
        //buscamos etiqueta por id
        public Mensaje FindById(int unId)
        {
            var mensaje = Contexto.mensajes
                .Where(msg => msg.Id == unId)
                .Include(e => e.Emisor)
                .Include(e => e.Receptor);

            return (Mensaje)mensaje;
        }

        public IEnumerable<Mensaje> ObtenerCharlaDeUsuarios(int idUsuario1, int idUsuario2)
        {
            var mensajes = Contexto.mensajes
                .Where(m => (m.Emisor.Id == idUsuario1 && m.Receptor.Id == idUsuario2) ||
                            (m.Emisor.Id == idUsuario2 && m.Receptor.Id == idUsuario1))
                .OrderBy(m => m.FechaHora)
                .Include(m=>m.Receptor)
                .Include(m => m.Emisor)
                .ToList();

            return mensajes;
        }

        public void Remove(Mensaje obj)
        {
            Contexto.mensajes.Remove(obj);
            Contexto.SaveChanges();
        }

        public void Update(Mensaje obj)
        {

            Contexto.mensajes.Update(obj);
            Contexto.SaveChanges();
        }
        public IEnumerable<int> ObtenerIdsDeMensajes(List<Mensaje> mensajes)
        {
            List<int> idsEncontrados = new List<int>();

            foreach (Mensaje msg in mensajes)
            {
                int idEncontrado = Contexto.mensajes
                    .Where(mens => mens.Id == msg.Id)
                    .Select(mens => mens.Id)
                    .FirstOrDefault();

                if (idEncontrado != 0)
                {
                    idsEncontrados.Add(idEncontrado);
                }
            }

            return idsEncontrados;
        }

        public void RemoveMensaje(int messageId, int userId)
        {
            // Obtener el mensaje
            var mensaje = Contexto.mensajes
                .Include(m => m.Receptor)
                .Include(m => m.Emisor)
                .FirstOrDefault(m => m.Id == messageId);

            if (mensaje != null)
            {
                // Verificar si el usuario es el receptor o el emisor del mensaje
                if (mensaje.Receptor.Id == userId || mensaje.Emisor.Id == userId)
                {
                    // Eliminar el mensaje de las listas de mensajes recibidos y enviados
                    mensaje.Receptor?.MensajesRecibidos?.Remove(mensaje);
                    mensaje.Emisor?.MensajesEnviados?.Remove(mensaje);

                    // Eliminar el mensaje de las listas de mensajes del usuario
                    Contexto.mensajes.Remove(mensaje);
                    Contexto.SaveChanges();
                }
                else
                {
                    // El usuario no es el receptor ni el emisor del mensaje
                    // Puedes lanzar una excepción o manejarlo de acuerdo a tus requerimientos
                    throw new Exception("No tienes permisos para eliminar este mensaje.");
                }
            }
            else
            {
                // El mensaje no fue encontrado
                // Puedes lanzar una excepción o manejarlo de acuerdo a tus requerimientos
                throw new Exception("Mensaje no encontrado.");
            }
        }


    }
}
    
