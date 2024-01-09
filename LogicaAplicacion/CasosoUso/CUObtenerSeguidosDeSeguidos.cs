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
    public class CUObtenerSeguidosDeSeguidos : IObtenerSeguidosDeSeguidos
    {
        public IRepositorioUsers Repo { get; set; }
        public IRepositorioGrupos RepoG { get; set; }
        public IRepositorioMensajes RepoMsgs { get; set; }
        public IRepositorioNotificaciones RepoNotis { get; set; }
        public IRepositorioPublicaciones RepoPubli { get; set; }

        public CUObtenerSeguidosDeSeguidos(IRepositorioUsers repo, IRepositorioGrupos repoG,IRepositorioMensajes repoMsgs,
           IRepositorioNotificaciones repoNotis, IRepositorioPublicaciones repoPubli)
        {
            Repo = repo;
            RepoG = repoG;
            RepoMsgs = repoMsgs;
            RepoNotis = repoNotis;
            RepoPubli = repoPubli;
        }
        public IEnumerable<DTOUsuario> Lista(int id)
        {
            IEnumerable<User> usuarios = Repo.ObtenerSeguidosDeSeguidos(id);
            List<DTOUsuario> listaUsuarios = new List<DTOUsuario>();

            foreach (User usu in usuarios)
            {
                var DTOUsuario = new DTOUsuario
                {
                   Apellido = usu.Apellido,
                   Email = usu.Email.Value,
                   FechaCreacionCuenta = usu.FechaCreacionCuenta,
                   FechaNacimiento = usu.FechaNacimiento,
                   idgrupos = RepoG.ObtenerIdsDeGrupos(usu.Grupos),
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

                listaUsuarios.Add(DTOUsuario);
            }

            return listaUsuarios;
        }

    }


}

