using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Implements
{
    public class MunicipioRepository : IMunicipioRepository
    {
        private readonly IDbConnection _db;
        public MunicipioRepository(IDbConnection db)
        {
            _db=db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Municipio> Add(Municipio municipio)
        {
            if(municipio == null)
                throw new ArgumentNullException(nameof(municipio));
            var id = await _db.InsertAsync(municipio);
            municipio.Id = (int)id;
            return municipio;
        }

        public async Task<bool> Update(Municipio municipio)
        {
            if(municipio == null)
                throw new ArgumentNullException(nameof(municipio));
            return await _db.UpdateAsync(municipio);
        }

        public async Task<bool> Delete(int id)
        {
            var municipio = await _db.GetAsync<Municipio>(id);
            if(municipio == null)
                return false;
            return await _db.DeleteAsync(municipio);
        }
    }
}
