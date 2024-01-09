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
    public class CUAltaMeGusta:IAltaMeGusta
    {
        public IRepositorioMeGustas RepoMeGusta { get; set; }
        public IRepositorioPublicaciones RepoPublis { get; set; }
        public IRepositorioUsers RepoUsers { get; set; }

        public CUAltaMeGusta(IRepositorioMeGustas repo, IRepositorioPublicaciones repoPublis, IRepositorioUsers repoUsers)
        {
            RepoMeGusta = repo;
            RepoPublis = repoPublis;
            RepoUsers = repoUsers;
        }

        public void Alta(DTOMeGusta meGusta)
        {
            MeGusta mg = new ()
            {
                Publicacion = RepoPublis.FindById(meGusta.idPublicacion),
                Usuario = RepoUsers.FindById(meGusta.idUsuario)
            };

            RepoMeGusta.Add(mg);
            meGusta.Id = mg.Id;
        }
    }
}
