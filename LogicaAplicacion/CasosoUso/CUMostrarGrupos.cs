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
    public class CUMostrarGrupos : IMostrarGrupos
    {

        public IRepositorioUsers Repo { get; set; }
        public IRepositorioGrupos RepoGrupos { get; set; }

        public CUMostrarGrupos(IRepositorioUsers repo, IRepositorioGrupos repoGrupos)
        {
            Repo = repo;
            RepoGrupos = repoGrupos;
        }

        public IEnumerable<DTOGrupo> Listado(int idUser)
        {
            IEnumerable<Grupo> grupos = Repo.MostrarGrupos(idUser);
            List<DTOGrupo> listaGrupos = new List<DTOGrupo>();

            foreach (Grupo gru in grupos)
            {
                var dtoGrupo = new DTOGrupo
                {   
                    Fecha = gru.Fecha,
                    idMiembros = (List<int>)RepoGrupos.ObtenerIdsDeMiembrosDeGrupos(gru.Id),
                    Nombre = gru.Nombre,
                    Id = gru.Id
                };

                listaGrupos.Add(dtoGrupo);
            }

            return listaGrupos;
        

    }
    }

}
