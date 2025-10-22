using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiNexo.Controllers
{
    /// <summary>
    /// Controlador para gestionar los Detalles de los Pedidos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DetallePedidoController : ControllerBase
    {
        private readonly IDetallePedidoRepository _repo;

        /// <summary>
        /// Constructor del controlador.
        /// Recibe el repositorio mediante inyección de dependencias.
        /// </summary>
        /// <param name="repo">Repositorio de DetallePedido</param>
        public DetallePedidoController(IDetallePedidoRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        /// <summary>
        /// Obtiene todos los registros de detalle de pedido existentes.
        /// </summary>
        /// <returns>Lista de DetallePedido</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DetallePedido>), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> Get()
        {
            var detalles = await _repo.GetAll();
            return Ok(detalles);
        }
        /// <summary>
        /// Registra un nuevo detalle de pedido en la base de datos.
        /// </summary>
        /// <param name="detalle">Objeto DetallePedido con la información a registrar</param>
        /// <returns>El detalle creado con su Id generado</returns>
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<DetallePedido>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(DetallePedido detalle)
        {
            if (detalle == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Los datos del detalle del pedido son inválidos.");
            try
            {
                var rs = await _repo.Add(detalle);
                return CreatedAtAction(nameof(Get), new { id = rs.IdDetalle }, rs);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al registrar el detalle del pedido: {ex.Message}");
            }
        }
    }
}