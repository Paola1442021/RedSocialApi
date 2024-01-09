using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DTOPublicacion
    {
        public int Id { get; set; }
       /* public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string? NomArchivoEcosistema { get; set; }

        public decimal Area { get; set; }
        public string Descripcion { get; set; }

        public int idEstado { get; set; }
        public string nombreEstado { get; set; }

        public List<string> paises { get; set; }
        public List<string> Amenazas { get; set; }
        public List<string> Especies { get; set; }
        */
           public bool EsComentario { get; set; }
        public int idUser { get; set; }
         public string   Contenido { get; set; }
       public string? FotoUrl { get; set; }
        public string?  VideoUrl { get; set; }
        public DateTime FechaHora { get; set; }
        public int PublicacionPadre { get; set; }


        public DTOPublicacion()
            {
                /*Amenazas = new List<string>();
                paises = new List<string>();*/
            }
        }

    }

