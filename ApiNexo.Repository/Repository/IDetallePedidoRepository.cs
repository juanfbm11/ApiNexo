using ApiNexo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Repository
{
    public interface IDetallePedidoRepository
    {
        Task<IEnumerable<DetallePedido>> GetAll();
        Task<DetallePedido> Add(DetallePedido detalle);
    }
}
    

