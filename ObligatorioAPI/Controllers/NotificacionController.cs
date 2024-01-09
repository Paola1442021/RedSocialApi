using DTOs;
using ExcepcionesPropias;
using LogicaAplicacion.CasosoUso;
using LogicaAplicacion.InterfacesCU;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace ObligatorioAPI.Controllers
{
    public class NotificacionController : Controller
    {
        public IAltaNotificacion CuAlta { get; set; }
        public IMarcarLeidoNotificacion CuMarcarLeidoNotificacion { get; set; }


        public  NotificacionController(IAltaNotificacion cuAlta, IMarcarLeidoNotificacion cuMarcarLeidoNotificacion)
        {
            CuAlta = cuAlta;
            CuMarcarLeidoNotificacion = cuMarcarLeidoNotificacion;
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("RegistroDeNotificaciones")]
        public IActionResult AltaNotificacion(DTONotificacion noti)
        {

            if (noti == null)
            {
                return BadRequest("No se envio informacion para el alta");
            }
            try
            {
                CuAlta.Alta(noti);
                return Ok(); // Devuelve un código 200 OK sin contenido adicional
            }
            catch (NotificacionException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error inesperado");
            }
        }

        /// <summary>
        /// Marcar leido la notificacion.
        /// </summary>
        /// <param name="idUsu">El ID del user al que queremos marcar leida la notificacion.</param>
        /// <param name="idNotificacion">La notificacion que queremos marcar como leido.</param>
        /// <returns>
        /// 200: Operación exitosa.
        /// 400: No se envió información para asignar al user o parámetros incorrectos.
        /// 404: No se encontró el user.
        /// 500: Error interno del servidor.
        /// </returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("User/{idUsu}/notificacion/{idNotificacion}")]
        public IActionResult MarcarLeidoNotificacion(int idUsu, int idNotificacion)
        {


            if ((idUsu == null || idUsu < 1) || (idNotificacion == null || idNotificacion < 1))
            {
                return BadRequest("No se envio informacion para asignarle al usuario");

            }
            try
            {
                CuMarcarLeidoNotificacion.MarcarLeido(idNotificacion, idUsu);
                return Ok();
            }
            catch (UserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error inesperado");
            }

        }

    }
}
