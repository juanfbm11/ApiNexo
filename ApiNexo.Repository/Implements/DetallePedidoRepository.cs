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
            try
            {
                if (detalle == null)
                    throw new ArgumentNullException(nameof(detalle), "El detalle del pedido no puede ser nulo.");

                //  Validar si IdPedido tiene valor y si existe en la base
                if (detalle.IdPedido.HasValue)
                {
                    var pedidoExiste = await _db.GetAsync<Pedido>(detalle.IdPedido.Value);
                    if (pedidoExiste == null)
                        throw new Exception($"El pedido con Id {detalle.IdPedido} no existe en la base de datos.");
                }

                // Insertar el detalle
                detalle.IdDetalle = await _db.InsertAsync(detalle);

                if (detalle.IdDetalle <= 0)
                    throw new Exception("No se pudo insertar el detalle del pedido.");

                return detalle;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar el detalle del pedido: {ex.Message}", ex);
            }
        }

    }
}