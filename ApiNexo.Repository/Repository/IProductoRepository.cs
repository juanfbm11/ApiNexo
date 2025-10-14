using ApiNexo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Repository
{
   public interface IProductoRepository
    {
        Task<Producto> Add(Producto producto);
        Task<bool> Update(Producto producto);
        Task<bool> Delete(int id);
    }
}
