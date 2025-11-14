using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Implements
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _db;

        public UsuarioRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Usuario> Add(Usuario usuario)
        {         
                usuario.IdUsuario = await _db.InsertAsync(usuario);
                return usuario;          
        }

        public async Task<bool> Delete(Usuario usuario)
        {
            return await _db.DeleteAsync(usuario);
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _db.GetAllAsync<Usuario>();
        }

        public async Task<bool> Update(Usuario usuario)
        {
            return await _db.UpdateAsync(usuario);
        }

        

        public async Task<Usuario> GetById(int IdUsuario)
        {
            return await _db.GetAsync<Usuario>(IdUsuario);
        }
    }
}
