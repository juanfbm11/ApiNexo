using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace ApiNexo.Models
{
    /// <summary>
    /// Representa un usuario en el sistema.
    /// </summary>
    [Table("dabo.Usuario")]
    public class Usuario
    {
        /// <summary>
        /// Obtiene o establece el identificador único del usuario.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del usuario.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el número de teléfono del usuario.
        /// </summary>
        public int Telefono { get; set; }

        /// <summary>
        /// Obtiene o establece la dirección del usuario.
        /// </summary>
        public string Direccion { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece la contraseña del usuario.
        /// </summary>
        public string Contrasena { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece la lista de productos asociados al usuario.
        /// Relación uno a muchos con Producto.
        /// </summary>
        [Write(false)] // No se debe escribir en la base de datos
        public List<Producto>? Productos { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador del municipio al que pertenece el usuario.
        /// Clave foránea para Municipio.
        /// </summary>
        public int MunicipioId { get; set; }

        /// <summary>
        /// Obtiene o establece el municipio asociado al usuario.
        /// Relación muchos a uno con Municipio.
        /// </summary>
        [Write(false)] // No se debe escribir en la base de datos
        public Municipio? Municipio { get; set; }
    }
}
