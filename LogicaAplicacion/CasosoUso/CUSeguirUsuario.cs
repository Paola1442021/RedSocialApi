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
    public class CUSeguirUsuario : ISeguirUsuario
    {
        public IRepositorioUsers Repo { get; set; }
        public CUSeguirUsuario(IRepositorioUsers repo)
        {
            Repo = repo;
        }
        public void SeguirUsuario(int idSeguidor,int idSeguido)
        {
            Repo.SeguirUsuario(idSeguidor, idSeguido);
        }
    }
}
