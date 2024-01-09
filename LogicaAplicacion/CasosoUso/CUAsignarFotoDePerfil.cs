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
    public class CUAsignarFotoDePerfil : IAsignarFotoDePerfil
    {
        public IRepositorioUsers Repo { get; set; }
        public CUAsignarFotoDePerfil(IRepositorioUsers repo)
        {
            Repo = repo;
        }
        public void AsignarFotoDePerfil(int unUsuario,string nomArchivo)
        {
            Repo.AsignarFotoPerfil(unUsuario, nomArchivo);
        }


    }
}
