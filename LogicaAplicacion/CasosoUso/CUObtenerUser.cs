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
    public class CUObtenerUser : IObtenerUser
    {
        public IRepositorioUsers RepoUser { get; set; }
        public IRepositorioGrupos RepoGrupos {  get; set; }
        public IRepositorioMensajes RepoMsgs { get; set; }
        public IRepositorioNotificaciones RepoNotis { get; set; }
        public IRepositorioPublicaciones RepoPubli { get; set; }

        public CUObtenerUser(IRepositorioUsers repo, IRepositorioGrupos grupos,
            IRepositorioMensajes repoMsgs, IRepositorioNotificaciones repoNotis
            , IRepositorioPublicaciones repoPubli)
        {
            RepoUser = repo;
            RepoGrupos = grupos;
            RepoMsgs = repoMsgs;
            RepoNotis = repoNotis;
            RepoPubli = repoPubli;
        }



        public DTOUsuario EncontrarUsuario(int unId)
        {
            User usu = RepoUser.FindById(unId);



            if (usu != null)
            {
                return new DTOUsuario
                {
                    Apellido = usu.Apellido,
                    Email = usu.Email.Value,
                    FechaCreacionCuenta = usu.FechaCreacionCuenta,
                    FechaNacimiento = usu.FechaNacimiento,
                    Id = unId,
                    idgrupos= RepoGrupos.ObtenerIdsDeGrupos(usu.Grupos),
                    idMensajesEnviados = RepoMsgs.ObtenerIdsDeMensajes(usu.MensajesEnviados),
                    idMensajesRecibidos = RepoMsgs.ObtenerIdsDeMensajes(usu.MensajesRecibidos),
                    idNotificaciones =RepoNotis.ObtenerIdsDeNotificaciones(usu.Notificaciones),
                    idPublicaciones = RepoPubli.ObtenerIdsDePublicaciones(usu.Publicaciones),
                    idSeguidores = RepoUser.ObtenerIdsDeUsers(usu.Seguidores),
                    idSeguidos = RepoUser.ObtenerIdsDeUsers(usu.Seguidos),
                    NomArchivoFotoPerfil = usu.NomArchivoFotoPerfil,
                    Nombre = usu.Nombre,
                    Username = usu.Username.Value
                    

                };

            }
            return null;
        }


    }
}

