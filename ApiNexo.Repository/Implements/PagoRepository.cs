using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Implements
{
    public class PagoRepository : IPagoRepository
    {
        private readonly IDbConnection _db;

        public PagoRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        /// <summary>
        /// Obtiene todos los pagos registrados en el sistema.
        /// </summary>
        public async Task<IEnumerable<Pago>> GetAll()
        {
            return await _db.GetAllAsync<Pago>();
        }

        /// <summary>
        /// Registra un nuevo pago.
        /// </summary>
        public async Task<Pago> Add(Pago pago)
        {
            try
            {
                if (pago == null)
                    throw new ArgumentNullException(nameof(pago), "El objeto de pago no puede ser nulo.");

                // Validar campos obligatorios
                if (pago.IdPedido <= 0)
                    throw new ArgumentException("Debe asociar el pago a un pedido válido.");

                if (string.IsNullOrWhiteSpace(pago.MetodoPago))
                    throw new ArgumentException("Debe especificar un método de pago.");

                if (pago.FechaPago == default)
                    pago.FechaPago = DateTime.Now; // Asigna fecha actual si no viene desde el cliente

                // Insertar el registro en la base de datos
                pago.IdPago = await _db.InsertAsync(pago);

                // Validar que se haya insertado correctamente
                if (pago.IdPago <= 0)
                    throw new Exception("No se pudo insertar el registro del pago.");

                return pago;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar el pago: {ex.Message}", ex);
            }
        }


    }
}
