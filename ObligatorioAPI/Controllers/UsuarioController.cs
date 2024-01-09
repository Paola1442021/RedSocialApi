using DTOs;
using ExcepcionesPropias;
using LogicaAplicacion.CasosoUso;
using LogicaAplicacion.InterfacesCU;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Web.Http.Cors;
using WebAPI;

namespace ObligatorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public ILogin CULogin { get; set; }
        public IAltaUser CUAltaUsuario { get; set; }
        public IModificarContrasenia CuModificar { get; set; }
        public IModificarUsername CuModificarUsuario { get; set; }
        public IBajaUser CuBaja { get; set; }
        public IObtenerUser CuObtUser { get; set; }
        public ISeguirUsuario CuSeguir { get; set; }
        public IObtenerPorNombre CuObtPorNombre { get; set; }
        
        public IAsignarFotoDePerfil CuAsignarFoto { get; set; }
        public IDejarDeSeguir CuDejarSeguir { get; set; }
       
        public IObtenerSeguidosDeSeguidos CuMostrarSeguidosDeSeguidos { get; set; }
        public IMostrarSeguidos CuMostrarSeguidos { get; set; }
        public IMostrarNotificaciones CuMostrarNotis { get; set; }
        public IListadoSeguidores CuListadoSeguidores { get; set; }



        public UsuarioController(IAltaUser CUAltaUsu, ILogin CULog, IBajaUser cuBaja, IModificarContrasenia cuModificar,
            IModificarUsername cuModificarUsuario, IObtenerUser cuObtUser, ISeguirUsuario cuSeguir,
            IObtenerPorNombre cuObtPorNombre, IAsignarFotoDePerfil cuAsignarFoto, IDejarDeSeguir cuDejarSeguir,
            IObtenerSeguidosDeSeguidos cuMostrarSeguidosDeSeguidos, IMostrarSeguidos cuMostrarSeguidos, IMostrarNotificaciones cuMostrarNotis, IListadoSeguidores cuListadoSeguidores)
        {
            this.CUAltaUsuario = CUAltaUsu;
            this.CULogin = CULog;
            this.CuBaja = cuBaja;
            this.CuModificar = cuModificar;
            this.CuModificarUsuario = cuModificarUsuario;
            this.CuObtUser = cuObtUser;
            CuSeguir = cuSeguir;
            CuObtPorNombre = cuObtPorNombre;
            CuAsignarFoto = cuAsignarFoto;
            this.CuMostrarSeguidosDeSeguidos = cuMostrarSeguidosDeSeguidos;
            CuDejarSeguir = cuDejarSeguir;
            CuMostrarSeguidos = cuMostrarSeguidos;
            CuMostrarNotis = cuMostrarNotis;
            CuListadoSeguidores = cuListadoSeguidores;
        }
        /*
        /// <summary>
        /// Esto es para dar de alta un Usuario
        /// </summary>
        /// <param name="u">El objeto que queremos crear </param>
        /// <returns>201 Objeto creado, 400 BadRequest o 500</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("AltaUsuario")]
        //[Authorize(Roles = "admin")]
        public IActionResult AltaUsuario(DTOUsuario u)
        {

            if (u == null)
            {
                return BadRequest("No se envió información para el alta");
            }

            try
            {
                CUAltaUsuario.Alta(u);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrión un error inesperado");
            }
        }*/
        /// <summary>
        /// Esto es para dar de alta un Usuario
        /// </summary>
        /// <param name="usuarioConFoto">El objeto que queremos crear </param>
        /// <returns>201 Objeto creado, 400 BadRequest o 500</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("AltaUsuario")]
        public IActionResult AltaUsuario([FromForm] DTOUsuario usuarioConFoto)
        {
            if (usuarioConFoto == null)
            {
                return BadRequest("No se envió información para el alta");
            }

            try
            {
                // Guardar la foto en una carpeta (ajusta la ruta según tu estructura de carpetas)
                var rutaCarpeta = "wwwroot/imagenes/fotoPerfil/";
                var nombreArchivo = Guid.NewGuid().ToString() + "_" + usuarioConFoto.FotoPerfil.FileName;
                var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    usuarioConFoto.FotoPerfil.CopyTo(stream);
                }

                // Asignar el nombre del archivo a la propiedad NomArchivoFotoPerfil
                usuarioConFoto.NomArchivoFotoPerfil = nombreArchivo;

                // Ahora puedes llamar a tu lógica de negocios
                CUAltaUsuario.Alta(usuarioConFoto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }

        /// <summary>
        /// Realiza el inicio de sesión para un usuario.
        /// </summary>
        /// <param name="username">El nombre de usuario para el inicio de sesión.</param>
        /// <param name="contrasenia">La contraseña para el inicio de sesión.</param>
        /// <returns>
        /// 200 OK si el inicio de sesión es exitoso,
        /// 400 BadRequest si la solicitud es incorrecta,
        /// 401 Unauthorized si el usuario o la contraseña son inválidos,
        /// 500 InternalServerError si ocurre un error inesperado.
        /// </returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("{username}/{contrasenia}", Name = "Loguearse")]
        public IActionResult Login(string username,string contrasenia) // ESTO ES PARA PROBAR SOLAMENTE, PENDIENTE IMPLEMENTARLO
        {
            if (String.IsNullOrEmpty(username)) return BadRequest("El username no puede ser vacio");
            if (String.IsNullOrEmpty(contrasenia)) return BadRequest("La contraseña no puede ser vacia");
            DTOUsuario usu = null;

            try
            {
                usu = CULogin.Login(username, contrasenia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }

            if (usu == null) return Unauthorized("usario o contraseña invalidos ");
            return Ok();
        }



        /// <summary>
        /// Borra un usuario existente.
        /// </summary>
        /// <param name="id">El ID del usuario que se desea eliminar.</param>
        /// <returns>
        /// 204: Operación exitosa. No hay contenido que devolver.
        /// 400: El ID no es válido o no se proporcionó.
        /// 404: No se encontró el usuario con el ID especificado.
        /// 500: Error interno del servidor.
        /// </returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [HttpDelete("BorrarUsuario/{id}")]
        public IActionResult RemoveUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El id debe ser un número mayor a cero.");
            }

            try
            {
                // Lógica para eliminar el usuario con el ID proporcionado
                CuBaja.Baja(id);

                // Retorna un código 204 (NoContent) indicando que la operación fue exitosa.
                return NoContent();
            }
            catch (UsuarioNotFoundException)
            {
                // Retorna un código 404 (NotFound) si el usuario no fue encontrado.
                return NotFound($"No se encontró el usuario con el ID {id}.");
            }
            catch (Exception ex)
            {
                // Retorna un código 500 (InternalServerError) en caso de error interno del servidor.
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        /// <summary>
        /// Cambiar contraenia.
        /// </summary>
        /// <param name="idusu">El ID del user al que queremos cambiar la contrasenia.</param>
        /// <param name="contrasenia">La contrasenia que queremos asignar al user.</param>
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
        [HttpPut("User/{idUsu}/contrasenia/{contrasenia}")]
        public IActionResult CambiarContrasenia(int idUsu, string contrasenia)
        {
            

            if ((idUsu == null || idUsu<1)|| String.IsNullOrEmpty(contrasenia))
            {
                return BadRequest("No se envio informacion para asignarle al usuario");

            }
            try
            {
                CuModificar.Modificar(idUsu, contrasenia);
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

        /// <summary>
        /// Cambiar contraenia.
        /// </summary>
        /// <param name="idusu">El ID del user al que queremos cambiar el nombre de usuario.</param>
        /// <param name="nombreUsu">El username que queremos asignar al user.</param>
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
        [HttpPut("User/{idUsu}/username/{nombreUsu}")]
        public IActionResult CambiarNombreDeUsuario(int idUsu, string nombreUsu)
        {


            if ((idUsu == null || idUsu < 1) || String.IsNullOrEmpty(nombreUsu))
            {
                return BadRequest("No se envio informacion para asignarle al usuario");

            }
            try
            {
                CuModificarUsuario.Modificar(idUsu, nombreUsu);
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


        /// <summary>
        /// Se busca los seguidos de seguidos por id
        /// </summary>
        /// <param name="id">El id del user al que queremos buscarle los seguidos de los seguidos</param>
        /// <returns>user o nulo</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("SeguidosDeSeguidos/{id}", Name = "seguidosDeSeguidos")]
        public IActionResult ObtenerSeguidosDeSeguidos(int id)
        {
            if (id <= 0) { return BadRequest("El id debe ser un numero positivo mayor a cero"); }

            IEnumerable<DTOUsuario> usuarios = null;
            try
            {
                usuarios = CuMostrarSeguidosDeSeguidos.Lista(id);

            }
            catch (Exception ex)
            {

                return StatusCode(500, "ocurrio un error inesperado");
            }

            if (usuarios == null) return NotFound("El usuario con el id  " + id + " no existe");
            return Ok(usuarios);
        }

        /// <summary>
        /// Se busca el usuario por id
        /// </summary>
        /// <param name="id">El id del user que queremos buscar</param>
        /// <returns>user o nulo</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("PorId/{id}", Name = "BuscarUserId")]
        public IActionResult UserID(int id)
        {
            if (id <= 0) { return BadRequest("El id debe ser un numero positivo mayor a cero"); }

            DTOUsuario user = null;
            try
            {
                user = CuObtUser.EncontrarUsuario(id);

            }
            catch (Exception ex)
            {

                return StatusCode(500, "ocurrio un error inesperado");
            }

            if (user == null) return NotFound("El usuario con el id  " + id + " no existe");
            return Ok(user);
        }

        /// <summary>
        /// Se busca el user por nombre
        /// </summary>
        /// <param name="nombre">El nombre del user que queremos buscar</param>
        /// <returns>user o nulo</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("PorNombre/{nombre}", Name = "BuscarUserPorNombre")]
        public IActionResult BuscarPorNombre(string nombre)
        {
            if (String.IsNullOrEmpty(nombre)) { return BadRequest("El nombre no puede ser nulo ni vacio"); }

            DTOUsuario user = null;
            try
            {
                user = CuObtPorNombre.ObtenerPorUsername(nombre);

            }
            catch (Exception ex)
            {

                return StatusCode(500, "ocurrio un error inesperado");
            }

            if (user == null) return NotFound("El usuario con el nombre de usuario  " + nombre + " no existe");
            return Ok(user);
        }

        /// <summary>
        /// sigo a un usuario.
        /// </summary>
        /// <param name="idSeguidor">El ID del usuario que quiere seguir</param>
        /// <param name="idSeguido">El ID del usuario que quiero seguir.</param>
        /// <returns>
        /// 200: Operación exitosa.
        /// 400: No se envió información para asignar al usuario.
        /// 500: Error interno del servidor.
        /// </returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("user/{idSeguidor}/user/{idSeguido}")]
        public IActionResult AsignarSeguidor(int idSeguidor, int idSeguido)
        {



            if (idSeguidor == null || idSeguido == null)
            {
                return BadRequest("No se envio informacion para asignarle al usuario");
            }
            try
            {
                CuSeguir.SeguirUsuario(idSeguidor, idSeguido);

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
        

        
        
        /// <summary>
        /// Asignar foto de perfil a un usuario.
        /// </summary>
        /// <param name="idusu">El ID del user al que queremos actualizar la foto.</param>
        /// <param name="stringFoto">Nombre de archivo que le vamos a poner.</param>
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
        [HttpPut("User/{idUsu}/archivo/{stringFoto}")]
        public IActionResult AsignarFotoDePerfil(int idUsu, string stringFoto)
        {


            if ((idUsu == null || idUsu < 1) || string.IsNullOrEmpty(stringFoto))
            {
                return BadRequest("No se envio informacion para asignarle al usuario");

            }
            try
            {
                CuAsignarFoto.AsignarFotoDePerfil(idUsu, stringFoto);
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
        /// <summary>
        /// Borra un seguidor existente.
        /// </summary>
        /// <param name="idUserSeguidor">El ID del usuario que desea eliminar seguido.</param>
        /// <param name="idUserSeguido">El ID del usuario que voy a dejar de seguir.</param>
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
        [HttpDelete("BorrarFollowers/user/{idUserSeguidor}/user/{idUserSeguido}")]
        public IActionResult BorrarSeguido(int idUserSeguidor, int idUserSeguido)
        {
            if ((idUserSeguidor <= 0) || (idUserSeguido <= 0))
            {
                return BadRequest("El id debe ser número mayor a cero");
            }

            try
            {
                CuDejarSeguir.DejarDeSeguir(idUserSeguidor, idUserSeguido);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }
        /// <summary>
        /// Se busca las notificaciones del usuario
        /// </summary>
        /// <param name="id">El id del user</param>
        /// <returns>user o nulo</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("Notificaciones/{id}", Name = "Notificaciones de usuario")]
        public IActionResult ObtenerNotificaciones(int id)
        {
            if (id <= 0) { return BadRequest("El id debe ser un numero positivo mayor a cero"); }

            IEnumerable<DTONotificacion> notificaciones = null;
            try
            {
                notificaciones = CuMostrarNotis.mostrarNotificaciones(id);

            }
            catch (Exception ex)
            {

                return StatusCode(500, "ocurrio un error inesperado");
            }

            if (notificaciones == null) return NotFound("El usuario con el id  " + id + " no existe o no tiene notificaciones");
            return Ok(notificaciones);
        }

        /// <summary>
        /// Se busca los seguidores del usuario
        /// </summary>
        /// <param name="id">El id del user</param>
        /// <returns>user o nulo</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("seguidores/{id}", Name = "Seguidores de usuario")]
        public IActionResult ObtenerSeguidoresDeUsuario(int id)
        {
            if (id <= 0) { return BadRequest("El id debe ser un numero positivo mayor a cero"); }

            IEnumerable<DTOUsuario> seguidores = null;
            try
            {
                seguidores = CuListadoSeguidores.mostrarSeguidores(id);

            }
            catch (Exception ex)
            {

                return StatusCode(500, "ocurrio un error inesperado");
            }

            if (seguidores == null) return NotFound("El usuario con el id  " + id + " no existe o no tiene seguidores");
            return Ok(seguidores);
        }

        /// <summary>
        /// Se busca los seguidos del usuario
        /// </summary>
        /// <param name="id">El id del user</param>
        /// <returns>user o nulo</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("seguidos/{id}", Name = "Seguidos de usuario")]
        public IActionResult ObtenerSeguidosDeUsuario(int id)
        {
            if (id <= 0) { return BadRequest("El id debe ser un numero positivo mayor a cero"); }

            IEnumerable<DTOUsuario> seguidos = null;
            try
            {
                seguidos = CuMostrarSeguidos.mostrarSeguidos(id);

            }
            catch (Exception ex)
            {

                return StatusCode(500, "ocurrio un error inesperado");
            }

            if (seguidos == null) return NotFound("El usuario con el id  " + id + " no existe o no tiene seguidores");
            return Ok(seguidos);
        }

        /// <summary>
        /// Modifica un usuario existente.
        /// </summary>
        /// <param name="user">El objeto DTOUser con los datos actualizados del user.</param>
        /// <param name="usId">El ID del user que se desea modificar.</param>
        /// <returns>
        /// 200: Operación exitosa. Retorna el objeto de user modificado.
        /// 400: No se envió información para la modificacion o parámetros incorrectos.
        /// 500: Error interno del servidor.
        /// </returns>
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[HttpPut("ModificarUser/{usId}")]

        //public IActionResult ModificarUsuario(DTOUsuario user, int usId)
        //{
        //    if (user == null)
        //    {
        //        return BadRequest("No se envio informacion para el alta");
        //    }
        //    try
        //    {
        //        CuModificar.Modificar(user);
        //        return Ok(user);
        //    }
        //    catch (UserException ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Ocurrio un error inesperado");
        //    }
        //}

    }
}
