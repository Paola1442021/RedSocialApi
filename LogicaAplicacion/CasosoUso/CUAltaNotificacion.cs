using DTOs;
using LogicaAplicacion.InterfacesCU;
using LogicaDeNegocio.Dominio;
using LogicaDeNegocio.InterfacesRepositorio;
using LogicaDeNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosoUso
{
    public class CUAltaNotificacion:IAltaNotificacion
    {


        public IRepositorioNotificaciones RepoNoti { get; set; }
        public IRepositorioUsers RepoUsuarios { get; set; }

        public CUAltaNotificacion(IRepositorioNotificaciones repo, IRepositorioUsers repoUsers)
            {
            RepoNoti = repo;
            RepoUsuarios = repoUsers;
            }


        public void Alta(DTONotificacion noti)
        {// Convertir el valor de cadena a enum
            TipoNotificacion tipoNotificacion;
            try
            {
                tipoNotificacion = (TipoNotificacion)Enum.Parse(typeof(TipoNotificacion), noti.tipo);
            }
            catch (ArgumentException)
            {
                // Manejar el caso en el que el valor de cadena no sea válido para el enum
                throw new ArgumentException("El valor del tipo de notificación no es válido.");
            }

            Notificacion ntf = new()
            {
                Usuario = RepoUsuarios.FindById(noti.idUsuario),
                Contenido = noti.contenido,
                Enlace = noti.enlace,
                Fecha = noti.fecha,
                Tipo = tipoNotificacion
            };

            RepoNoti.Add(ntf);

            // Si es necesario, puedes asignar el Id después de agregar la notificación
            noti.Id = ntf.Id;
        }
    }

    
}
    

