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
    public class CUListadoSeguidores : IListadoSeguidores
    {
        public IRepositorioUsers Repo { get; set; }
        public CUListadoSeguidores(IRepositorioUsers repo)
        {
            Repo = repo;
        }

        public IEnumerable<DTOUsuario> mostrarSeguidores(int idUser)
        {
            IEnumerable<User> seguidores = Repo.MostrarSeguidores(idUser);
            List<DTOUsuario> listaSeguidores = new List<DTOUsuario>();

            foreach (User u in seguidores)
            {
                var dto = new DTOUsuario
                {
                    Apellido = u.Apellido,
                    Id = u.Id,
                    NomArchivoFotoPerfil = u.NomArchivoFotoPerfil,
                    Nombre = u.Nombre,
                    Username = u.Username.Value

                };

                listaSeguidores.Add(dto);
            }

            return listaSeguidores;
        }

    }
}
