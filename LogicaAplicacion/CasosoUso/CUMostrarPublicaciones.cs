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
    public class CUMostrarPublicaciones : IMostrarPublicaciones
    {
        public IRepositorioUsers Repo { get; set; }


        public CUMostrarPublicaciones(IRepositorioUsers repo)
        {
            Repo = repo;
        }

        public IEnumerable<DTOPublicacion> Listado(int idUser)
        {
            IEnumerable<Publicacion> publicaciones = Repo.MostrarPublicacionesDeUsuario(idUser);
            List<DTOPublicacion> listaPublis = new List<DTOPublicacion>();

            foreach (Publicacion pub in publicaciones)
            {
                var dtoPublicacion = new DTOPublicacion
                {
                    idUser = pub.User.Id,
                    Contenido = pub.Contenido,
                    EsComentario = pub.EsComentario,
                    FechaHora = pub.FechaHora,
                    FotoUrl = pub.FotoUrl,
                    Id = pub.Id, // Corregido: Asignar el Id de la publicación
                    PublicacionPadre = pub.PublicacionPadre != null ? (int)pub.PublicacionPadre.Id : 0,
                    VideoUrl = pub.VideoUrl
                };

                listaPublis.Add(dtoPublicacion);
            }

            return listaPublis;
        }

    }
}
