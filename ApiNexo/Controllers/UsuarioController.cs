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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var usuario = await _usuarioQueries.GetById(id); 
                if (usuario == null)
                    return NotFound("El usuario no fue encontrado");
                return Ok(usuario); // Retornar el usuario encontrado
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el usuario: {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al obtener el usuario");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody]Usuario usuario)
        {
            try
            {
                if (id != usuario.Id)
                    return StatusCode(StatusCodes.Status404NotFound, "El ID de la URL no coincide con el del usuario enviado.");

                var rs = await _usuarioRepository.Update(usuario);
                if (!rs)
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontró el usuario con el ID especificado");

                return StatusCode(StatusCodes.Status200OK, "El usuario fue actualizado correctamente.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar actualizar el usuario");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error inesperado al actualizar el usuario.");

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(Usuario usuario)
        {
            try
            {
                var rs = await _usuarioRepository.Delete(usuario);
                if (!rs)
                    return StatusCode(StatusCodes.Status404NotFound, "El usuario no fue encontrado.");

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
