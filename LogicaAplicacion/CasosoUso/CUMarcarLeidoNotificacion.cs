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
    public class CUMarcarLeidoNotificacion : IMarcarLeidoNotificacion
    {
        public IRepositorioNotificaciones Repo { get; set; }
        public IRepositorioUsers RepoU { get; set; }


        public CUMarcarLeidoNotificacion(IRepositorioNotificaciones repo, IRepositorioUsers repoU)
        {
            Repo = repo;
            RepoU = repoU;
        }
        public void MarcarLeido(int idNotificacion,int idUser)
        {

            User u= RepoU.FindById(idUser);
            Repo.MarcarNotificacionComoLeida(idNotificacion,idUser);

        }
    }
}
