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
    public class CUDejarDeSeguir : IDejarDeSeguir
    {


        public IRepositorioUsers Repo { get; set; }
        public CUDejarDeSeguir(IRepositorioUsers repo)
        {
            Repo = repo;
        }





        public void DejarDeSeguir(int idUserSeguidor,int idUserSeguido)
        {
            Repo.DejarDeSeguirUsuario(idUserSeguidor, idUserSeguido);
        }



    }
}

