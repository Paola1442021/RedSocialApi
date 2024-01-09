using DTOs;
using LogicaAplicacion.InterfacesCU;
using LogicaDeNegocio.Dominio;
using LogicaDeNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosoUso
{
    public class CUMostrarNotificaciones : IMostrarNotificaciones
    {
        public IRepositorioUsers Repo { get; set; }
        public CUMostrarNotificaciones(IRepositorioUsers repo)
        {
            Repo = repo;
        }

        public IEnumerable<DTONotificacion> mostrarNotificaciones(int idUser)
        {
            IEnumerable<Notificacion> notificaciones = Repo.Mostrarnotificaciones(idUser);
            List<DTONotificacion> listaNotis = new List<DTONotificacion>();

            foreach (Notificacion noti in notificaciones)
            {
                var dto = new DTONotificacion
                {
                    contenido = noti.Contenido,
                    enlace = noti.Enlace,
                    fecha = noti.Fecha,
                    idUsuario = noti.Usuario.Id,
                    tipo = noti.Tipo.ToString()
                    
                };

                listaNotis.Add(dto);
            }

            return listaNotis;
        }

    }
}
