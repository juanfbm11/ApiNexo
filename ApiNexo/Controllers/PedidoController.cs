
using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Threading.Tasks;

namespace ApiNexo.Controllers
{
    /// <summary>
    /// Controlador encargado de manejar las operaciones relacionadas con los pedidos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {

        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidoQueries _pedidoQueries;
        private readonly ILogger<PedidoController> _logger;


        /// <summary>
        /// /// Inicializa una nueva instancia del, inyectando las dependencias
        /// necesarias para manejar las operaciones de pedidos y registrar información del sistema.
        /// </summary>
        /// <param name="pedidoRepository">Interfaz para realizar operaciones CRUD sobre los pedidos.</param>
        /// <param name="pedidoQueries">Interfaz para ejecutar consultas y obtener información de los pedidos.<param>
        /// <param name="logger">Componente utilizado para registrar información, advertencias y errores del controlador.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public PedidoController(IPedidoRepository pedidoRepository, IPedidoQueries pedidoQueries, ILogger<PedidoController> logger)
        {
            _pedidoRepository = pedidoRepository ?? throw new ArgumentNullException(nameof(pedidoRepository));
            _pedidoQueries = pedidoQueries ?? throw new ArgumentNullException(nameof(pedidoQueries));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///  Obtiene la lista de pedidos asociados a un usuario específico, 
        /// identificándolo por su ID.
        /// </summary>
        /// <param name="idUsuario">Identificador único del usuario cuyos pedidos se desean consultar.</param>
        /// <returns>Devuelve una lista de pedidos pertenecientes al usuario o un mensaje si no se encuentran resultados.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Pedido>), StatusCodes.Status200OK)]       
        [ProducesResponseType(typeof(IEnumerable<Pedido>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerPedidosPorUsuario([FromQuery] int idUsuario)
        {
            try
            {
                var pedidos = await _pedidoQueries.ObtenerPedidosPorUsuario(idUsuario);
                if (pedidos == null || !pedidos.Any())
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron pedidos para este usuario");

                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los pedidos del usuario {idUsuario}", idUsuario);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al obtener pedidos");
            }
        }

        /// <summary>
        /// Obtiene la información detallada de un pedido específico utilizando su identificador único (ID).
        /// </summary>
        /// <param name="id">Identificador único del pedido que se desea consultar.</param>
        /// <returns>Devuelve los datos del pedido si existe o un mensaje indicando que no fue encontrado.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<Pedido>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Pedido>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerPedido(int id)
        {
            try
            {
                var pedido = await _pedidoQueries.ObtenerPedidoPorId(id);
                if (pedido == null)
                    return StatusCode(StatusCodes.Status404NotFound, "Pedido no encontrado");

                return Ok(pedido);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el pedido con ID: {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al obtener el pedido");
            }
        }

        /// <summary>
        ///  Crea un nuevo pedido en el sistema con la información proporcionada en el cuerpo de la solicitud.
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns>Devuelve un código de estado 201 si el pedido se crea correctamente o un mensaje de error en caso contrario.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Pedido>), StatusCodes.Status201Created)]        
        [ProducesResponseType(typeof(IEnumerable<Pedido>), StatusCodes.Status500InternalServerError)]
        public IActionResult CrearPedido([FromBody] Pedido pedido)
        {
            try
            {
                if (pedido == null)
                    return StatusCode(StatusCodes.Status400BadRequest,"Datos inválidos");

                _pedidoRepository.CrearPedido(pedido);
                return StatusCode(StatusCodes.Status201Created, "Pedido creado correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el pedido");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al crear el pedido");
            }
        }
        /// <summary>
        /// Actualiza un pedido completo por su ID.
        /// </summary>
        /// <param name="id">ID del pedido a actualizar</param>
        /// <param name="pedido">Objeto con los datos actualizados del pedido</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IEnumerable<Pedido>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<Pedido>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Pedido>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarPedido(int id, [FromBody] Pedido pedido)
        {
            try
            {
                if (id != pedido.IdPedido)
                    return StatusCode(StatusCodes.Status400BadRequest,"El ID de la URL no coincide con el del pedido enviado.");

                var pedidoExistente = await _pedidoQueries.ObtenerPedidoPorId(id);
                if (pedidoExistente == null)
                    return StatusCode(StatusCodes.Status400BadRequest,"Pedido no encontrado.");

                pedidoExistente.usuarioId = pedido.usuarioId;
                pedidoExistente.Fecha = pedido.Fecha;

                await _pedidoRepository.ActualizarPedido(pedidoExistente);

                return StatusCode(StatusCodes.Status201Created,"Pedido actualizado correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el pedido con ID: {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al actualizar el pedido.");
            }
        }
        /// <summary>
        ///  Elimina un pedido existente del sistema utilizando su identificador único (ID).
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Devuelve un mensaje de confirmación si el pedido se elimina correctamente o un error si no se encuentra.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IEnumerable<Pedido>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Pedido>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Pedido>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarPedido(int id)
        {
            try
            {
                var pedido = await _pedidoQueries.ObtenerPedidoPorId(id);
                if (pedido == null)
                    return StatusCode(StatusCodes.Status400BadRequest,"Pedido no encontrado");

                await _pedidoRepository.EliminarPedido(id);
                return StatusCode(StatusCodes.Status200OK,"Pedido eliminado correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el pedido con ID: {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al eliminar el pedido");
            }
        }
    }
}