using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Implements
{
    public class PedidoQueries : IPedidoQueries
    {
        private readonly IDbConnection _db;

        public PedidoQueries(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Pedido> ObtenerPedidoPorId(int idPedido)
        {
            // Obtiene un pedido por ID
            return await _db.GetAsync<Pedido>(idPedido);
        }

        public async Task<List<Pedido>> ObtenerPedidosPorUsuario(int usuarioId)
        {
            // Devuelve todos los pedidos de un usuario
            var sql = "SELECT * FROM dbo.Pedido WHERE UsuarioId = @UsuarioId ORDER BY Fecha DESC";
            var result = await _db.QueryAsync<Pedido>(sql, new { UsuarioId = usuarioId });
            return result.ToList();
        }
    }
}
