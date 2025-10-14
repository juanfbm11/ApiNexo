using ApiNexo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Repository
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAll();
        Task<Categoria> Add(Categoria categoria);
        Task<bool> Update(Categoria categoria);
        Task<bool> Delete(Categoria categoria);
    }
}

