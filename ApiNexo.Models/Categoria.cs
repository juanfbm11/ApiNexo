using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiNexo.Models
{
    /// <summary>
    /// Representa una categoría de productos en el sistema.
    /// </summary>
    [Table("Categoria")]
    public class Categoria
    {
        /// <summary>
        /// Obtiene o establece el identificador único de la categoría.
        /// </summary>
        [Key]
        public int IdCategoria { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de la categoría.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;        

        /// <summary>
        /// Obtiene o establece la colección de productos asociados a esta categoría.
        /// </summary>
        public ICollection<Producto>? Producto { get; set; }
    }
}