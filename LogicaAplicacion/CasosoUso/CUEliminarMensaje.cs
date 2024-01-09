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
    public class CUEliminarMensaje : IEliminarMensaje
    {
        public IRepositorioMensajes Repo { get; set; }
        public CUEliminarMensaje(IRepositorioMensajes repo)
        {
            Repo = repo;
        }
        public void EliminarMensaje(int message,int idUser)

        {
            Repo.RemoveMensaje(message, idUser);
        }


    }
}
