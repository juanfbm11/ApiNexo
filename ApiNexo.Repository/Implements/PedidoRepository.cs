using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Dapper.Contrib.Extensions;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Implements
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IDbConnection _db;

        public PedidoRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task CrearPedido(Pedido pedido)
        {
            if (pedido == null)
                throw new ArgumentNullException(nameof(pedido));

            // Inserta el pedido en la BD
            await _db.InsertAsync(pedido);
        }

        public async Task ActualizarPedido(Pedido pedido)
        {
            if (pedido == null)
                throw new ArgumentNullException(nameof(pedido));

            // Actualiza el pedido completo
            await _db.UpdateAsync(pedido);
        }

        public async Task EliminarPedido(int idPedido)
        {
            // Obtiene el pedido primero
            var pedido = await _db.GetAsync<Pedido>(idPedido);
            if (pedido != null)
            {
                await _db.DeleteAsync(pedido);
            }
        }
    }
}
