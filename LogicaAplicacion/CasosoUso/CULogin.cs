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
    public class CULogin : ILogin
    {
        public IRepositorioUsers RepoUsuarios { get; set; }

        public CULogin(IRepositorioUsers repo) {

            RepoUsuarios = repo;
        
        }
        
        public DTOUsuario Login(string username, string password)
        {
            User usu = RepoUsuarios.Login(username, password);

            if (usu != null)
            {


                return new DTOUsuario
                {
                    Id = usu.Id,
                    Username = username,
                    

                };

            }

            return null;
        }
    }
}
