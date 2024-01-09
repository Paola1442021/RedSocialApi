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
    public class MeGustaController : ControllerBase
    {
        public IAltaMeGusta CUAlta { get; set; }
        /*public IObtenerEspecie CUObtenerEspecie { get; set; }
        public IObtenerAmenaza CUObtenerAmenaza { get; set; }
        public IObtenerEcosistema CUObtenerEcosistema { get; set; }
        public IObtenerPorPeso CuObtenerEspeciesPeso { get; set; }
        public IPorNombreCientifico CUObtenerPorNombreCientifico { get; set; }
        public IObtenerEspeciesDeEcosistemas CuEspeciesDeEco { get; set; }
        public IAgregarEspecieAEcosistema CUAgregarEcosistemaAEspecie { get; set; }
        public IAgregarAmenazaAEspecie CUAgregarAmenazaAEspecie { get; set; }
        public IModificarEspecie CUModificarEspecie { get; set; }
        public IListadoEspecie CUListadoEspecie { get; set; }
        public IObtenerEcosistemasDondeNo CuEcosNo { get; set; }*/
        public MeGustaController(IAltaMeGusta CUalta)
        {

            CUAlta = CUalta;
           
        }
        /// <summary>
        /// Esto es para dar de alta un MeGusta
        /// </summary>
        /// <param name="mg">El objeto que queremos crear </param>
        /// <returns>201 Objeto creado, 400 BadRequest o 500</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("RegistroDeMeGusta")]
        public IActionResult AltaMeGusta(DTOMeGusta mg)
        {

            if (mg == null)
            {
                return BadRequest("No se envio informacion para el alta");
            }
            try
            {
                CUAlta.Alta(mg);
                return Ok();
            }
            catch (MeGustaException ex)/*paisexception*/
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

