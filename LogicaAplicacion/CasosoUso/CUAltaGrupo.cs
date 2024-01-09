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
    public class CUAltaGrupo:IAltaGrupo
    {
        public IRepositorioGrupos RepoGrupos { get; set; }
        public CUAltaGrupo(IRepositorioGrupos repo)
        {
            RepoGrupos = repo;
        }
        public void Alta(DTOGrupo grupo)
        {
            Grupo group = new ()
            {
                Fecha = grupo.Fecha,
                Nombre = grupo.Nombre,

            };
            RepoGrupos.Add(group);

            grupo.Id = group.Id;
        }

    }
}
