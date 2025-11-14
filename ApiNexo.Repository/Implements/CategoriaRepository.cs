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
            try
            {
                categoria.IdCategoria = await _db.InsertAsync(categoria);
                return categoria;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al insertar la categoría: {ex.Message}", ex);
            }
        }

        public async Task<bool> Update(Categoria categoria)
        {
            try
            {
                if (categoria == null)
                    throw new ArgumentNullException(nameof(categoria), "La categoría no puede ser nula.");

                var resultado = await _db.UpdateAsync(categoria);

                if (!resultado)
                    throw new Exception($"No se encontró la categoría con Id {categoria.IdCategoria} para actualizar.");

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la categoría: {ex.Message}", ex);
            }
        }


        public async Task<bool> Delete(Categoria categoria)
        {
            return await _db.DeleteAsync(categoria);
        }

        public async Task<Categoria> GetById(int id)
        {
            return await _db.GetAsync<Categoria>(id);
        }
    }
}


