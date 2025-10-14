using ApiNexo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace ApiNexo.Repository.Repository 
{ 
    public interface IPedidoQueries 
    { 
        Task<Pedido> ObtenerPedidoPorId(int idPedido); 
        Task<List<Pedido>> ObtenerPedidosPorUsuario(int usuarioId);
    } 
}