using ApiNexo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Repository
{
    public interface IPedidoRepository
    {
        Task CrearPedido(Pedido pedido); 
        Task ActualizarPedido(Pedido pedido); 
        Task EliminarPedido(int idPedido);

    }
}
