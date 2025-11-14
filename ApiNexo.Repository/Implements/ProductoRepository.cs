using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Implements
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IDbConnection _db;
        public ProductoRepository(IDbConnection db) 
        {
            _db=db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Producto> Add(Producto producto)
        {
            var sql = @"
        INSERT INTO Producto (Nombre, Precio, Imagen, Cantidad, Descripcion, IdCategoria, IdUsuario)
        VALUES (@Nombre, @Precio, @Imagen, @Cantidad, @Descripcion, @IdCategoria, @IdUsuario);
        SELECT CAST(SCOPE_IDENTITY() as int);";

            var id = await _db.QuerySingleAsync<int>(sql, producto);
            producto.IdProducto = id;
            return producto;
        }

        public async Task<bool> Delete(int id)
        {
            var producto = _db.GetAsync<Producto>(id);
            if (producto == null)
                return false;
            return await _db.DeleteAsync(producto);
        }

        public async Task<bool> Update(Producto producto)
        {
            if ( producto == null)
                throw new ArgumentNullException(nameof(producto));
            return await _db.UpdateAsync(producto);
        }
    }
}
