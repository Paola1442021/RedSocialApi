using DTOs;
using ExcepcionesPropias;
using LogicaAplicacion.CasosoUso;
using LogicaAplicacion.InterfacesCU;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace ObligatorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionController : ControllerBase
    {

        public IAltaPublicacion CUAlta { get; set; }
        public IMostrarPublicaciones CuMostrarPublicaciones { get; set; }
        public IBajaPublicacion BajaPublicacion { get; set; }


        public PublicacionController(IAltaPublicacion cUAlta, IMostrarPublicaciones cuMostrarPublicaciones, IBajaPublicacion bajaPublicacion)
        {
            CUAlta = cUAlta;
            CuMostrarPublicaciones = cuMostrarPublicaciones;
            BajaPublicacion = bajaPublicacion;
        }
        /// <summary>
        /// Borra una publicacion existente.
        /// </summary>
        /// <param name="idUser">El ID del usuario que desea eliminar publicaciones.</param>
        /// <param name="idPubli">El ID de la publicacion que se desea eliminar.</param>
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
        [HttpDelete("BorrarPublicacion/user/{idUser}/publicacion/{idPubli}")]
        public IActionResult BorrarPublicacionDeUsuario(int idUser, int idPubli)
        {
            if ((idUser <= 0) || (idPubli <= 0))
            {
                return BadRequest("El id debe ser número mayor a cero");
            }

            try
            {
                // Utiliza los valores de idUser e idMensaje en tu lógica de eliminación
                BajaPublicacion.EliminarPublicacion(idPubli, idUser);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }


        /// <summary>
        /// Se busca las publicaciones por id de usuario
        /// </summary>
        /// <param name="id">El id del user</param>
        /// <returns>user o nulo</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("PublicacionesPorUserId/{id}", Name = "PublicacionesPorUserId")]
        public IActionResult BuscarPublicaciones(int id)
        {
            if (id <= 0) { return BadRequest("El id debe ser un numero positivo mayor a cero"); }

            IEnumerable<DTOPublicacion> publis = null;
            try
            {
                publis = CuMostrarPublicaciones.Listado(id);

            }
            catch (Exception ex)
            {

                return StatusCode(500, "ocurrio un error inesperado");
            }

            if (publis == null) return NotFound("El usuario con el id  " + id + " no existe");
            return Ok(publis);
        }


        /*
        /// <summary>
        /// Esto es para listar las Ecosistemas 
        /// </summary>
        /// <returns>Lista de ecosistemas o nulo</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("ListadosEcosistemas")]
        public IActionResult ListadosEcosistemas()
        {
            IEnumerable<DTOPublicacion> ecosistemas = null;
            try
            {
                ecosistemas = CUListadoEcosistema.Listado();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error inesperado");
            }

            return Ok(ecosistemas);
        }*/
        /// <summary>
        /// Esto es para dar de alta una publicacion
        /// </summary>
        /// <param name="publi">El objeto que queremos crear </param>
        /// <returns>201 Objeto creado, 400 BadRequest o 500</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("RegistroPublicacion")]
        public IActionResult AltaPublicacion(DTOPublicacion publi)
        {

            if (publi == null)
            {
                return BadRequest("No se envio informacion para el alta");
            }
            try
            {
                CUAlta.Alta(publi);
                return Ok();
            }
            catch (PublicacionException ex)
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
