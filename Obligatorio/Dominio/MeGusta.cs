using ExcepcionesPropias;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio;

namespace LogicaDeNegocio.Dominio
{
    public class MeGusta 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue(1)]
        public int Id { get; set; }

        public User? Usuario { get; set; }
        public int UserId { get; set; } // Propiedad que representará la clave foránea
        public int PublicacionId { get; set; } // Propiedad que representará la clave foránea

        public Publicacion? Publicacion { get; set; }
        
        public MeGusta()
        {

        }

        public MeGusta(int id, User usuario, Publicacion publicacion)
        {
            this.Id = id;
            this.Usuario = usuario;
            this.Publicacion = publicacion;

           
        }


    }
}
