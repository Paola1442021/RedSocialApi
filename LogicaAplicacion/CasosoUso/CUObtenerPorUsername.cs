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
    public class CUObtenerPorUsername : IObtenerPorNombre
    {
        public IRepositorioUsers Repo { get; set; }
        public IRepositorioGrupos RepoGrupos { get; set; }
        public IRepositorioMensajes RepoMsgs { get; set; }
        public IRepositorioNotificaciones RepoNotis { get; set; }
        public IRepositorioPublicaciones RepoPubli { get; set; }
        public CUObtenerPorUsername(IRepositorioUsers repo, IRepositorioGrupos repoGrupos, IRepositorioMensajes repoMsgs,
          IRepositorioNotificaciones repoNotis, IRepositorioPublicaciones repoPubli)
        {
            Repo = repo;
            RepoGrupos = repoGrupos;
            RepoMsgs = repoMsgs;
            RepoNotis = repoNotis;
            RepoPubli = repoPubli;
        }

        public DTOUsuario ObtenerPorUsername(string username)
        {

            var usu = Repo.EncontrarUsuario(username);

            if (usu != null)
            {
                return new DTOUsuario
                {
                    Apellido = usu.Apellido,
                    Email = usu.Email.Value,
                    FechaCreacionCuenta = usu.FechaCreacionCuenta,
                    FechaNacimiento = usu.FechaNacimiento,
                    Id = usu.Id,
                    idgrupos = RepoGrupos.ObtenerIdsDeGrupos(usu.Grupos),
                    idMensajesEnviados = RepoMsgs.ObtenerIdsDeMensajes(usu.MensajesEnviados),
                    idMensajesRecibidos = RepoMsgs.ObtenerIdsDeMensajes(usu.MensajesRecibidos),
                    idNotificaciones = RepoNotis.ObtenerIdsDeNotificaciones(usu.Notificaciones),
                    idPublicaciones = RepoPubli.ObtenerIdsDePublicaciones(usu.Publicaciones),
                    idSeguidores = Repo.ObtenerIdsDeUsers(usu.Seguidores),
                    idSeguidos = Repo.ObtenerIdsDeUsers(usu.Seguidos),
                    NomArchivoFotoPerfil = usu.NomArchivoFotoPerfil,
                    Nombre = usu.Nombre,
                    Username = usu.Username.Value


                };

            }
            return null;
        }
    }
}
