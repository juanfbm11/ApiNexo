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
            pago.IdPago = await _db.InsertAsync(pago);
            return pago;
        }
    }
}
