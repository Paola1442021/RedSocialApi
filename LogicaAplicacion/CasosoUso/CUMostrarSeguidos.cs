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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicaAplicacion.CasosoUso
{
    public class CUMostrarSeguidos : IMostrarSeguidos
    {
        public IRepositorioUsers Repo { get; set; }
        public CUMostrarSeguidos(IRepositorioUsers repo)
        {
            Repo = repo;
        }

        public IEnumerable<DTOUsuario> mostrarSeguidos(int idUser)
        {
            IEnumerable<User> seguidos = Repo.MostrarSeguidos(idUser);
            List<DTOUsuario> listaSeguidos = new List<DTOUsuario>();

            foreach (User u in seguidos)
            {
                var dto = new DTOUsuario
                {
                    Apellido = u.Apellido,
                    Id = u.Id,
                    NomArchivoFotoPerfil = u.NomArchivoFotoPerfil,
                    Nombre = u.Nombre,
                    Username = u.Username.Value

                };

                listaSeguidos.Add(dto);
            }

            return listaSeguidos;
        }

    }
}
