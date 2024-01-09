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
    public class CUModificarContrasenia : IModificarContrasenia
    {

        public IRepositorioUsers Repo { get; set; }
        public CUModificarContrasenia(IRepositorioUsers repo)
        {
            Repo = repo;
        }
        public void Modificar(int id, string contrasenia)
        { User encontrado = Repo.FindById(id);
            Repo.ModificarContrasenia(encontrado, contrasenia);

        }





    }
}
