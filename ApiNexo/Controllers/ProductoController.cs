using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace ApiNexo.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
       private readonly IProductoRepository _productoRepository;
         private readonly IProductoQueries _productoQueries;
        private readonly ILogger<ProductoController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productoRepository"></param>
        /// <param name="productoQueries"></param>
        /// <param name="logger"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProductoController(IProductoRepository productoRepository, IProductoQueries productoQueries, ILogger<ProductoController> logger)
        {
            _productoRepository = productoRepository ?? throw new ArgumentNullException(nameof(productoRepository));
            _productoQueries = productoQueries ?? throw new ArgumentNullException(nameof(productoQueries));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        ///
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                _logger.LogInformation("Consultando todos los productos");
                var rs = await _productoQueries.Getall();
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
                var producto =  await _productoQueries.GetById(id);
                if (producto == null)               
                    return NotFound("el producto no fue encontrado");
                return Ok(producto); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el producto con ID: {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al obtener el producto");

            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("{id}/pedidos")]
        public async Task<IActionResult> GetPedidosPorUsuario(int id)
        {
            try
            {
                var productos = await _productoQueries.Getall();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar los productos");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al listar productos");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="producto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(int id,[FromBody] Producto producto)
        {
            try
            {
                if (id != producto.Id)
                    return StatusCode(StatusCodes.Status404NotFound, "El ID de la URL no coincide con el del producto enviado.");
                var rs = await _productoRepository.Update(producto);
                if (rs)
                    return Ok("El producto fue actualizado correctamente.");
                else
                    return NotFound("No se encontró el producto con el ID especificado.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el producto con ID: {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el producto");

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Producto producto)
        {
            try
            {
                var newProducto = await _productoRepository.Add(producto);
                return CreatedAtAction(nameof(Get), new { id = newProducto.Id }, newProducto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el producto");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el producto");

            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                var rs = await _productoRepository.Delete(id);
                if (rs)
                    return Ok("El producto fue eliminado correctamente.");
                else
                    return NotFound("No se encontró el producto con el ID especificado.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el producto con ID: {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el producto");

            }
        }
    }

 
}