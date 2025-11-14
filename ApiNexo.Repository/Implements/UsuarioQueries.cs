using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Implements
{
    public class UsuarioQueries : IUsuarioQueries
    {
        private readonly IDbConnection _db;

        public UsuarioQueries(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Usuario> Add(Usuario usuario)
        {
            try
            {
                usuario.IdUsuario = await _db.InsertAsync(usuario);
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar el usuario: {ex.Message}", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var usuario = await GetById(id);
            if (usuario == null)
                return false;
            return await _db.DeleteAsync(usuario);
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _db.GetAllAsync<Usuario>();
        }

        public Task<Usuario> GetByCorreo(string correo)
        {
            var sql = "SELECT * FROM Usuario WHERE Correo = @Correo";
            return _db.QueryFirstOrDefaultAsync<Usuario>(sql, new { Correo = correo });
        }

        public async Task<Usuario> GetById(int idusuario)
        {
            var sql = "SELECT * FROM Usuario WHERE IdUsuario = @Id";
            var usuario = await _db.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = idusuario });
            return usuario;
        }

        public async Task<bool> Update(Usuario usuario)
        {
            return await _db.UpdateAsync(usuario);
        }

        

       
    }
}


