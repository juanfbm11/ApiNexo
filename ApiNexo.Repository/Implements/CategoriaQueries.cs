using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Implements
{
    namespace TuProyecto.Repositories.Implements
    {
        public class CategoriaQueries : ICategoriaQueries
        {
            private readonly IDbConnection _db;

            public CategoriaQueries(IDbConnection db)
            {
                _db = db;
            }

            // 🔹 Obtener todas las categorías con sus productos asociados
            public async Task<IEnumerable<Categoria>> GetCategoriasConProductos()
            {
                string sql = @"
                SELECT c.*, p.*
                FROM Categoria c
                LEFT JOIN Producto p ON c.IdCategoria = p.IdCategoria";

                var categoriaDict = new Dictionary<int, Categoria>();

                var categorias = await _db.QueryAsync<Categoria, Producto, Categoria>(
                    sql,
                    (c, p) =>
                    {
                        if (!categoriaDict.TryGetValue(c.IdCategoria, out var categoria))
                        {
                            categoria = c;
                            categoria.Producto = new List<Producto>();
                            categoriaDict.Add(c.IdCategoria, categoria);
                        }

                        if (p != null)
                            categoria.Producto.Add(p);

                        return categoria;
                    },
                    splitOn: "IdCategoria"
                );

                return categoriaDict.Values;
            }

            // 🔹 Obtener una categoría por su ID
            public async Task<Categoria?> GetById(int id)
            {
                string sql = "SELECT * FROM Categoria WHERE IdCategoria = @id";
                return await _db.QueryFirstOrDefaultAsync<Categoria>(sql, new { id });
            }

            // 🔹 Buscar categorías por nombre (parcial o exacto)
            public async Task<IEnumerable<Categoria>> BuscarPorNombre(string nombre)
            {
                string sql = "SELECT * FROM Categoria WHERE Nombre LIKE @nombre";
                return await _db.QueryAsync<Categoria>(sql, new { nombre = $"%{nombre}%" });
            }
        }
    }
}




