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
    public class CUModificarUsername : IModificarUsername
    {
        public IRepositorioUsers Repo{ get; set; }
        public CUModificarUsername(IRepositorioUsers repo)
        {
            Repo = repo;
        }

        public void Modificar(int id, string username)
        {
            User encontrado = Repo.FindById(id);
            Repo.ModificarNombreDeUsuario(encontrado, username);

        }






    }
}

