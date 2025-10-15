using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Implements
{
    public class DetallePedidoQueries : IDetallePedidoQueries
    {
        private readonly IDbConnection _db;

        public DetallePedidoQueries(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<IEnumerable<DetallePedido>> GetAll()
        {
            string sql = "SELECT * FROM dbo.DetallePedido";
            return await _db.QueryAsync<DetallePedido>(sql);
        }
    }
}