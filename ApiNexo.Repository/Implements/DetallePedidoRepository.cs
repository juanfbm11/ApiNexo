using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Implements
{
    public class DetallePedidoRepository : IDetallePedidoRepository
    {
        private readonly IDbConnection _db;

        public DetallePedidoRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<IEnumerable<DetallePedido>> GetAll()
        {
            return await _db.GetAllAsync<DetallePedido>();
        }

        public async Task<DetallePedido> Add(DetallePedido detalle)
        {
            detalle.Id = await _db.InsertAsync(detalle);
            return detalle;
        }
    }
}