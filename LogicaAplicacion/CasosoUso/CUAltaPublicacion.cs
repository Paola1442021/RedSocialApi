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
    public class CUAltaPublicacion: IAltaPublicacion
    {
        public IRepositorioPublicaciones  RepoPublicaciones { get; set; }
        public IRepositorioUsers RepoUsuarios { get; set; }

        public CUAltaPublicacion(IRepositorioPublicaciones repo, IRepositorioUsers RepUsuarios)
        {
            RepoPublicaciones = repo;
            RepoUsuarios = RepUsuarios;
        }
        public void Alta(DTOPublicacion publi)
        {


            Publicacion pub = new()
            {
                EsComentario  = publi.EsComentario,
                User = RepoUsuarios.FindById(publi.idUser),
                Contenido = publi.Contenido,
                FotoUrl = publi.FotoUrl,
                VideoUrl = publi.VideoUrl,
                PublicacionPadre = RepoPublicaciones.FindById(publi.PublicacionPadre),
                FechaHora = publi.FechaHora,
                
            };

            RepoPublicaciones.Add(pub);
            publi.Id = pub.Id;

        }
    }
}
