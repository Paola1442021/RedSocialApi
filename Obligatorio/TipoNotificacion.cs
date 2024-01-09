using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum TipoNotificacion
{
    Mensaje,
    MeGusta,
    Solicitud,
    Comentario
}