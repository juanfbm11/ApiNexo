
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
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {

        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidoQueries _pedidoQueries;
        private readonly ILogger<PedidoController> _logger;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedidoRepository"></param>
        /// <param name="pedidoQueries"></param>
        /// <param name="logger"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public PedidoController(IPedidoRepository pedidoRepository, IPedidoQueries pedidoQueries, ILogger<PedidoController> logger)
        {
            _pedidoRepository = pedidoRepository ?? throw new ArgumentNullException(nameof(pedidoRepository));
            _pedidoQueries = pedidoQueries ?? throw new ArgumentNullException(nameof(pedidoQueries));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ObtenerPedidosPorUsuario([FromQuery] int idUsuario)
        {
            try
            {
                var pedidos = await _pedidoQueries.ObtenerPedidosPorUsuario(idUsuario);
                if (pedidos == null || !pedidos.Any())
                    return NotFound("No se encontraron pedidos para este usuario");

                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los pedidos del usuario {idUsuario}", idUsuario);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al obtener pedidos");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPedido(int id)
        {
            try
            {
                var pedido = await _pedidoQueries.ObtenerPedidoPorId(id);
                if (pedido == null)
                    return NotFound("Pedido no encontrado");

                return Ok(pedido);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el pedido con ID: {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al obtener el pedido");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CrearPedido([FromBody] Pedido pedido)
        {
            try
            {
                if (pedido == null)
                    return BadRequest("Datos inválidos");

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
        public async Task<IActionResult> ActualizarPedido(int id, [FromBody] Pedido pedido)
        {
            try
            {
                if (id != pedido.IdPedido)
                    return BadRequest("El ID de la URL no coincide con el del pedido enviado.");

                var pedidoExistente = await _pedidoQueries.ObtenerPedidoPorId(id);
                if (pedidoExistente == null)
                    return NotFound("Pedido no encontrado.");

                pedidoExistente.usuarioId = pedido.usuarioId;
                pedidoExistente.Fecha = pedido.Fecha;

                await _pedidoRepository.ActualizarPedido(pedidoExistente);

                return Ok("Pedido actualizado correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el pedido con ID: {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al actualizar el pedido.");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPedido(int id)
        {
            try
            {
                var pedido = await _pedidoQueries.ObtenerPedidoPorId(id);
                if (pedido == null)
                    return NotFound("Pedido no encontrado");

                await _pedidoRepository.EliminarPedido(id);
                return Ok("Pedido eliminado correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el pedido con ID: {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al eliminar el pedido");
            }
        }
    }
}