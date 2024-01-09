using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DTONotificacion
    {
        public int Id { get; set; }
        public int idUsuario { get; set; }
        public string enlace {  get; set; }
        public string contenido {  get; set; }
        public DateTime fecha {  get; set; }
        public string tipo { get; set; }
        public DTONotificacion()
        {
            //Amenazas = new List<string>();
        }
    }
}
