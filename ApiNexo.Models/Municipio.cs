using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiNexo.Models
{
    /// <summary>
    /// Representa un municipio en el sistema.
    /// </summary>
    [Table("dbo.Municipio")]
    public class Municipio
    {
        /// <summary>
        /// Obtiene o establece el identificador único del municipio.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del municipio.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el código DANE del municipio.
        /// </summary>
        public string CodigoDane { get; set; } = string.Empty;
    }
}
}
