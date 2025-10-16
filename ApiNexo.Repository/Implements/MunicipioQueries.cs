using ApiNexo.Models;
using ApiNexo.Repository.Repository;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiNexo.Repository.Implements
{
    public class MunicipioQueries : IMunicipioQueries
    {
        private readonly IDbConnection _db;
        public MunicipioQueries(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<IEnumerable<Municipio>> Gettall()
        {
            return await _db.GetAllAsync<Municipio>();
        }

        public async Task<Municipio> GetById(int id)
        {
            return await _db.GetAsync<Municipio>(id);
        }

        public async Task<IEnumerable<Municipio>> GetMunicipiosPorCodigoDane(int codigoDane)
        {
            var sql = "SELECT * FROM Municipio WHERE CodigoDane = @CodigoDane";
            var municipios = await _db.QueryAsync<Municipio>(sql, new { CodigoDane = codigoDane });
            return municipios.ToList();
        }
    }
}
