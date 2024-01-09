using DTOs;
using ExcepcionesPropias;
using LogicaAplicacion.CasosoUso;
using LogicaAplicacion.InterfacesCU;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;

namespace ObligatorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensajeController : ControllerBase
    {
        public IAltaMensaje cuAlta { get; set; }
        public IEliminarMensaje CuEliminarMensaje {  get; set; }
        public IObtenerMensajes CuObtenerCharla { get; set; }

       // public IListadoAmenazas CUListadoAmenazas { get; set; }

        //  public IObtenerAmenaza CUObtenerAmenaza { get; set; }

        public MensajeController(IAltaMensaje CUAlta, IEliminarMensaje cuEliminarMensaje, IObtenerMensajes cuObtenerCharla)
        {
            cuAlta = CUAlta;
            CuEliminarMensaje= cuEliminarMensaje;
            CuObtenerCharla = cuObtenerCharla;



        }
        /// <summary>
         /// Borra un mensaje existente.
         /// </summary>
         /// <param name="idUser">El ID del usuario que desea eliminar mensajes.</param>
         /// <param name="idMensaje">El ID del mensaje que se desea eliminar.</param>
         /// <returns>
         /// 204: Operación exitosa. No hay contenido que devolver.
         /// 400: El ID no es válido o no se proporcionó.
         /// 404: No se encontró el ecosistema con el ID especificado.
         /// 500: Error interno del servidor.
         /// </returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("BorrarMensaje/user/{idUser}/mensaje/{idMensaje}")]
        public IActionResult BorrarMensajeDeUsuario(int idUser, int idMensaje)
        {
            if ((idUser <= 0) || (idMensaje <= 0))
            {
                return BadRequest("El id debe ser número mayor a cero");
            }

            try
            {
                // Utiliza los valores de idUser e idMensaje en tu lógica de eliminación
                CuEliminarMensaje.EliminarMensaje(idMensaje, idUser);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }

        /// <summary>
        /// Esto es para dar de alta un mensaje
        /// </summary>
        /// <param name="msg">El objeto que queremos crear </param>
        /// <returns>201 Objeto creado, 400 BadRequest o 500</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("RegistroDeMensaje")]
        public IActionResult Alta(DTOMensaje msg)
        {

            if (msg == null)
            {
                return BadRequest("No se envio informacion para el alta");
            }
            try
            {
                cuAlta.Alta(msg);
                return Ok();
            }
            catch (MensajeExeption ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error inesperado");
            }
        }
        /// <summary>
        /// Se busca los chats por id
        /// </summary>
        /// <param name="id">El id del user1</param>
        /// <param name="id2">El id del user2</param>
        /// <returns>user o nulo</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("PorIds/user/{id}/user2/{id2}", Name = "BuscarCharla")]
        public IActionResult EncontrarCharla(int id,int id2)
        {
            if ((id <= 0)|| (id2 <= 0)) { return BadRequest("El id debe ser un numero positivo mayor a cero"); }

            IEnumerable<DTOMensaje> mensajes;
            try
            {mensajes = CuObtenerCharla.ObtenerMensajes(id, id2);

            }
            catch (Exception ex)
            {

                return StatusCode(500, "ocurrio un error inesperado");
            }

            if (mensajes == null) return NotFound("El usuario con el id  " + id + " no existe");
            return Ok(mensajes);
        }


        /*
        /// <summary>
        /// Se busca la amenaza por id
        /// </summary>
        /// <param name="id">El id de la amenaza que queremos buscar</param>
        /// <returns>Ameanza o nulo</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}", Name = "BuscarAmenazaId")]
        public IActionResult Get(int id)
        {
            if (id <= 0) { return BadRequest("El id debe ser un numero positivo mayor a cero"); }

            DTOGrupo amazena = null;
            try
            {
                amazena = CUObtenerAmenaza.EncontrarAmenaza(id);

            }
            catch (Exception ex)
            {

                return StatusCode(500, "ocurrio un error inesperado");
            }

            if (amazena == null) return NotFound("La amenaza con el id " + id + " no existe");
            return Ok(amazena);
        }*/
    }
}
