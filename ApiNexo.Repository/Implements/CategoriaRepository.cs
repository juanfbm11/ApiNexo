using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Implements
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IDbConnection _db;

        public CategoriaRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            return await _db.GetAllAsync<Categoria>();
        }

        public async Task<Categoria> Add(Categoria categoria)
        {
            categoria.IdCategoria = await _db.InsertAsync(categoria);
            return categoria;
        }

        public async Task<bool> Update(Categoria categoria)
        {
            return await _db.UpdateAsync(categoria);
        }

        public async Task<bool> Delete(Categoria categoria)
        {
            return await _db.DeleteAsync(categoria);
        }
    }
}


