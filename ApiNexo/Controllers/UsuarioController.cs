using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiNexo.Controllers
{
    /// <summary>
    ///     
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioQueries _usuarioQueries;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioQueries"></param>
        /// /// <param name="usuarioRepository"></param>
        /// <param name="logger"></param>
        /// <exception cref="Exception"></exception>
        public UsuarioController(IUsuarioQueries usuarioQueries,IUsuarioRepository usuarioRepository,
            ILogger<UsuarioController> logger)
        {
            _usuarioQueries = usuarioQueries ?? throw new ArgumentNullException(nameof(usuarioQueries));  
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));   
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// Obtiene la lista completa de usuarios registrados en el sistema.
        /// </summary>
        /// <remarks>
        /// Este endpoint devuelve todos los usuarios almacenados en la base de datos.
        /// </remarks>
        /// <returns>
        /// Devuelve un código de estado 200 (OK) con la lista de usuarios si la operación es exitosa.
        /// </returns>
        /// <response code="200">Devuelve la lista de usuarios registrados.</response>
        /// <response code="500">Error interno del servidor al intentar obtener los usuarios.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Usuario>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Usuario>),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Listar()
        {
            try
            {
                _logger.LogInformation("Consultando todos los usuarios");
                var rs = await _usuarioQueries.GetAll(); 
                return Ok(rs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Obtiene la información detallada de un usuario específico a partir de su identificador único (ID).
        /// </summary>
        /// <param name="id">Identificador único del usuario que se desea consultar.</param>
        /// <remarks>
        /// Este endpoint permite recuperar los datos completos de un usuario existente en el sistema 
        /// mediante su ID. Si el usuario no existe, se devuelve un mensaje indicando que no fue encontrado.
        /// </remarks>
        /// <returns>
        /// Devuelve un código de estado 200 (OK) junto con la información del usuario si se encuentra.
        /// </returns>
        /// <response code="200">Devuelve los datos del usuario solicitado.</response>
        /// <response code="404">El usuario no fue encontrado en la base de datos.</response>
        /// <response code="500">Error interno del servidor al intentar obtener el usuario.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<Usuario>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Usuario>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Usuario>), StatusCodes.Status500InternalServerError)]


        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var usuario = await _usuarioQueries.GetById(id); 
                if (usuario == null)
                    return StatusCode(StatusCodes.Status404NotFound, "El usuario no fue encontrado");
                return Ok(usuario); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el usuario: {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al obtener el usuario");
            }
        }
        /// <summary>
        /// Crea un nuevo usuario en el sistema con la información proporcionada en el cuerpo de la solicitud.
        /// </summary>
        /// <param name="usuario">Objeto que contiene los datos del usuario a registrar.</param>
        /// <remarks>
        /// Este endpoint permite registrar un nuevo usuario en la base de datos.  
        /// Si la operación es exitosa, devuelve la ubicación del nuevo recurso creado junto con su identificador.
        /// </remarks>
        /// <returns>
        /// Devuelve un código de estado 201 (Created) si el usuario se crea correctamente,
        /// o un mensaje de error si ocurre algún problema durante la creación.
        /// </returns>
        /// <response code="201">Usuario creado correctamente.</response>
        /// <response code="400">Los datos proporcionados no son válidos.</response>
        /// <response code="500">Error interno del servidor al intentar crear el usuario.</response>
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Usuario>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Usuario>),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Usuario>),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            try
            {
                var newUsurio = await _usuarioRepository.Add(usuario);
                return CreatedAtAction(nameof(Get), new { id = newUsurio.Id });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el usuario");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al crear el usuario");

            }
        }
        /// <summary>
        /// Actualiza la información de un usuario existente en el sistema.
        /// </summary>
        /// <param name="id">Identificador único del usuario que se desea actualizar.</param>
        /// <param name="usuario">Objeto que contiene los nuevos datos del usuario.</param>
        /// <remarks>
        /// Este endpoint permite modificar la información de un usuario existente.  
        /// El ID proporcionado en la URL debe coincidir con el ID del objeto usuario enviado en el cuerpo de la solicitud.
        /// </remarks>
        /// <returns>
        /// Devuelve un código de estado 200 (OK) si la actualización es exitosa,  
        /// 404 (NotFound) si el usuario no existe o si los IDs no coinciden,  
        /// y 500 (Internal Server Error) si ocurre un error inesperado.
        /// </returns>
        /// <response code="200">El usuario fue actualizado correctamente.</response>
        /// <response code="400">El usuario no fue encontrado o los IDs no coinciden.</response>
        /// <response code="500">Error interno del servidor al intentar actualizar el usuario.</response>
        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<Usuario>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Usuario>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Usuario>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody]Usuario usuario)
        {
            try
            {
                if (id != usuario.Id)
                    return StatusCode(StatusCodes.Status404NotFound, "El ID de la URL no coincide con el del usuario enviado.");

                var rs = await _usuarioRepository.Update(usuario);
                if (!rs)
                    return StatusCode(StatusCodes.Status400BadRequest, "No se encontró el usuario con el ID especificado");

                return StatusCode(StatusCodes.Status200OK, "El usuario fue actualizado correctamente.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar actualizar el usuario");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error inesperado al actualizar el usuario.");

            }
        }
        /// <summary>
        /// Elimina un usuario existente del sistema utilizando la información proporcionada en el cuerpo de la solicitud.
        /// </summary>
        /// <param name="usuario">Objeto que contiene los datos del usuario que se desea eliminar.</param>
        /// <remarks>
        /// Este endpoint permite eliminar un usuario de la base de datos.  
        /// Se debe enviar el objeto del usuario que se desea eliminar en el cuerpo de la solicitud.
        /// </remarks>
        /// <returns>
        /// Devuelve un código de estado 200 (OK) si el usuario se elimina correctamente,  
        /// 404 (NotFound) si no se encuentra el usuario,  
        /// y 500 (Internal Server Error) si ocurre un error durante el proceso.
        /// </returns>
        /// <response code="200">El usuario ha sido eliminado exitosamente.</response>
        /// <response code="404">El usuario no fue encontrado en la base de datos.</response>
        /// <response code="500">Error interno del servidor al intentar eliminar el usuario.</response>
        [HttpDelete]
        [ProducesResponseType(typeof(IEnumerable<Usuario>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Usuario>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Usuario>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Usuario usuario)
        {
            try
            {
                var rs = await _usuarioRepository.Delete(usuario);
                if (!rs)
                    return StatusCode(StatusCodes.Status400BadRequest, "El usuario no fue encontrado.");

                return StatusCode(StatusCodes.Status200OK, "El usuario ha sido eliminado exitosamente.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el usuario con ID: {id}", usuario);
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al eliminar el usuario.");

            }
            
        }
    }
}
