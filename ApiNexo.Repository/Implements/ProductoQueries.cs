using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Implements
{
    public class ProductoQueries : IProductoQueries
    {
        private readonly IDbConnection _db;

        public ProductoQueries(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<IEnumerable<Producto>> Getall()
        {
            return await _db.GetAllAsync<Producto>();
        }

        public async Task<Producto> GetById(int id)
        {
            return await _db.GetAsync<Producto>(id);
        }

        public async Task<IEnumerable<Pedido>> GetPedidosPorProducto(int productoId)
        {
            var sql = @"
                        SELECT p.* 
                FROM Pedido p
                INNER JOIN PedidoProducto pp ON p.Id = pp.PedidoId
                WHERE pp.ProductoId = @ProductoId";

            var pedidos = await _db.QueryAsync<Pedido>(sql, new { ProductoId = productoId });
            return pedidos.ToList();
        }
    }
}
