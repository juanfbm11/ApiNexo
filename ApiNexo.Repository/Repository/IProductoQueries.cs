using ApiNexo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Repository
{
    public interface IProductoQueries
    {
        Task<IEnumerable<Producto>> Getall();
        Task<Producto> GetById(int id);
        Task<IEnumerable<Pedido>> GetPedidosPorProducto(int productoId);
    }
}
