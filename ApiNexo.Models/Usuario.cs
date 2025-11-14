using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Text.Json.Serialization;

namespace ApiNexo.Models
{
    /// <summary>
    /// Representa un usuario en el sistema.
    /// </summary>
    [Table("dbo.Usuario")]
    public class Usuario
    {
        /// <summary>
        /// Obtiene o establece el identificador único del usuario.
        /// </summary>
        [Key]
        public int IdUsuario { get; set; }

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
        /// Se Obtiene la fecha de nacimiento de cada persona
        /// </summary>
        public DateTime FechaNacimiento { get; set; }= DateTime.Now;

        /// <summary>
        /// Obtiene o establece la contraseña del usuario.
        /// </summary>
        public string Contrasena { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece la lista de productos asociados al usuario.
        /// Relación uno a muchos con Producto.
        /// </summary>
        [Write(false)] // No se debe escribir en la base de datos
        [JsonIgnore]
        [ForeignKey]
        public List<Producto>? Productos { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador del municipio al que pertenece el usuario.
        /// Clave foránea para Municipio.
        /// </summary>
   
        

        /// <summary>
        /// Obtiene o establece el municipio asociado al usuario.
        /// Relación muchos a uno con Municipio.
        /// </summary>
        [Write(false)] // No se debe escribir en la base de datos
        [ForeignKey]
        public Municipio? Municipio { get; set; }
    }
}
