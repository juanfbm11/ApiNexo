using ApiNexo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Repository
{
    public interface ICategoriaQueries
    {
        Task<IEnumerable<Categoria>> GetCategoriasConProductos();
        Task<Categoria?> GetById(int id);
        Task<IEnumerable<Categoria>> BuscarPorNombre(string nombre);
    }
}