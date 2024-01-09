using ExcepcionesPropias;
using Obligatorio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Dominio
{
    public class Grupo : IValidable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue(1)]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; }
        public List<User>? Miembros { get; set; }

        public Grupo()
        {
            Miembros = new List<User>(); // Inicializar Miembros como una lista vacía

        }

        public Grupo(int id, DateTime fecha, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Fecha = fecha;

            Validar();
            Miembros = new List<User>(); // Inicializar Miembros como una lista vacía

        }




        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new NotificacionException("El grupo debe tener un nombre");
            }

        }
    }
}
