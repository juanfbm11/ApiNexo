using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiNexo.Controllers
{
    namespace TuProyecto.Controllers
    {
        /// <summary>
        /// 
        /// </summary>
        [ApiController]
        [Route("api/[controller]")]
        public class CategoriasController : ControllerBase
        {
            private readonly ICategoriaRepository _categoriaRepository;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="categoriaRepository"></param>
            public CategoriasController(ICategoriaRepository categoriaRepository)
            {
                _categoriaRepository = categoriaRepository;
            }

            /// <summary>
            /// Obtiene todas las categorías registradas en el sistema.
            /// </summary>
            /// <returns>Lista de categorías.</returns>
            /// <response code="200">Devuelve la lista de categorías.</response>
            [HttpGet]
            [ProducesResponseType(typeof(IEnumerable<Categoria>), StatusCodes.Status200OK)]
            public async Task<ActionResult<IEnumerable<Categoria>>> GetAll()
            {
                var categorias = await _categoriaRepository.GetAll();
                return Ok(categorias);
            }

            /// <summary>
            /// Crea una nueva categoría.
            /// </summary>
            /// <param name="categoria">Objeto con los datos de la categoría a crear.</param>
            /// <returns>La categoría creada.</returns>
            /// <response code="201">Categoría creada correctamente.</response>
            /// <response code="400">Datos inválidos enviados en la solicitud.</response>
            [HttpPost]
            [ProducesResponseType(typeof(IEnumerable<Categoria>), StatusCodes.Status200OK)]
            [ProducesResponseType(typeof(IEnumerable<Categoria>), StatusCodes.Status400BadRequest)]
            [HttpPost]
            public async Task<ActionResult<Categoria>> Add([FromBody] Categoria categoria)
            {
                if (categoria == null)
                    return StatusCode(StatusCodes.Status400BadRequest, "Los datos de la categoría son inválidos.");

                try
                {
                    var creada = await _categoriaRepository.Add(categoria);
                    return CreatedAtAction(nameof(GetAll), new { id = creada.IdCategoria }, creada);
                }
                catch (Exception ex)
                {
                    // Puedes registrar el error si tienes ILogger
                    // _logger.LogError(ex, "Error al insertar la categoría.");

                    // Devuelve el mensaje detallado (para desarrollo)
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error al insertar la categoría: {ex.Message}");
                }
            }


            /// <summary>
            /// Actualiza una categoría existente.
            /// </summary>
            /// <param name="id">ID de la categoría que se desea actualizar.</param>
            /// <param name="categoria">Objeto con los nuevos datos de la categoría.</param>
            /// <response code="204">Categoría actualizada correctamente.</response>
            /// <response code="400">El ID no coincide con la solicitud.</response>
            /// <response code="404">Categoría no encontrada.</response>
            [HttpPut("{id}")]
            [ProducesResponseType(typeof(IEnumerable<Categoria>), StatusCodes.Status204NoContent)]
            [ProducesResponseType(typeof(IEnumerable<Categoria>), StatusCodes.Status400BadRequest)]
            [ProducesResponseType(typeof(IEnumerable<Categoria>), StatusCodes.Status404NotFound)]
            public async Task<bool> Update(int id, [FromBody] Categoria categoria)
            {
                try
                {
                    if (categoria == null)
                        throw new ArgumentNullException(nameof(categoria), "La categoría no puede ser nula.");

                    var resultado = await _categoriaRepository.Update(categoria);

                    if (!resultado)
                        throw new Exception($"No se encontró la categoría con Id {categoria.IdCategoria} para actualizar.");

                    return true;        
                }
                catch (Exception ex)
                {

                    throw new Exception($"Error al actualizar la categoría: {ex.Message}", ex);
                }
            }

            /// <summary>
            /// Elimina una categoría por su ID.
            /// </summary>
            /// <param name="id">ID de la categoría que se desea eliminar.</param>
            /// <response code="204">Categoría eliminada correctamente.</response>
            /// <response code="404">Categoría no encontrada.</response>
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Delete(int id)
            {
                var categoria = new Categoria { IdCategoria = id };
                var eliminado = await _categoriaRepository.Delete(categoria);

                if (!eliminado)
                    return NotFound("La categoría no fue encontrada.");

                return NoContent();
            }
        }
    }
}


