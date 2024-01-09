using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcepcionesPropias;
using LogicaDeNegocio;
using ExcepcionesPropias;
using Obligatorio;
using LogicaDeNegocio.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace LogicaDeNegocio.Dominio
{
    public class Publicacion : IValidable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue(1)]
        public int Id { get; set; }
        [DefaultValue(false)]
        public bool EsComentario { get; set; }
        public User User { get; set; }
        [Required]
        public string? VideoUrl { get; set; }
        public string? FotoUrl { get; set; }
        public List<MeGusta>? MeGustas { get; set; }
        public List<Publicacion>? Comentarios { get; set; }

        public Publicacion? PublicacionPadre { get; set; }

        public string Contenido { get; set; }
        public DateTime FechaHora { get; set; }


        //[ForeignKey("EcosistemaId")]
        //public Ecosistema unEcosistema { get; set; }
        public Publicacion()
        {
            MeGustas = new List<MeGusta>(); // Inicializar Miembros como una lista vacía
            Comentarios = new List<Publicacion>(); // Inicializar Miembros como una lista vacía
        }
        public Publicacion(int id, bool esComentario, Publicacion publicacionPadre, User user, string contenido, string urlFoto, string urlVideo, DateTime fechaHora)
        {
            Id = id;
            EsComentario = esComentario;
            User = user;
            Contenido = contenido;
            PublicacionPadre = publicacionPadre;
            FotoUrl = urlFoto;
            VideoUrl = urlVideo;
            FechaHora = fechaHora;
            MeGustas = new List<MeGusta>();
            Comentarios = new List<Publicacion>();



        }
        public void Validar()
        {
            if (EsComentario)
            {
                // Validar si es un comentario
                if (string.IsNullOrEmpty(Contenido) && string.IsNullOrEmpty(FotoUrl) && string.IsNullOrEmpty(VideoUrl))
                {
                    throw new EtiquetaException("Un comentario debe tener al menos contenido, foto o video.");
                }
                // Puedes agregar más validaciones específicas para comentarios si es necesario
            }
            else
            {
                // Validar si no es un comentario (puede tener foto, video o ambos)
                if (string.IsNullOrEmpty(Contenido) && string.IsNullOrEmpty(FotoUrl) && string.IsNullOrEmpty(VideoUrl))
                {
                    throw new EtiquetaException("La publicación debe tener contenido, foto o video.");
                }
            }
        }

    }
}
