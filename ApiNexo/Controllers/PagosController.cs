using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiNexo.Controllers
{
    /// <summary>
    /// Controlador para la gestión de pagos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly IPagoRepository _pagoRepository;

        /// <summary>
        /// Constructor del controlador de pagos.
        /// </summary>
        /// <param name="pagoRepository">Repositorio de pagos.</param>
        public PagosController(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }

        /// <summary>
        /// Obtiene todos los pagos registrados en el sistema.
        /// </summary>
        /// <returns>Lista de pagos.</returns>
        /// <response code="200">Devuelve la lista de pagos registrados.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Pago>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Pago>>> GetAll()
        {
            var pagos = await _pagoRepository.GetAll();
            return Ok(pagos);
        }

        /// <summary>
        /// Crea un nuevo pago.
        /// </summary>
        /// <param name="pago">Objeto con los datos del pago a registrar.</param>
        /// <returns>El pago creado.</returns>
        /// <response code="201">Pago creado correctamente.</response>
        /// <response code="400">Datos inválidos enviados en la solicitud.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Pago), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pago>> Add([FromBody] Pago pago)
        {
            if (pago == null)
                return BadRequest("Los datos del pago son inválidos.");

            var creado = await _pagoRepository.Add(pago);
            return CreatedAtAction(nameof(GetAll), new { id = creado.IdPago }, creado);
        }
    }
}
