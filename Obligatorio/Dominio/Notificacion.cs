using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ExcepcionesPropias;
using LogicaDeNegocio.ValueObjects;
using Obligatorio;

namespace LogicaDeNegocio.Dominio
{
    public class Notificacion : IValidable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue(1)]
        public int Id { get; set; }

        public User? Usuario { get; set; }
        public string? Enlace { get; set; }
        public string Contenido { get; set; }
        public bool leido { get; set; }
        [DefaultValue(false)]

        public DateTime Fecha { get; set; }
        public TipoNotificacion Tipo { get; set; }

        public Notificacion()
        {

        }

        public Notificacion(int id, User usuario, string enlace, string contenido, DateTime fecha, TipoNotificacion tipo)
        {
            this.Id = id;
            this.Usuario = usuario;
            this.Fecha = fecha;
            this.Tipo = tipo;

            // Verificar si enlace y contenido son ambos nulos o vacíos
            Validar(); // Llamada a la validación después de la inicialización


            // Asignar valores a las propiedades
            this.Enlace = enlace;
            this.Contenido = contenido;

        }




        public void Validar()
        {
            // Verificar que si no tiene enlace, debe tener contenido
            if (string.IsNullOrEmpty(Enlace) && string.IsNullOrEmpty(Contenido))
            {
                throw new NotificacionException("La notificación debe tener al menos enlace o contenido.");
            }

        }

    }
}

