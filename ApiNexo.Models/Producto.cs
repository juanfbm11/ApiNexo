using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace ApiNexo.Models
{
    /// <summary>
    /// Representa un producto en el sistema.
    /// </summary>
    [Table("dbo.Producto")]
    public class Producto
    {
        /// <summary>
        /// Obtiene o establece el identificador único del producto.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del producto.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el precio del producto.
        /// </summary>
        public string Precio { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece la URL de la imagen del producto.
        /// </summary>
        public string Imagen { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece la cantidad disponible del producto.
        /// </summary>
        public int Cantidad { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador de la categoría a la que pertenece el producto.
        /// </summary>
        public int CategoriaId { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador del usuario del producto.
        /// </summary>
        public int UsuarioId { get; set; }
    }
}

