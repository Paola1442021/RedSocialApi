using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExcepcionesPropias;
using Obligatorio;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicaDeNegocio.Dominio
{
    public class Mensaje : IValidable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue(1)]
        public int Id { get; set; }
        public User? Receptor { get; set; }
        public int EmisorId { get; set; } // Propiedad que representará la clave foránea
        public int ReceptorId { get; set; } // Propiedad que representará la clave foránea


        public User? Emisor { get; set; }

        public string Contenido { get; set; }
        public string? UrlFoto { get; set; }
        public string? UrlVideo { get; set; }
        public DateTime FechaHora { get; set; }

        public bool leido { get; set; }
        [DefaultValue(false)]




        public Mensaje()
        {

        }
        public Mensaje(int id, User receptor, User emisor, DateTime fechaHora, string contenido, string urlFoto, string urlVideo)
        {
            Id = id;
            Receptor = receptor;
            Emisor = emisor;
            FechaHora = fechaHora;
            Contenido = contenido;
            UrlFoto = urlFoto;
            UrlVideo = urlVideo;

            Validar(); // Llamada a la validación después de la inicialización
        }

        public void Validar()
        {
            // Verificar que el mensaje tenga al menos contenido, foto o video
            if (string.IsNullOrEmpty(Contenido) && string.IsNullOrEmpty(UrlFoto) && string.IsNullOrEmpty(UrlVideo))
            {
                throw new MensajeExeption("El mensaje debe tener al menos contenido, foto o video.");
            }

        }
    }
}