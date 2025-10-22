using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiNexo.Models
{
    /// <summary>
    /// Representa los detalles de un pedido en el sistema.
    /// </summary>
    [Table("DetallePedido")]
    public class DetallePedido
    {
        /// <summary>
        /// Obtiene o establece el identificador único del detalle del pedido.
        /// </summary>
        [Key]
        public int IdDetalle { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador del pedido al que pertenece este detalle.
        /// </summary>
        public int? IdPedido { get; set; } 

        /// <summary>
        /// Obtiene o establece el identificador del producto asociado a este detalle.
        /// </summary>
        public int IdProducto { get; set; }

        /// <summary>
        /// Obtiene o establece la cantidad del producto en este detalle del pedido.
        /// </summary>
        public int Cantidad { get; set; }        

        /// <summary>
        /// Obtiene o establece el subtotal calculado para este detalle del pedido.
        /// </summary>
        public decimal Total { get; set; }
    }
}