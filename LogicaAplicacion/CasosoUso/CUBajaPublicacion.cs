using DTOs;
using LogicaAplicacion.InterfacesCU;
using LogicaDeNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosoUso
{
    public class CUBajaPublicacion : IBajaPublicacion
    {



        public IRepositorioPublicaciones Repo { get; set; }
        public CUBajaPublicacion(IRepositorioPublicaciones repo)
        {
            Repo = repo;
        }
        public void EliminarPublicacion(int publiId, int user)

        {
            Repo.RemovePublicacion(publiId, user);
        }

    }
}
