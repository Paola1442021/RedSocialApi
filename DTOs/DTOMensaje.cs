using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DTOMensaje
    {
        public int idReceptor { get; set; }
        public int id { get; set; }
        public int idEmisor { get; set; }
        public string Contenido { get; set; }
        public string? UrlFoto { get; set; }
        public string? UrlVideo { get; set; }
        public DateTime FechaHora { get; set; }

        public bool leido { get; set; }
        public string username1 { get; set; }
        public string username2 { get; set; }

        public DTOMensaje()
        {
        }
    }
}
