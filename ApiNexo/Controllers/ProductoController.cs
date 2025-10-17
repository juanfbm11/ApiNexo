using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace ApiNexo.Controllers
{
    /// <summary>
    /// Este controlador maneja las operaciones con relacion a los productos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
       private readonly IProductoRepository _productoRepository;
         private readonly IProductoQueries _productoQueries;
        private readonly ILogger<ProductoController> _logger;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ProductoController"/>.
        /// </summary>
        /// <param name="productoRepository">Repositorio de productos.</param>
        /// <param name="productoQueries">Consultas de productos.</param>
        /// <param name="logger">Logger para registrar información y errores.</param>
        public ProductoController(IProductoRepository productoRepository, IProductoQueries productoQueries, ILogger<ProductoController> logger)
        {
            _productoRepository = productoRepository ?? throw new ArgumentNullException(nameof(productoRepository));
            _productoQueries = productoQueries ?? throw new ArgumentNullException(nameof(productoQueries));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        /// <summary>
        /// Obtiene una lista de todos los productos.
        /// </summary>
        /// <returns>Una lista de productos.</returns>
        /// <response code="200">Devuelve la lista de productos.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Producto>), StatusCodes.Status200OK)]        
        [ProducesResponseType(typeof(IEnumerable<Producto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Listar()
        {
            try
            {
                _logger.LogInformation("Consultando todos los productos");
                var rs = await _productoQueries.Getall();
                return StatusCode(StatusCodes.Status200OK,rs);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto.</param>
        /// <returns>El producto correspondiente al ID.</returns>
        /// <response code="200">Devuelve el producto encontrado.</response>
        /// <response code="400">El producto no fue encontrado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<Producto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Producto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Producto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var producto =  await _productoQueries.GetById(id);
                if (producto == null)               
                    return StatusCode(StatusCodes.Status400BadRequest, "el producto no fue encontrado");
                return StatusCode(StatusCodes.Status200OK,producto); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el producto con ID: {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al obtener el producto");

            }
            
        }

        /// <summary>
        /// Obtiene todos los pedidos relacionados con un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto.</param>
        /// <returns>Lista de pedidos relacionados con el producto.</returns>
        /// <response code="200">Devuelve la lista de pedidos.</response>
        /// <response code="404">No se encontraron pedidos.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet("{id}/pedidos")]
        [ProducesResponseType(typeof(IEnumerable<Producto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Producto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Producto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPedidosPorUsuario(int id)
        {
            try
            {
                var productos = await _productoQueries.Getall();
                return StatusCode(StatusCodes.Status200OK, productos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar los productos");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al listar productos");
            }
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="id">ID del producto a actualizar.</param>
        /// <param name="producto">Objeto producto con los nuevos datos.</param>
        /// <returns>Resultado de la actualización.</returns>
        /// <response code="200">El producto fue actualizado correctamente.</response>
        /// <response code="400">El ID no coincide con el producto enviado.</response>
        /// <response code="500">Error al actualizar el producto.</response>
        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<Producto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Producto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Producto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id,[FromBody] Producto producto)
        {
            try
            {
                if (id != producto.Id)
                    return StatusCode(StatusCodes.Status400BadRequest, "El ID de la URL no coincide con el del producto enviado.");
                var rs = await _productoRepository.Update(producto);
                if (rs)
                    return StatusCode(StatusCodes.Status200OK, "El producto fue actualizado correctamente.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, "No se encontró el producto con el ID especificado.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el producto con ID: {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el producto");

            }
        }


        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="producto">Objeto producto a crear.</param>
        /// <returns>El producto creado.</returns>
        /// <response code="201">El producto fue creado correctamente.</response>
        /// <response code="400">El producto no puede ser nulo.</response>
        /// <response code="500">Error al crear el producto.</response>
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Producto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<Producto>), StatusCodes.Status400BadRequest)]         
        [ProducesResponseType(typeof(IEnumerable<Producto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] Producto producto)
        {
            if (producto == null) 
            {
                return StatusCode(StatusCodes.Status400BadRequest,"El producto no puede ser nulo."); 
            }

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
        /// Elimina un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto a eliminar.</param>
        /// <returns>Resultado de la eliminación.</returns>
        /// <response code="200">El producto fue eliminado correctamente.</response>
        /// <response code="400">No se encontró el producto con el ID especificado.</response>
        /// <response code="500">Error al eliminar el producto.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                var rs = await _productoRepository.Delete(id);
                if (rs)
                    return StatusCode(StatusCodes.Status200OK, "El producto fue eliminado correctamente.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest, "No se encontró el producto con el ID especificado.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el producto con ID: {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el producto");

            }
        }
    }

 
}