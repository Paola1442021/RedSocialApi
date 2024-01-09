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
    public class CUObtenerMensajes : IObtenerMensajes
    {
        public IRepositorioMensajes Repo { get; set; }

        public CUObtenerMensajes(IRepositorioMensajes repo)
        {
            Repo = repo;
        }



        public IEnumerable<DTOMensaje> ObtenerMensajes(int unId1, int unId2)
        {

            IEnumerable<Mensaje> mensajes = Repo.ObtenerCharlaDeUsuarios(unId1, unId2);
            List<DTOMensaje> listaDTOS = new List<DTOMensaje>();

            foreach (Mensaje msg in mensajes)
            {
                var DTOMensaje = new DTOMensaje
                {
                    Contenido = msg.Contenido,
                    FechaHora = msg.FechaHora,
                    id = msg.Id,
                    UrlFoto = msg.UrlFoto,
                    UrlVideo = msg.UrlVideo,
                    idReceptor = msg.Receptor.Id,
                    idEmisor = msg.Emisor.Id

                };

                listaDTOS.Add(DTOMensaje);
            }

            return listaDTOS;
        }


    }
}
