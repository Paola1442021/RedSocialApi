using DTOs;
using ExcepcionesPropias;
using LogicaAplicacion.CasosoUso;
using LogicaAplicacion.InterfacesCU;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ObligatorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoController : ControllerBase
    {
       // public IListadoPaises CUListadoPaises { get; set; }
        public IAltaGrupo CuAlta { get; set; }
       // public IObtenerPaises CUObtenerPais { get; set; }
       public IMostrarGrupos CuMostrarGrupos {  get; set; }

        public GrupoController(IAltaGrupo cuAlta, IMostrarGrupos cuMostrarGrupos)
        {
            CuAlta = cuAlta;
            CuMostrarGrupos = cuMostrarGrupos;
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("RegistroDeGrupos")]
        public IActionResult AltaGrupos(DTOGrupo group)
        {

            if (group == null)
            {
                return BadRequest("No se envio informacion para el alta");
            }
            try
            {
                CuAlta.Alta(group);
                return Ok(); // Devuelve un código 200 OK sin contenido adicional
            }
            catch (GrupoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error inesperado");
            }
        }

        /// <summary>
        /// Se busca los grupos por id
        /// </summary>
        /// <param name="id">El id del grupos</param>
        /// <returns>user o nulo</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GruposPorUserId/{id}", Name = "gruposPorUserId")]
        public IActionResult BuscarGrupos(int id)
        {
            if (id <= 0) { return BadRequest("El id debe ser un numero positivo mayor a cero"); }

            IEnumerable<DTOGrupo> grupos = null;
            try
            {
                grupos = CuMostrarGrupos.Listado(id);

            }
            catch (Exception ex)
            {

                return StatusCode(500, "ocurrio un error inesperado");
            }

            if (grupos == null) return NotFound("El usuario con el id  " + id + " no existe");
            return Ok(grupos);
        }
       
    }
}
