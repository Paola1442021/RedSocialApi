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
    public class CUAltaMensaje : IAltaMensaje
    {
        public IRepositorioMensajes RepoMensajes { get; set; }
        public IRepositorioUsers RepoUsu { get; set; }

        public CUAltaMensaje(IRepositorioMensajes repo, IRepositorioUsers repoUsu)
        {
            RepoMensajes = repo;
            RepoUsu = repoUsu;
        }
        public void Alta(DTOMensaje mensaje)
        {


            Mensaje msg = new()
            {
                Contenido = mensaje.Contenido,
                Emisor = RepoUsu.FindById(mensaje.idEmisor),
                FechaHora = mensaje.FechaHora,//ver despues la fecha
                Receptor = RepoUsu.FindById(mensaje.idReceptor),
                UrlFoto = mensaje.UrlFoto,
                UrlVideo = mensaje.UrlVideo,


            };

            RepoMensajes.Add(msg);
            mensaje.id = msg.Id;

        }
    }
    
}
