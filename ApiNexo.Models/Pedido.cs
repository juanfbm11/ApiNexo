using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace ApiNexo.Models
{
    /// <summary>
    /// Representa un pedido en el sistema.
    /// </summary>
    [Table("Pedido")]
    public class Pedido
    {
        /// <summary>
        /// Obtiene o establece el identificador único del pedido.
        /// </summary>
        [Key]
        public int IdPedido { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha en que se realizó el pedido.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador del usuario que realizó el pedido.
        /// </summary>
        public int usuarioId { get; set; }
    }
}

