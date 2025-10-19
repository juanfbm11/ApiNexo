using Dapper.Contrib.Extensions;
using System;

namespace ApiNexo.Models
{
    /// <summary>
    /// Representa un pago realizado por un pedido en el sistema.
    /// </summary>
    [Table("dbo.Pago")]
    public class Pago
    {
        /// <summary>
        /// Obtiene o establece el identificador único del pago.
        /// </summary>
        [Key]
        public int IdPago { get; set; }

        /// <summary>
        /// Identificador del pedido asociado a este pago.
        /// </summary>
        public int IdPedido { get; set; }

        /// <summary>
        /// Fecha en la que se realizó el pago.
        /// </summary>
        public DateTime FechaPago { get; set; }

        /// <summary>
        /// Método de pago utilizado (por ejemplo: efectivo, tarjeta, transferencia).
        /// </summary>
        public string MetodoPago { get; set; } = string.Empty;
    }
}
