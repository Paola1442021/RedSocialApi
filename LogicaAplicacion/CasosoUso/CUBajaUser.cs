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
    public class CUBajaUser : IBajaUser
    {

        public IRepositorioUsers RepoUsers { get; set; }
        public CUBajaUser(IRepositorioUsers repo)
        {
            RepoUsers = repo;
        }
        public void Baja(int id)
        {
            if (id != null && id>0)
            {
                
                    User user = RepoUsers.FindById(id);
                RepoUsers.Remove(user);

                    // RepoEcosistema.Remove(new Ecosistema() { Id = eco.Id });
                }
            }
        }

    }


